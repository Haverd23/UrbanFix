

namespace UrbanFix.Application.DTOs
{
    public class ChamadoDTO
    {
        public string Descricao { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; } 
        public DateTime DataCriacao { get; set; }
        public EnderecoDTO? Endereco { get; set; }

    }
}
