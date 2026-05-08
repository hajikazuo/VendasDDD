using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Catalogo;


namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.CriarCategoria;

/// <summary>
/// Application Service responsável por orquestrar o caso de uso CriarCategoria.
/// Nenhuma regra de negócio vive aqui – tudo é delegado ao domínio.
/// </summary>
public sealed class CriarCategoriaCommandHandler
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CriarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<CriarCategoriaResultDto> HandleAsync(
        CriarCategoriaCommand command,
        CancellationToken cancellationToken = default)
    {
        var categoria = new Categoria(
                        command.Nome,
                        command.Descricao);

        await _categoriaRepository.AdicionarAsync(categoria, cancellationToken);

        return new CriarCategoriaResultDto
        {
            CategoriaId = categoria.Id,
            Nome = categoria.Nome,
            Ativa = categoria.Ativa
        };
    }
}

