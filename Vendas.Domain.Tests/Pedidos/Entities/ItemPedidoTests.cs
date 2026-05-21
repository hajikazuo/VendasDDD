using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Text;
using Vendas.Domain.Common.Base;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos;

namespace Vendas.Domain.Tests.Pedidos.Entities
{
    public class ItemPedidoTests
    {
        private static ItemPedido CriarItemValido(decimal preco = 100m, int quantidade = 2)
        {
            return new ItemPedido(Guid.NewGuid(), "Produto teste", preco, quantidade);
        }

        [Fact(DisplayName = "Deve criar ItemPedido com sucesso quando dados válidos")]
        public void Criar_DeveRetornarItemPedido_QuandoDadosValidos()
        {
            var produtoId = Guid.NewGuid();
            var nomeProduto = "Teclado mecânico";
            var precoUnitario = 250m;
            var quantidade = 2;

            var item = new ItemPedido(produtoId, nomeProduto, precoUnitario, quantidade);

            item.ProdutoId.Should().Be(produtoId);  
            item.NomeProduto.Should().Be(nomeProduto);  
            item.PrecoUnitario.Should().Be(precoUnitario);
            item.Quantidade.Should().Be(quantidade);
            item.DescontoAplicado.Should().Be(0);
            item.ValorTotal.Should().Be(500m);
        }

        [Theory(DisplayName = "Deve lançar DomainException quando parâmetros inválidos")]
        [InlineData("", "Produto A", 10, 1, "ProdutoId inválido.")]
        [InlineData("guid", "", 10, 1, "O nome do produto é obrigatório.")]
        [InlineData("guid", "Produto B", 0, 1, "O preço unitário deve ser maior que zero.")]
        [InlineData("guid", "Produto C", 10, 0, "A quantidade deve ser maior que zero.")]
        public void Criar_DeveLancarExcecao_QuandoParametrosInvalidos(string tipo, string nomeProduto, decimal preco, int qtd, string mensagem)
        {
            //Arrange
            var produtoId = tipo == "guid" ? Guid.NewGuid() : Guid.Empty;

            //Act
            Action act = () => new ItemPedido(produtoId, nomeProduto, preco, qtd);

            //Assert
            act.Should().Throw<DomainException>().WithMessage(mensagem);
        }

        [Fact(DisplayName = "Deve aplicar desconto com sucesso quando valor válido")]
        public void AplicarDesconto_DeveAplicarComSucesso_QuandoValorValido()
        {
            //Arrange
            var item = CriarItemValido(preco: 200m, quantidade: 2);

            //Act
            item.AplicarDesconto(50m);

            //Assert
            item.DescontoAplicado.Should().Be(50m);
            item.ValorTotal.Should().Be(350m);
            item.DataAtualizacao.Should().NotBeNull();
        }

        [Theory(DisplayName = "Deve lançar exceçõ ao aplicar desconto inválido")]
        [InlineData(-10, "Desconto não pode ser negativo.")]
        [InlineData(1000, "Desconto não pode exceder o valor do item.")]
        public void AplicarDesconto_DeveLancarExcecao_QuandoValorInvalido(decimal desconto, string mensagem)
        {
            //Arrange
            var item = CriarItemValido(preco: 100m, quantidade: 2);

            //Act
            Action act = () => item.AplicarDesconto(desconto);

            //Assert
            act.Should().Throw<DomainException>().WithMessage(mensagem);
        }


        [Fact(DisplayName = "Deve adicionar unidades com sucesso quando valor válido")]
        public void AdicionarUnidades_DeveAdicionarComSucesso_QuandoValorValido()
        {
            //Arrange
            var item = CriarItemValido(preco: 50m, quantidade: 2);

            //Act
            item.AdicionarUnidades(3);

            //Assert
            item.Quantidade.Should().Be(5);
            item.ValorTotal.Should().Be(250m);
            item.DataAtualizacao.Should().NotBeNull();
        }

        [Fact(DisplayName = "Deve lançar exceção ao adicionar unidades inválidas")]
        public void AdicionarUnidades_DeveLancarExcecao_QuandoValorInvalido()
        {
            //Arrange
            var item = CriarItemValido();

            //Act
            Action act = () => item.AdicionarUnidades(0);

            //Assert
            act.Should().Throw<DomainException>().WithMessage("Deve-se adicionar pelo menos uma unidade.");
        }

        [Fact(DisplayName = "Deve remover unidades com sucesso quando valor válido")]
        public void RemoverUnidades_DeveRemoverComSucesso_QuandoValorValido()
        {
            //Arrange
            var item = CriarItemValido(preco: 100m, quantidade: 5);

            //Act
            item.RemoverUnidades(3);

            //Assert
            item.Quantidade.Should().Be(2);
            item.ValorTotal.Should().Be(200m);
            item.DataAtualizacao.Should().NotBeNull();
        }

        [Fact(DisplayName = "Deve lançar exceção ao remover unidades e zerar quantidade")]
        public void RemoverUnidades_DeveLancarExcecao_QuandoQuantidadeZerar()
        {
            //Arrange
            var item = CriarItemValido(preco: 100m, quantidade: 2);

            //Act
            Action act = () => item.RemoverUnidades(2);

            //Assert
            act.Should().Throw<DomainException>().WithMessage("Um item de pedido não pode ter quantidade zero. Use o método da classe pedido para removê-lo.");
        }

        [Fact(DisplayName = "Dois items com o mesmo ID devem ser considerados iguais")]
        public void Equals_DeveRetornarTrue_QuandoMesmoID()
        {
            //Arrange
            var item1 = CriarItemValido();
            var item2 = CriarItemValido();

            // Forçar mesmo Id por reflexão
            typeof(Entity).GetProperty("Id")!.SetValue(item2, item1.Id);

            //Assert
            (item1 == item2).Should().BeTrue();
            item1.Equals(item2).Should().BeTrue();
        }
    }
}
