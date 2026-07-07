using Vendas.Application.Abstractions.Persistence;

namespace Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEmSeparacao
{
    public sealed class MarcarPedidoComoEmSeparacaoCommandHandler
    {
        private readonly IPedidoRepository _pedidoRepository;

        public MarcarPedidoComoEmSeparacaoCommandHandler(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<MarcarPedidoComoEmSeparacaoResultDto>HandleAsync(MarcarPedidoComoEmSeparacaoCommand command, CancellationToken cancellationToken = default)
        {
            var pedido = await _pedidoRepository.ObterPorIdAsync(command.PedidoId) ?? throw new InvalidOperationException("Pedido não encontrado.");

            pedido.MarcarComoEmSeparacao();
            await _pedidoRepository.AtualizarAsync(pedido, cancellationToken);

            return new MarcarPedidoComoEmSeparacaoResultDto
            {
                PedidoId = pedido.Id,
                StatusPedido = pedido.StatusPedido.ToString()
            };
        }
    }
}
