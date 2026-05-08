using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Catalogo.ValueObjects;
using Vendas.Domain.Common.Exceptions;

namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.AtualizarPrecoProduto;

public sealed class AtualizarPrecoProdutoCommandHandler
{
    private readonly IProdutoRepository _produtoRepository;

    public AtualizarPrecoProdutoCommandHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<AtualizarPrecoProdutoResultDto> HandleAsync(
        AtualizarPrecoProdutoCommand command,
        CancellationToken cancellationToken = default)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(command.ProdutoId, cancellationToken)
            ?? throw new DomainException("Produto não encontrado.");

        produto.AlterarPreco(new PrecoProduto(command.NovoPreco));

        await _produtoRepository.AtualizarAsync(produto, cancellationToken);

        return new AtualizarPrecoProdutoResultDto
        {
            ProdutoId = produto.Id,
            NovoPreco = produto.Preco.Valor
        };
    }
}

