namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.AtivarCategoria;

/// <summary>
/// Intenção explícita de tornar uma categoria disponível no catálogo.
/// </summary>
public sealed class AtivarCategoriaCommand
{
    public Guid CategoriaId { get; }

    public AtivarCategoriaCommand(Guid categoriaId)
    {
        CategoriaId = categoriaId;
    }
}
