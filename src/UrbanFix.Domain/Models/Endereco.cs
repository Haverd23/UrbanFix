using UrbanFix.Core.Domain;

namespace UrbanFix.Domain.Models
{
    public class Endereco
    {
        public string CEP { get; private set; }
        public string Numero { get; private set; }
        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        protected Endereco() { }
        public Endereco(string cep, string numero, string logradouro, string bairro, string cidade, string estado)
        {
            ValidaEndereco(cep, numero, logradouro, bairro, cidade, estado);
            CEP = cep;
            Numero = numero;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
        private void ValidaEndereco(string cep, string numero,string logradouro, string bairro, string cidade, string estado)
        {
            if (string.IsNullOrWhiteSpace(cep))
                throw new DomainException("CEP não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(numero))
                throw new DomainException("Numero não pode ser vazio.");

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
