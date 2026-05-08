namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.InativarCategoria;

/// <summary>
/// Intenção de negócio: retirar uma categoria do uso ativo.
/// </summary>
public sealed class InativarCategoriaCommand
{
    public Guid CategoriaId { get; }

    public InativarCategoriaCommand(Guid categoriaId)
    {
        CategoriaId = categoriaId;
    }
}
