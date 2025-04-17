

using UrbanFix.Core.DomainObjects;
using UrbanFix.Domain.Models;

namespace UrbanFix.Domain.Tests
{
    public class ChamadoTests
    {
        [Fact(DisplayName = "Deve criar chamado válido com status 'EmAberto'" +
            " quando os dados forem válidos")]
        public void CriarChamado_ComDadosValidos_DeveCriarComStatusAberto()
        {
            //Arrange
            var tipo = Chamado.TipoDeProblema.Buraco;
            var descricao = "Buraco enorme na rua que pode causar acidentes";
            var cep = "01001000";
            var numero = "123";

            //Act
            var chamado = new Chamado(tipo, descricao, cep, numero);

            //Assert
            Assert.Equal(Chamado.TipoDeStatus.EmAberto, chamado.Status);
            Assert.Equal(tipo, chamado.Tipo);
            Assert.Equal(descricao, chamado.Descricao);
            Assert.Equal(cep, chamado.CEP);
            Assert.Equal(numero, chamado.Numero);
            Assert.NotEqual(Guid.Empty, chamado.Id);
            Assert.True((DateTime.UtcNow - chamado.DataCriacao).TotalSeconds < 5);

        }
        [Fact(DisplayName = "Deve lançar DomainException se a descrição for menor que 10, sem" +
            "contar espaços em branco")]
        public void CriarChamado_ComDescricaoMenorQue10Caracteres_DeveLancarException()
        {
            //Arrange
            var tipo = Chamado.TipoDeProblema.Buraco;
            var descricao = "      Buraco   ";
            var cep = "01001000";
            var numero = "123";

            // Act & Assert
            Assert.Throws<DomainException>(() =>
                new Chamado(tipo, descricao, cep, numero));
        }
        [Fact(DisplayName = "Deve lançar DomainException se o CEP não tiver 8 digítos")]
        public void CriarChamado_ComCEPDiferenteDe8Caracteres_DeveLancarException()
        {
            //Arrange
            var descricao = "Buraco enorme na rua, que pode causar acidentes";
            var cep = "000106780";
            var numero = "123";

            // Act & Assert
            Assert.Throws<DomainException>(() =>
                new Chamado(Chamado.TipoDeProblema.Buraco, descricao, cep, numero));
        }
        [Fact(DisplayName = "Deve permitir alteração do status de 'EmAberto' para 'EmAndamento'")]
        public void AlterarStatus_DeEmAbertoParaEmAtendimento_DeveAtualizarStatus()
        {
            //Arrange
            var chamado = new Chamado(Chamado.TipoDeProblema.Buraco, "Descrição válida de buraco", "01001000", "123");

            //Act
            chamado.AtualizarStatus(Chamado.TipoDeStatus.EmAndamento);

            //Assert
            Assert.Equal(Chamado.TipoDeStatus.EmAndamento,chamado.Status);

        }
        [Fact(DisplayName = "Deve lançar DomainException se alterar status de EmAberto para" +
            " Finalizado")]
        public void AlterarStatus_DeEmAbertoParaFinalizado_DeveLancarException()
        {
            // Arrange
            var chamado = new Chamado(Chamado.TipoDeProblema.Lixo, "Lixo acumulado há dias", "01001000", "456");

            // Act & Assert
            Assert.Throws<DomainException>(() =>
                chamado.AtualizarStatus(Chamado.TipoDeStatus.Finalizado));
        }
        [Fact(DisplayName = "Deve lançar DomainException se alterar status de EmAndamento para" +
            "EmAndamento")]
        public void AlterarStatus_DeEmAndamentoParaEmAberto_DeveLancarException()
        {
            //Arrange
            var tipo = Chamado.TipoDeProblema.Lixo;

            var chamado = new Chamado(tipo, "Lixo acumulado há dias", "01001000", "456");
            chamado.AtualizarStatus(Chamado.TipoDeStatus.EmAndamento);

            //Act & Assert
            Assert.Throws<DomainException>(() =>
                chamado.AtualizarStatus(Chamado.TipoDeStatus.EmAberto));

        }
        [Fact(DisplayName = "Deve lançar DomainException se tentar alterar status de um chamado que" +
            " já foi finalizado")]
        public void AlterarStatus_FinalizadoParaQualquerStatus_DeveLancarException()
        {
            //Arrange
            var tipo = Chamado.TipoDeProblema.Buraco;
            var chamado = new Chamado(tipo, "Lixo acumulado há dias", "01001000", "456");
            chamado.AtualizarStatus(Chamado.TipoDeStatus.EmAndamento);
            chamado.AtualizarStatus(Chamado.TipoDeStatus.Finalizado);

            //Act & Assert
            Assert.Throws<DomainException>(() =>
                chamado.AtualizarStatus(Chamado.TipoDeStatus.EmAndamento));
            Assert.Throws<DomainException>(() =>
                chamado.AtualizarStatus(Chamado.TipoDeStatus.EmAberto));

        }

        [Fact(DisplayName = "Deve lançar DomainException se for um tipo de problema inválido")]
        public void CriarChamado_ComTipoInvalido_DeveLancarException()
        {
            // Arrange
            var tipoInvalido = (Chamado.TipoDeProblema)9;

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() =>
                new Chamado(tipoInvalido, "Lixo acumulado há dias", "01001000", "456"));

        }
        [Fact(DisplayName = "Não deve permitir alterar status para o mesmo valor atual")]
        public void AtualizarStatus_ParaOMesmoStatus_DeveLancarException()
        {
            // Arrange
            var chamado = new Chamado(Chamado.TipoDeProblema.Buraco, "Buraco na rua", "01001000", "10");
            chamado.AtualizarStatus(Chamado.TipoDeStatus.EmAndamento);
            // Act & Assert
            Assert.Throws<DomainException>(() =>
                chamado.AtualizarStatus(Chamado.TipoDeStatus.EmAndamento));
        }

        [Fact(DisplayName = "Deve adicionar uma imagem ao chamado")]
        public void AdicionarImagem_DeveAdicionarImagem()
        {
            // Arrange
            var tipo = Chamado.TipoDeProblema.Buraco;
            var descricao = "Buraco grande";
            var cep = "01001000";
            var numero = "123";
            byte[] imagemValida = new byte[1024 * 1024];
            string imagemBase64 = Convert.ToBase64String(imagemValida); 


            var chamado = new Chamado(tipo, descricao, cep, numero);

            // Act
            chamado.AdicionarImagem(new Chamado.Imagem(imagemBase64)); 

            // Assert
            Assert.NotNull(chamado.ImagemBytes);
            Assert.Equal(imagemBase64, chamado.ImagemBytes.Dados);
        }
        [Fact(DisplayName = "Deve preencher endereço após criação do chamado")]
        public void PreencherEndereco_ComDadosValidos_DeveAtribuirEnderecoAoChamado()
        {
            // Arrange
            var chamado = new Chamado(
                Chamado.TipoDeProblema.Lixo,
                "Acúmulo de lixo há dias na rua.",
                "01001000",
                "123");

            var endereco = new Endereco("Rua Exemplo", "Bairro Central", "São Paulo", "SP");

            // Act
            chamado.PreencherEndereco(endereco);

            // Assert
            Assert.NotNull(chamado.Endereco);
            Assert.Equal("São Paulo", chamado.Endereco.Cidade);
        }


    }
}
