namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.AlterarNomeProduto;

public sealed class AlterarNomeProdutoCommand
{
    public Guid ProdutoId { get; }
    public string NovoNome { get; }

    public AlterarNomeProdutoCommand(Guid produtoId, string novoNome)
    {
        ProdutoId = produtoId;
        NovoNome = novoNome;
    }
}