using System;
using System.Collections.Generic;
using System.Text;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Common.Validations;
using Vendas.Domain.Pedidos.ValueObjects;

namespace Vendas.Domain.Pedidos.Integration.Clientes
{
    public sealed class ClientesACL
    {
        public EnderecoEntrega TraduzirEndereco(EnderecoDto dto)
        {
            return EnderecoEntrega.Criar(
                dto.Cep,
                dto.Logradouro,
                dto.Complemento,
                dto.Numero,
                dto.Bairro,
                dto.Estado,
                dto.Cidade,
                dto.Pais
            );
        }
    }
}
