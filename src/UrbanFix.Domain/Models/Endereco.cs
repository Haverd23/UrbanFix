using UrbanFix.Core.DomainObjects;

namespace UrbanFix.Domain.Models
{
    public class Endereco
    {
        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        protected Endereco() { }

        public Endereco(string logradouro, string bairro, string cidade, string estado)
        {
            ValidaEndereco(logradouro, bairro, cidade, estado);

            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        private void ValidaEndereco(string logradouro,string bairro, string cidade, string estado)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
                throw new DomainException("Logradouro não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(bairro))
                throw new DomainException("Bairro não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(cidade))
                throw new DomainException("Cidade não pode ser vazia.");

            if (string.IsNullOrWhiteSpace(estado))
                throw new DomainException("Estado não pode ser vazio.");
        }

    }
}
