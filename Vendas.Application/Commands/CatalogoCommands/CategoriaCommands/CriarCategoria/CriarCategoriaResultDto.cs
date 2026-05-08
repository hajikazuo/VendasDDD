namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.CriarCategoria;

/// <summary>
/// Resultado observável da criação de uma categoria.
/// DTO protege o domínio de exposições indevidas.
/// </summary>
public sealed class CriarCategoriaResultDto
{
    public Guid CategoriaId { get; init; }
    public string Nome { get; init; } = string.Empty;
    public bool Ativa { get; init; }
}
