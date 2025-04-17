namespace UrbanFix.Application.DTOs
{
    public class CriarChamadoDTO
    {
        public int Tipo { get; set; }
        public string Descricao { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string? Base64Imagem { get; set; }
    }
}
