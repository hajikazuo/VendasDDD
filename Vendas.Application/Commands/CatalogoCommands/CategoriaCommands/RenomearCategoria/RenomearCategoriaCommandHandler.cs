using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Exceptions;

namespace Vendas.Application.Commands.CatalogoCommands.CategoriaCommands.RenomearCategoria;

/// <summary>
/// Orquestra a alteração do nome da categoria.
/// A validação semântica do nome ocorre no domínio.
/// </summary>
public sealed class RenomearCategoriaCommandHandler
{
    private readonly ICategoriaRepository _categoriaRepository;

    public RenomearCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<RenomearCategoriaResultDto> HandleAsync(
        RenomearCategoriaCommand command,
        CancellationToken cancellationToken = default)
    {
        var categoria = await _categoriaRepository.ObterPorIdAsync(command.CategoriaId, cancellationToken)
                        ?? throw new DomainException("Categoria não encontrada.");

        categoria.AlterarNome(command.NovoNome);

        await _categoriaRepository.AtualizarAsync(categoria, cancellationToken);

        return new RenomearCategoriaResultDto
        {
            CategoriaId = categoria.Id,
            NomeAtual = categoria.Nome
        };
    }
}
