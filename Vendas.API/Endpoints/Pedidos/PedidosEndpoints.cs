using Vendas.Application.Commands.PedidosCommands.AdicionarItemAoPedido;
using Vendas.Application.Commands.PedidosCommands.CriarPedido;
using Vendas.Application.Commands.PedidosCommands.IniciarPagamento;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEmSeparacao;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEntregue;
using Vendas.Application.Commands.PedidosCommands.MarcarPedidoComoEnviado;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos.Enums;
using Vendas.Infra.Fakes;

namespace Vendas.API.Endpoints.Pedidos
{
    public static class PedidosEndpoints
    {
        public static WebApplication MapPedidosEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/pedidos")
                .WithTags("Pedidos")
                .WithOpenApi();

            group.MapGet("/fake-ids", () => Results.Ok(new
            {
                clientes = new[]
                {
                    new
                    {
                        clienteId = Guid.Parse("22222222-0000-0000-0000-000000000001"),
                        enderecos = new[]
                        {
                             new { enderecoId = Guid.Parse("33333333-0000-0000-0000-000000000001"),
                                descricao = "Avenida Paulista, 1578 - Conj 42, São Paulo"},

                             new { enderecoId = Guid.Parse("33333333-0000-0000-0000-000000000002"),
                                descricao = "Rua das Flores 300, Vila Olímpia, São Paulo"}
                        }
                    },
                    new
                    {
                        clienteId = Guid.Parse("22222222-0000-0000-0000-000000000002"),
                        enderecos = new[]
                        {
                             new { enderecoId = Guid.Parse("33333333-0000-0000-0000-000000000003"),
                                descricao = "Av. do Contorno 8000, Santo Agostinho, Belo Horizonte"}
                        }
                    }
                },
                produtos = new[]
                {
                    new {   produtoId = Guid.Parse("11111111-0000-0000-0000-000000000001"),
                        descricao = "Notebook Gamer RTX 4060 - R$ 8.500,00" },
                    new {   produtoId = Guid.Parse("11111111-0000-0000-0000-000000000002"),
                        descricao = "Mouse Sem Fio Logitech MX Master - R$ 450,00" },
                    new {   produtoId = Guid.Parse("11111111-0000-0000-0000-000000000003"),
                        descricao = "Teclado Mecânico Keychron K8 - R$ 680,00" },
                    new {   produtoId = Guid.Parse("11111111-0000-0000-0000-000000000004"),
                        descricao = "Monitor Ultrawide 34 Polegadas - R$ 3.200,00" },
                }
            })).WithSummary("Exibe os IDs dos dados disponíveis nos Fakes para usar nos testes");

            group.MapGet("/", async (FakePedidoRepository repo, CancellationToken ct) =>
            {
                var pedidos = await repo.ListarTodosAsync(ct);
                var resultado = pedidos.Select(p => new
                {
                    p.Id,
                    p.NumeroPedido,
                    p.ClienteId,
                    p.ValorTotal,
                    Status = p.StatusPedido.ToString(),
                    p.DataCriacao,
                    TotalItens = p.Itens.Count,
                });
                return Results.Ok(resultado);
            }).WithSummary("Lista todos os pedidos em memória");

            group.MapGet("/{id:guid}", async (Guid id, FakePedidoRepository repo, CancellationToken ct) =>
            {
                var pedido = await repo.ObterPorIdAsync(id, ct);
                if (pedido is null) return Results.NotFound();

                var resultado = new
                {
                    pedido.Id,
                    pedido.NumeroPedido,
                    pedido.ClienteId,
                    pedido.ValorTotal,
                    Status = pedido.StatusPedido.ToString(),
                    pedido.DataCriacao,
                    pedido.DataAtualizacao,
                    Endereco = new
                    {
                        pedido.EnderecoEntrega.Logradouro,
                        pedido.EnderecoEntrega.Numero,
                        pedido.EnderecoEntrega.Bairro,
                        pedido.EnderecoEntrega.Cidade,
                        pedido.EnderecoEntrega.Estado,
                        pedido.EnderecoEntrega.Cep
                    },
                    Itens = pedido.Itens.Select(i => new
                    {
                        i.Id,
                        i.ProdutoId,
                        i.NomeProduto,
                        i.PrecoUnitario,
                        i.Quantidade,
                        i.ValorTotal
                    }),
                    Pagamentos = pedido.Pagamentos.Select(pg => new
                    {
                        pg.Id,
                        Metodo = pg.MetodoPagamento.ToString(),
                        Status = pg.StatusPagamento.ToString(),
                        pg.Valor,
                        pg.CodigoTransacao,
                        pg.DataPagamento
                    })
                };
                return Results.Ok(resultado);
            }).WithSummary("Retorna detalhes completos de um pedido");

