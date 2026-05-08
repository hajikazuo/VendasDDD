namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.InativarProduto;

public sealed class InativarProdutoCommand
{
    public Guid ProdutoId { get; }

    public InativarProdutoCommand(Guid produtoId)
    {
        ProdutoId = produtoId;
    }
}
