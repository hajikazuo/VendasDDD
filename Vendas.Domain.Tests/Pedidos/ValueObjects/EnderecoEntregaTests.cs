using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Vendas.Domain.Common.Exceptions;
using Vendas.Domain.Pedidos.ValueObjects;

namespace Vendas.Domain.Tests.Pedidos.ValueObjects
{
    public class EnderecoEntregaTests
    {
        [Fact(DisplayName = "Deve criar um endereço válido quando os dados forem válidos")]
        public void Criar_DeveRetornarEnderecoValido_QuandoDadosForemValidos()
        {
            //Arrange
            var cep = "12345-678";
            var logradouro = "Rua das Flores";
            var complemento = "Apto 101";
            var bairro = "Centro";
            var estado = "SP";
            var cidade = "São Paulo";
            var pais = "Brasil";

            //Act
            var endereco = EnderecoEntrega.Criar(cep, logradouro, complemento, bairro, estado, cidade, pais);

            //Assert
            endereco.Should().NotBeNull();
            endereco.Cep.Should().Be(cep);
            endereco.Logradouro.Should().Be(logradouro);
            endereco.Complemento.Should().Be(complemento);
            endereco.FormatarEndereco().Should().Contain("Rua das Flores");
        }

        [Theory(DisplayName = "Deve lançar exceção quando o CEP for inválido")]
        [InlineData("12345678")]
        [InlineData("12-34-67")]
        [InlineData("ABCDE-123")]
        public void Criar_DeveLancarDomainException_QuandoCepForInvalido(string cepInvalido)
        {
            //Arrange
            var logradouro = "Rua das Flores";
            var complemento = "Apto 101";
            var bairro = "Centro";
            var estado = "SP";
            var cidade = "São Paulo";
            var pais = "Brasil";
            //Act
            Action act = () => EnderecoEntrega.Criar(cepInvalido, logradouro, complemento, bairro, estado, cidade, pais);
            //Assert
            act.Should().Throw<DomainException>()
                .WithMessage("CEP deve estar no formato 00000-000.");
        }

        [Fact(DisplayName = "Dois EnderecosEntrega com os mesmos dados devem ser considerados iguais (Value Object)")]
        public void EnderecosDevemSerIguais_QuandoPossuemMesmosValores()
        {
            //Arrange 
            var endereco1 = EnderecoEntrega.Criar("12345-678", "Rua das Flores", "Apto 101", "Centro", "SP", "São Paulo", "Brasil");
            var endereco2 = EnderecoEntrega.Criar("12345-678", "Rua das Flores", "Apto 101", "Centro", "SP", "São Paulo", "Brasil");

            //Assert
            endereco1.Should().Be(endereco2);
            (endereco1 == endereco2).Should().BeTrue();
        }

        [Fact(DisplayName = "EnderecosEntrega devem ser diferentes quando algum campo for diferente")]
        public void EnderecosDevemSerDiferentes_QuandoAlgumCampoForDiferente()
        {
            //Arrange 
            var endereco1 = EnderecoEntrega.Criar("12345-678", "Rua X", "Apto 101", "Centro", "SP", "São Paulo", "Brasil");
            var endereco2 = EnderecoEntrega.Criar("12345-678", "Rua Y", "Apto 101", "Centro", "SP", "São Paulo", "Brasil");

            //Assert
            endereco1.Should().NotBe(endereco2);
        }

        [Fact(DisplayName = "EnderecoEntrega deve ser imutável após criação")]
        public void EnderecoDeveSerImutavel_AposCriacao()
        {
            //Arrange
            var endereco = EnderecoEntrega.Criar("12345-678", "Rua das Flores", "Apto 101", "Centro", "SP", "São Paulo", "Brasil");

            Action act = () =>
            {

            };

            //Assert
            endereco.GetType().GetProperties()
                .All(p => p.SetMethod == null || p.SetMethod.IsPrivate)
                .Should().BeTrue("as propriedades do VO devem ser imutáveis");
        }

        [Theory(DisplayName = "Deve lançar DomainException quando campos obrigatórios forem nulos ou vazios")]
        [InlineData(null, "Rua das Flores", "Apto 101", "Centro", "SP", "São Paulo", "Brasil", "cep")]
        [InlineData("12345-678", null, "Apto 101", "Centro", "SP", "São Paulo", "Brasil", "logradouro")]
        [InlineData("12345-678", "Rua das Flores", "Apto 101", null, "SP", "São Paulo", "Brasil", "bairro")]
        public void Criar_DeveLancarDomainException_QuandoCamposObrigatoriosNulosOuVazios(string cep, string logradouro, string complemento, string bairro, string estado, string cidade, string pais, string campoEsperado)
        {
            //Act
            Action act = () => EnderecoEntrega.Criar(cep, logradouro, complemento, bairro, estado, cidade, pais);

            //Assert
            act.Should().Throw<DomainException>()
                .WithMessage($"{campoEsperado} não pode ser nulo ou vazio.");
        }
    }
}
