using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Common.Validations;

namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.AlterarCategoriaDoProduto;

public sealed class AlterarCategoriaDoProdutoCommandHandler
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly ICategoriaRepository _categoriaRepository;

    public AlterarCategoriaDoProdutoCommandHandler(
        IProdutoRepository produtoRepository,
        ICategoriaRepository categoriaRepository)
    {
        _produtoRepository = produtoRepository;
        _categoriaRepository = categoriaRepository;
    }

    public async Task<AlterarCategoriaDoProdutoResultDto> HandleAsync(
        AlterarCategoriaDoProdutoCommand command,
        CancellationToken cancellationToken = default)
    {
        // 1️⃣ Recupera a categoria de destino
        var categoria = await _categoriaRepository.ObterPorIdAsync(
            command.NovaCategoriaId, cancellationToken)
            ?? throw new DomainException("Categoria não encontrada.");

        // 2️⃣ Regra de consistência entre agregados
        Guard.Against<DomainException>(
            !categoria.Ativa,
            "Não é possível associar um produto a uma categoria inativa.");

        // 3️⃣ Recupera o produto
        var produto = await _produtoRepository.ObterPorIdAsync(
            command.ProdutoId, cancellationToken)
            ?? throw new DomainException("Produto não encontrado.");

        // 4️⃣ Execução do comportamento no domínio
        produto.AlterarCategoria(command.NovaCategoriaId);

        // 5️⃣ Persistência do agregado
        await _produtoRepository.AtualizarAsync(produto, cancellationToken);

        return new AlterarCategoriaDoProdutoResultDto
        {
            ProdutoId = produto.Id,
            CategoriaId = produto.CategoriaId
        };
    }
}

