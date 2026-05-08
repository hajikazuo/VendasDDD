using Vendas.Application.Abstractions.Persistence;
using Vendas.Domain.Catalogo.ValueObjects;

namespace Vendas.Application.Commands.CatalogoCommands.ProdutoCommands.AlterarNomeProduto;

public sealed class AlterarNomeProdutoCommandHandler
{
    private readonly IProdutoRepository _produtoRepository;

    public AlterarNomeProdutoCommandHandler(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<AlterarNomeProdutoResultDto> HandleAsync(
        AlterarNomeProdutoCommand command,
        CancellationToken cancellationToken = default)
    {
        var produto = await _produtoRepository.ObterPorIdAsync(
                            command.ProdutoId,
                            cancellationToken)
                            ?? throw new 
                            InvalidOperationException("Produto não encontrado.");

        var novoNome = new NomeProduto(command.NovoNome);

        produto.AlterarNome(novoNome);

        await _produtoRepository.AtualizarAsync(produto, cancellationToken);

        return new AlterarNomeProdutoResultDto
        {
            ProdutoId = produto.Id,
            Nome = produto.Nome.Valor
        };
    }
}