            group.MapPost("/", async (CriarPedidoRequest req, CriarPedidoCommandHandler handler, CancellationToken ct) =>
            {
                try
                {
                    var command = new CriarPedidoCommand(req.ClienteId, req.EnderecoId);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Created($"/pedidos/{result.PedidoId}", result);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { erro = ex });
                }
                catch (DomainException ex)
                {
                    return Results.UnprocessableEntity(new { erro = ex });
                }
            }).WithSummary("Cria um novo pedido");

            group.MapPost("/{id:guid}/itens", async (Guid id, AdicionarItemRequest req, AdicionarItemAoPedidoCommandHandler handler, CancellationToken ct) =>
            {
                try
                {
                    var command = new AdicionarItemAoPedidoCommand(id, req.ProdutoId, req.Quantidade);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { erro = ex });
                }
                catch (DomainException ex)
                {
                    return Results.UnprocessableEntity(new { erro = ex });
                }
            }).WithSummary("Adiciona um item a um pedido existente");

            group.MapPost("/{id:guid}/pagamento", async (Guid id, IniciarPagamentoRequest req, IniciarPagamentoCommandHandler handler, CancellationToken ct) =>
            {
                try
                {
                    var metodo = (MetodoPagamento)req.MetodoPagamento;
                    var command = new IniciarPagamentoCommand(id, metodo);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                }
                catch (InvalidOperationException ex)
                {
                    return Results.NotFound(new { erro = ex });
                }
                catch (DomainException ex)
                {
                    return Results.UnprocessableEntity(new { erro = ex });
                }
            }).WithSummary("Inicia o pagamento do pedido");

            group.MapPost("/{id:guid}/pagamento/confirmacao", async (Guid id, ConfirmarPagamentoRequest req, FakePedidoRepository repo, CancellationToken ct) =>
            {
                try
                {
                    var pedido = await repo.ObterPorIdAsync(id, ct);
                    if (pedido is null) return Results.NotFound();

                    var pagamento = pedido.Pagamentos.FirstOrDefault(p => p.Id == req.PagamentoId);

                    if (pagamento is null) return Results.NotFound(new { erro = "Pagamento não encontrado" });

                    pagamento.GerarCodigoTransacaoLocal();
                    pagamento.ConfirmarPagamento();

                    pedido.HandlePagamentoAprovado(pagamento.Id);

                    await repo.AtualizarAsync(pedido, ct);

                    return Results.Ok(new
                    {
                        PedidoId = pedido.Id,
                        PagamentoId = pagamento.Id,
                        StatusPedido = pedido.StatusPedido.ToString(),
                        StatusPagamento = pagamento.StatusPagamento.ToString(),
                        CodigoTransacao = pagamento.CodigoTransacao
                    });
                }
                catch (DomainException ex)
                {
                    return Results.UnprocessableEntity(new { erro = ex });
                }
            }).WithSummary("Confirma o pagamento do pedido");

            group.MapPost("/{id:guid}/separacao", async (Guid id, MarcarPedidoComoEmSeparacaoCommandHandler handler, CancellationToken ct) =>
            {
                try
                {
                    var command = new MarcarPedidoComoEmSeparacaoCommand(id);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                }
                catch (DomainException ex)
                {
                    return Results.UnprocessableEntity(new { erro = ex });
                }
            }).WithSummary("Marca o pedido em separação (PagamentoConfirmado -> EmSeparacao)");

            group.MapPost("/{id:guid}/enviado", async (Guid id, MarcarPedidoComoEnviadoCommandHandler handler, CancellationToken ct) =>
            {
                try
                {
                    var command = new MarcarPedidoComoEnviadoCommand(id);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                }
                catch (DomainException ex)
                {
                    return Results.UnprocessableEntity(new { erro = ex });
                }
            }).WithSummary("Marca o pedido como enviado (PagamentoConfirmado -> Enviado)");

            group.MapPost("/{id:guid}/entregue", async (Guid id, MarcarPedidoComoEntregueCommandHandler handler, CancellationToken ct) =>
            {
                try
                {
                    var command = new MarcarPedidoComoEntregueCommand(id);
                    var result = await handler.HandleAsync(command, ct);
                    return Results.Ok(result);
                }
                catch (DomainException ex)
                {
                    return Results.UnprocessableEntity(new { erro = ex });
                }
            }).WithSummary("Marca o pedido como entregue");

            return app;
        }
    }
}
