using System;
using System.Collections.Generic;
using System.Text;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Common.Validations;

namespace Vendas.Domain.Pedidos.Integration.Catalogo
{
    public sealed class CatalogoAcl
    {
        private readonly ICatalogoGateway _gateway;
        
        public(string nomeProduto, decimal precoUnitario) TraduzirProduto(ProdutoDto dto)
        {
            return (dto.Nome, dto.Preco); 
        }
    }
}
