namespace UrbanFix.Api.Request
{
    public class CriarChamadoRequest
    {
        public int Tipo { get; set; }
        public string Descricao { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
    }
}
