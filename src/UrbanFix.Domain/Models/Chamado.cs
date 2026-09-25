using UrbanFix.Domain.Enums;
namespace UrbanFix.Domain.Models
{
    public class Chamado
    {
        public Guid Id { get; private set; }
        public TipoDeProblema Tipo { get; private set; } 
        public string Descricao { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public TipoDeStatus Status { get; private set; }
        public Endereco? Endereco { get; private set; }


        public Chamado(TipoDeProblema tipo, string descricao)
        {
            Validacoes(descricao, tipo);
            Id = Guid.NewGuid();
            DataCriacao = DateTime.UtcNow;
            Status = TipoDeStatus.Aberto;
            Tipo = tipo;
            Descricao = descricao;
        }
        public void DefinirEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }
        private void Validacoes(string descricao,TipoDeProblema tipo)
        {
            if (string.IsNullOrEmpty(descricao) || descricao.Trim().Length < 10)
            {
                throw new Exception("A descrição deve conter no mínimo 10 caracteres");
            }
            if (!Enum.IsDefined(typeof(TipoDeProblema), tipo))
            {
                var tiposValidos = string.Join(", ", Enum.GetNames(typeof(TipoDeProblema)));
                throw new Exception($"Tipo de problema inválido. Tipos válidos: {tiposValidos}");
            }
        }

    }
}
