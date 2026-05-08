namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.RenomearCategoria;

/// <summary>
/// Intenção de negócio: alterar o nome de uma categoria existente.
/// </summary>
public sealed class RenomearCategoriaCommand
{
    public Guid CategoriaId { get; }
    public string NovoNome { get; }

    public RenomearCategoriaCommand(Guid categoriaId, string novoNome)
    {
        CategoriaId = categoriaId;
        NovoNome = novoNome;
    }
}