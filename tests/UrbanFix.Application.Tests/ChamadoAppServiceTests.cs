using Moq;
using System.ComponentModel;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Services;
using UrbanFix.Application.Services.Interfaces;
using UrbanFix.Domain;
using UrbanFix.Domain.Models;

namespace UrbanFix.Application.Tests
{
    public class ChamadoAppServiceTests
    {
        private readonly Mock<IChamadoRepository> _chamadoRepositoryMock;
        private readonly Mock<ICepService> _cepServiceMock;
        private readonly ChamadoAppService _chamadoAppService;

        public ChamadoAppServiceTests()
        {
            _chamadoRepositoryMock = new Mock<IChamadoRepository>();
            _cepServiceMock = new Mock<ICepService>();
            _chamadoAppService = new ChamadoAppService(_chamadoRepositoryMock.Object, _cepServiceMock.Object);
        }

        [Fact, DisplayName("Adicionar chamado válido deve adicionar chamado")]
        public async Task AdicionarAsync_Valido_DeveAdicionarChamado()
        {
            // Arrange
            var criarChamadoDTO = new CriarChamadoDTO
            {
                Tipo = 1,
                Descricao = "Problema com a cidade",
                CEP = "12345678",
                Numero = "123",
                Base64Imagem = ""
            };

            var enderecoDto = new EnderecoDTO
            {
                Logradouro = "Rua Exemplo",
                Bairro = "Bairro Exemplo",
                Cidade = "Cidade Exemplo",
                Estado = "Estado Exemplo"
            };

            _cepServiceMock.Setup(service => service.ObterEnderecoPorCepAsync(criarChamadoDTO.CEP)).ReturnsAsync(enderecoDto);
            _chamadoRepositoryMock.Setup(repo => repo.Adicionar(It.IsAny<Chamado>())).Returns(Task.CompletedTask);
            _chamadoRepositoryMock.Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await _chamadoAppService.AdicionarAsync(criarChamadoDTO);

            // Assert
            _chamadoRepositoryMock.Verify(repo => repo.Adicionar(It.IsAny<Chamado>()), Times.Once);
            _chamadoRepositoryMock.Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact, DisplayName("Atualizar status válido deve atualizar status do chamado")]
        public async Task AtualizarStatusAsync_Valido_DeveAtualizarStatus()
        {
            // Arrange
            var chamadoId = Guid.NewGuid();
            var novoStatus = Chamado.TipoDeStatus.EmAndamento;
            var chamado = new Chamado(Chamado.TipoDeProblema.Lixo, "Lixo por toda a rua", "12345678", "123");

            _chamadoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(chamadoId)).ReturnsAsync(chamado);
            _chamadoRepositoryMock.Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await _chamadoAppService.AtualizarStatusAsync(chamadoId, novoStatus);

            // Assert
            Assert.Equal(novoStatus, chamado.Status);
            _chamadoRepositoryMock.Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact, DisplayName("Remover chamado válido deve remover chamado")]
        public async Task RemoverAsync_Valido_DeveRemoverChamado()
        {
            // Arrange
            var chamadoId = Guid.NewGuid();
            var chamado = new Chamado(Chamado.TipoDeProblema.FaltadeLuz, "Problema na rede", "12345678", "123");

            _chamadoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(chamadoId)).ReturnsAsync(chamado);
            _chamadoRepositoryMock.Setup(repo => repo.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            await _chamadoAppService.RemoverAsync(chamadoId);

            // Assert
            _chamadoRepositoryMock.Verify(repo => repo.Remover(chamado), Times.Once);
            _chamadoRepositoryMock.Verify(repo => repo.UnitOfWork.Commit(), Times.Once);
        }

