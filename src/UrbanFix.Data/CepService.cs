using System.Net.Http.Json;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Interfaces;

namespace UrbanFix.Data
{
    public class CepService : ICepService
    {
        private readonly HttpClient _http;

        public CepService(HttpClient http)
        {
            _http = http;
        }

        public async Task<EnderecoDTO> ObterEnderecoPorCEPAsync(string cep)
        {
            var response = await _http.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

            response.EnsureSuccessStatusCode();

            var content =
                await response.Content.ReadFromJsonAsync<ViaCepResponse>();

            if (content is null || content.Erro)
                throw new Exception("CEP inválido ou não encontrado.");

            return new EnderecoDTO
            {
                Cep = content.CEP,
                Logradouro = content.Logradouro,
                Bairro = content.Bairro,
                Cidade = content.Localidade,
                Estado = content.Uf
            };



        }

        private class ViaCepResponse
        {
            public string CEP { get; set; }
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string Uf { get; set; }
            public bool Erro { get; set; }

           
        }

    }

}
