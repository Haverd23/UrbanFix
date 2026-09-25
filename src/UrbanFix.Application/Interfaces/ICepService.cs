using UrbanFix.Application.DTOs;

namespace UrbanFix.Application.Interfaces
{
    public interface ICepService
    {
        Task<EnderecoDTO> ObterEnderecoPorCEPAsync(string cep);
    }
}
