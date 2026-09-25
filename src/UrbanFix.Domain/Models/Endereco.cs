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
                throw new Exception("CEP não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(numero))
                throw new Exception("Numero não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(logradouro))
                throw new Exception("Logradouro não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(bairro))
                throw new Exception("Bairro não pode ser vazio.");

            if (string.IsNullOrWhiteSpace(cidade))
                throw new Exception("Cidade não pode ser vazia.");

            if (string.IsNullOrWhiteSpace(estado))
                throw new Exception("Estado não pode ser vazio.");
        }




    }
}
