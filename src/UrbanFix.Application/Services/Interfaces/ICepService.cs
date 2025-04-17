using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanFix.Application.DTOs;

namespace UrbanFix.Application.Services.Interfaces
{
    public interface ICepService
    {
        Task<EnderecoDTO> ObterEnderecoPorCepAsync(string cep);
    }
}
