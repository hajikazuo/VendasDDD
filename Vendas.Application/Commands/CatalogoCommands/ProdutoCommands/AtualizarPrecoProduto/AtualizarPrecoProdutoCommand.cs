namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.AtualizarPrecoProduto;

public sealed class AtualizarPrecoProdutoCommand
{
    public Guid ProdutoId { get; }
    public decimal NovoPreco { get; }

    public AtualizarPrecoProdutoCommand(Guid produtoId, decimal novoPreco)
    {
        ProdutoId = produtoId;
        NovoPreco = novoPreco;
    }
}
