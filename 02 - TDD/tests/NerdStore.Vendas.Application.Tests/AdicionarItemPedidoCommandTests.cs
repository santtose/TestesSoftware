﻿using NerdStore.Vendas.Application.Coomands;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Application.Tests
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar Item Command Válido")]
        [Trait("Categoria", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_CommandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(),
                Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Item Command Inválido")]
        [Trait("Categoria", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_CommandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.Empty,
                Guid.Empty, "", 0, 0);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.IdClienteErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.IdProdutoErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.NomeErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.QtdMinErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.ValorErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Adicionar Item Command unidades acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_QuantidadeUnidadesSuperiorAoPermitido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(),
                Guid.NewGuid(), "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.QtdMaxErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}
