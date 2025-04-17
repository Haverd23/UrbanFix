using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UrbanFix.Core.DomainObjects;

namespace UrbanFix.Domain.Models
{
    public partial class Chamado : Entity, IAggregateRoot
    {

     
        public TipoDeProblema Tipo { get; private set; }
        public string Descricao { get; private set; }
        public string CEP { get; private set; }
        public string Numero { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public TipoDeStatus Status {  get; private set; }
        public Imagem? ImagemBytes { get; private set; }
        public Endereco? Endereco { get; private set; }

        protected Chamado() { }

        public Chamado(TipoDeProblema tipo, string descricao, string cep, string numero)
        {
            
            Validacoes(descricao,cep,tipo,numero);
            Tipo = tipo;
            Descricao = descricao;
            CEP = cep;
            Numero = numero;
            DataCriacao = DateTime.UtcNow;
            Status = TipoDeStatus.EmAberto;
            ImagemBytes = null;

        }

        public void DefinirEndereco(Endereco endereco)
        {
            Endereco = endereco ?? throw new DomainException("Endereço inválido");
        }

        public void PreencherEndereco(Endereco endereco)
        {
            if (endereco == null)
                throw new DomainException("Endereço não pode ser nulo.");    

            Endereco = endereco;
        }


        public void AdicionarImagem(Imagem novaImagem)
        {
            ImagemBytes = novaImagem;
        }
        public void AtualizarImagem(Imagem novaImagem)
        {
            ImagemBytes = novaImagem;
        }

        public void RemoverImagem()
        {
            ImagemBytes = null;
        }

        public void AtualizarStatus(TipoDeStatus statusNovo)
        {
            if(Status == TipoDeStatus.EmAberto && statusNovo == TipoDeStatus.Finalizado)
            {
                throw new DomainException("Não pode atualizar status de Aberto para Finalizado");
            }
            if(Status == TipoDeStatus.EmAndamento && statusNovo == TipoDeStatus.EmAberto)
            {
                throw new DomainException("Não pode atualizar status de em Andamento para Aberto");
            }
            if(Status == TipoDeStatus.Finalizado)
            {
                throw new DomainException("O chamado já foi finalizado, não é possível mudar seu status");
            }
            if(statusNovo == Status)
            {
                throw new DomainException("Não pode atualizar para o mesmo estado atual");
            }

            Status = statusNovo;
        }


        private void Validacoes(string descricao,string cep, TipoDeProblema tipo, string numero)
        {
            if (string.IsNullOrEmpty(descricao) || descricao.Trim().Length < 10)
            {
               throw new DomainException("A descrição deve conter no mínimo 10 caracteres");
            }

            if(string.IsNullOrEmpty(cep) || cep.Trim().Length != 8)
            {
                throw new DomainException("O cep deve possuir 8 digítos");
            }

            if (!Enum.IsDefined(typeof(TipoDeProblema), tipo))
            {
                var tiposValidos = string.Join(", ", Enum.GetNames(typeof(TipoDeProblema)));
                throw new DomainException($"Tipo de problema inválido. Tipos válidos: {tiposValidos}");
            }

            if (!numero.All(char.IsDigit)) 
            {
                throw new DomainException("O número deve conter apenas dígitos");
            }
        }




    }
}
