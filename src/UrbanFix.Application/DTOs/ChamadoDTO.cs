using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanFix.Application.DTOs
{
    public class ChamadoDTO
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }
        public string Cep { get; set; }
        public string Numero { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
