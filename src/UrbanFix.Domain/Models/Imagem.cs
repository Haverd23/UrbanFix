using UrbanFix.Core.DomainObjects;

namespace UrbanFix.Domain.Models
{
    public partial class Chamado
    {
        public class Imagem
        {
            public string Dados { get; private set; }

            protected Imagem() { }

            public Imagem(string dados)
            {
                Validacao(dados);
                Dados = dados;
            }

            public void Validacao(string dados)
            {
                if (string.IsNullOrEmpty(dados))
                    throw new DomainException("Imagem não pode estar vazia.");

                var imagemBytes = Convert.FromBase64String(dados);
                if (imagemBytes.Length > 5 * 1024 * 1024) 
                    throw new DomainException("Imagem excede o tamanho permitido (5MB).");
            }
        }
    }
}