        [Fact, DisplayName("Obter por ID válido deve retornar chamado")]
        public async Task ObterPorIdAsync_Valido_DeveRetornarChamado()
        {
            // Arrange
            var chamado = new Chamado(Chamado.TipoDeProblema.FaltadeLuz, "Lâmpada queimada na rua principal", "12345678", "123");

            _chamadoRepositoryMock.Setup(r => r.ObterPorIdAsync(chamado.Id)).ReturnsAsync(chamado);

            // Act
            var resultado = await _chamadoAppService.ObterPorIdAsync(chamado.Id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(chamado.Descricao, resultado.Descricao);
            Assert.Equal(chamado.CEP, resultado.CEP);
        }

        [Fact, DisplayName("Obter todos deve retornar todos os chamados")]
        public async Task ObterTodosAsync_DeveRetornarTodosChamados()
        {
            // Arrange
            var chamados = new List<Chamado>
            {
                new Chamado(Chamado.TipoDeProblema.Lixo, "Lixo acumulado", "12345678", "10"),
                new Chamado(Chamado.TipoDeProblema.FaltadeLuz, "Falta de luz", "87654321", "22")
            };

            _chamadoRepositoryMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(chamados);

            // Act
            var resultado = await _chamadoAppService.ObterTodosAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact, DisplayName("Atualizar status com chamado inexistente deve lançar exceção")]
        public async Task AtualizarStatusAsync_ChamadoNaoExiste_DeveLancarExcecao()
        {
            // Arrange
            var chamadoId = Guid.NewGuid();
            _chamadoRepositoryMock.Setup(repo => repo.ObterPorIdAsync(chamadoId)).ReturnsAsync((Chamado)null!);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _chamadoAppService.AtualizarStatusAsync(chamadoId, Chamado.TipoDeStatus.EmAndamento));
        }

      
        [Fact, DisplayName("Obter por tipo deve retornar apenas chamados do tipo especificado")]
        public async Task ObterPorTipoAsync_DeveRetornarChamadosDoTipo()
        {
            // Arrange
            var tipo = Chamado.TipoDeProblema.Lixo;
            var chamados = new List<Chamado>
            {
                new Chamado(tipo, "Lixo na praça", "12345678", "1"),
                new Chamado(tipo, "Lixo na rua", "87654321", "2")
            };

            _chamadoRepositoryMock.Setup(r => r.ObterPorTipoAsync(tipo)).ReturnsAsync(chamados);

            // Act
            var resultado = await _chamadoAppService.ObterPorTipoAsync(tipo);

            // Assert
            Assert.NotNull(resultado);
            Assert.All(resultado, c => Assert.Equal(tipo.ToString(), c.Tipo));
            Assert.Equal(2, resultado.Count());
        }

        [Fact, DisplayName("Obter por status deve retornar apenas chamados com o status especificado")]
        public async Task ObterPorStatusAsync_DeveRetornarChamadosComStatus()
        {
            // Arrange
            var status = Chamado.TipoDeStatus.EmAberto;
            var chamados = new List<Chamado>
        {
            new Chamado(Chamado.TipoDeProblema.Lixo, "Muito lixo na rua", "12345678", "1"),
            new Chamado(Chamado.TipoDeProblema.FaltadeLuz, "Falta luz na rua", "87654321", "2")
        };
            _chamadoRepositoryMock.Setup(r => r.ObterPorStatusAsync(status))
                .ReturnsAsync(chamados);  

            // Act
            var resultado = await _chamadoAppService.ObterPorStatusAsync(status);

            // Assert
            Assert.NotNull(resultado);
            Assert.All(resultado, c => Assert.Equal(status.ToString(), c.Status));  
            Assert.Equal(2, resultado.Count());
        }


        [Fact, DisplayName("Obter por tipo mais antigos deve retornar chamados ordenados por data")]
        public async Task ObterPorTipoMaisAntigosAsync_DeveRetornarOrdenadosPorData()
        {
            // Arrange
            var tipo = Chamado.TipoDeProblema.Lixo;
            var chamados = new List<Chamado>
            {
                new Chamado(tipo, "Mais antigo", "12345678", "1"),
                new Chamado(tipo, "Mais recente", "12345678", "2")
            };

            var agora = DateTime.UtcNow;
            chamados[0].GetType().GetProperty("DataCriacao")!.SetValue(chamados[0], agora.AddDays(-2));
            chamados[1].GetType().GetProperty("DataCriacao")!.SetValue(chamados[1], agora);

            _chamadoRepositoryMock.Setup(r => r.ObterPorTipoMaisAntigosAsync(tipo)).ReturnsAsync(chamados.OrderBy(c => c.DataCriacao).ToList());

            // Act
            var resultado = await _chamadoAppService.ObterPorTipoMaisAntigosAsync(tipo);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Mais antigo", resultado.First().Descricao);
        }

        [Fact, DisplayName("Obter por status mais antigos deve retornar chamados ordenados por data")]
        public async Task ObterPorStatusMaisAntigosAsync_DeveRetornarOrdenadosPorData()
        {
            // Arrange
            var status = Chamado.TipoDeStatus.EmAberto;
            var chamados = new List<Chamado>
            {
                new Chamado(Chamado.TipoDeProblema.Lixo, "Mais antigo", "12345678", "1"),
                new Chamado(Chamado.TipoDeProblema.FaltadeLuz, "Mais recente", "12345678", "2")
            };

            var agora = DateTime.UtcNow;
            chamados[0].GetType().GetProperty("DataCriacao")!.SetValue(chamados[0], agora.AddDays(-2));
            chamados[1].GetType().GetProperty("DataCriacao")!.SetValue(chamados[1], agora);

            _chamadoRepositoryMock.Setup(r => r.ObterPorStatusMaisAntigosAsync(status)).ReturnsAsync(chamados.OrderBy(c => c.DataCriacao).ToList());

            // Act
            var resultado = await _chamadoAppService.ObterPorStatusMaisAntigosAsync(status);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Mais antigo", resultado.First().Descricao);
        }
    }
}
    

