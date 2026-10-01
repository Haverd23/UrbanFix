using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanFix.Core.Web.ApiResponse
{
    public class ApiResponse<T>
    {
        public bool Sucesso { get; init; }
        public string Mensagem { get; init; }
        public T? Dados { get; init; }


        public static ApiResponse<T> OK(T dados,string? mensagem = null)
        {
            return new ApiResponse<T>
            {
                Sucesso = true,
                Mensagem = mensagem,
                Dados = dados
            };
        }
        public static ApiResponse<T> Fail(string? mensagem = null)
        {
            return new ApiResponse<T>
            {
                Sucesso = false,
                Mensagem = mensagem,
                Dados = default
            };
        }
    }
}
