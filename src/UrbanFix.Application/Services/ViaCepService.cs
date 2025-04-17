using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Services.Interfaces;

namespace UrbanFix.Application.Services
{
    public class ViaCepService : ICepService
    {
        private readonly HttpClient _httpClient;

        public ViaCepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EnderecoDTO> ObterEnderecoPorCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadFromJsonAsync<ViaCepResponse>();

            if (content == null || !string.IsNullOrEmpty(content.Erro))
                throw new Exception("CEP inválido ou não encontrado.");

            return new EnderecoDTO
            {
                Logradouro = content.Logradouro,
                Bairro = content.Bairro,
                Cidade = content.Localidade,
                Estado = content.Uf
            };
        }

        private class ViaCepResponse
        {
            public string Logradouro { get; set; }
            public string Bairro { get; set; }
            public string Localidade { get; set; }
            public string Uf { get; set; }
            public string Erro { get; set; }
        }
    }
}
