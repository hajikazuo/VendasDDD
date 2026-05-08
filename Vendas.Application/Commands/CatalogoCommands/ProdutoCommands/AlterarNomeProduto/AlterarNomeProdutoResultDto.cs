namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.AlterarNomeProduto;

public sealed class AlterarNomeProdutoResultDto
{
    public Guid ProdutoId { get; init; }
    public string Nome { get; init; } = default!;
}