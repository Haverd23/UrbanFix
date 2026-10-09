using Microsoft.EntityFrameworkCore;
using System;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Ordering;
using UrbanFix.Application.Repository;
using UrbanFix.Core.Pagination;
using UrbanFix.Domain.Models;

namespace UrbanFix.Data.Queries
{
    public class ChamadoQueryRepository : IChamadoQueryRepository
    {
        private readonly ChamadoContext _context;

        public ChamadoQueryRepository(ChamadoContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ChamadoDTO>> ListarAsync(ChamadoFiltroDTO filtro)
        {
            var pagination = new PaginationParams(filtro.Page, filtro.PageSize);

            var query = _context.Chamados.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filtro.Tipo))
            {
                query = query.Where(c => c.Tipo.ToString().ToUpper() == filtro.Tipo.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(filtro.Status))
            {
                query = query.Where(c => c.Status.ToString().ToUpper() == filtro.Status.ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(filtro.Bairro))
            {
                query = query.Where(c => c.Endereco.Bairro.ToUpper() == filtro.Bairro.ToUpper());
            }
            query = AplicarOrdenacao(query, filtro.OrderBy, filtro.OrderDirection);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip(pagination.Skip)
                .Take(pagination.PageSize)
                .Select(c => new ChamadoDTO
                {
                    Id = c.Id,
                    Tipo = c.Tipo.ToString(),
                    Descricao = c.Descricao,
                    DataCriacao = c.DataCriacao,
                    Status = c.Status.ToString(),
                    Cep = c.Endereco.CEP,
                    Numero = c.Endereco.Numero,
                    Logradouro = c.Endereco.Logradouro,
                    Bairro = c.Endereco.Bairro,
                    Cidade = c.Endereco.Cidade,
                    Estado = c.Endereco.Estado

                })
                .ToListAsync();

            return new PagedResult<ChamadoDTO>
            {
                Items = items,
                Page = pagination.Page,
                PageSize = pagination.PageSize,
                TotalItems = totalItems
            };

        }

        private static IQueryable<Chamado> AplicarOrdenacao(IQueryable<Chamado> query,
            ChamadoOrderBy? orderBy, OrderDirection? orderDirection)
        {
            var descending = orderDirection == OrderDirection.Desc;
            return orderBy switch
            {
                ChamadoOrderBy.Bairro => descending
                ? query.OrderByDescending(c => c.Endereco.Bairro)
                : query.OrderBy(c => c.Endereco.Bairro),

                ChamadoOrderBy.DataCriacao => descending
                ? query.OrderByDescending(c => c.DataCriacao)
                : query.OrderBy(c => c.DataCriacao),
                
                ChamadoOrderBy.Status => descending
                ? query.OrderByDescending(c => c.Status)
                : query.OrderBy(c => c.Status),

                ChamadoOrderBy.Tipo => descending
                ? query.OrderByDescending(c => c.Tipo)
                : query.OrderBy(c => c.Tipo),

                _ => query.OrderBy(c => c.DataCriacao)
            };

        }
        }
}
