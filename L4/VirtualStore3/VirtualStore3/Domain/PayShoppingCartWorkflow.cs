using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VirtualStore2.Domain.Models.OrderProcessingEvent;
using static VirtualStore2.Domain.Models.ShoppingCartChoice;
using static VirtualStore2.Domain.ShoppingCartOperation;
using VirtualStore2.Domain.Models;
using LanguageExt;

namespace VirtualStore2.Domain
{
    public class PayShoppingCartWorkflow
    {
        public async Task<IOrderProcessingEvent> ExecuteAsync(ProcessOrderCommand command, Func<ProductCode, TryAsync<bool>> checkProductExists, Func<ProductQuantity, TryAsync<bool>> checkIfEnoughStock)
        {
            UnvalidatedShoppingCart unvalidatedCart = new UnvalidatedShoppingCart(command.InputShoppingCart);
            IShoppingCart cart = await ValidateShoppingCart(checkProductExists, checkIfEnoughStock, unvalidatedCart);
            cart = CalculateTotalPrice(cart);
            cart = PayShoppingCart(cart);

            return cart.Match(
                    whenEmptyShoppingCart: emptyCart => new OrderProcessingFailedEvent("Unexpected empty state") as IOrderProcessingEvent,
                    whenUnvalidatedShoppingCart: unvalidatedCart => new OrderProcessingFailedEvent("Unexpected unvalidated state"),
                    whenInvalidatedShoppingCart: invalidCart => new OrderProcessingFailedEvent(invalidCart.Reason),
                    whenValidatedShoppingCart: validatedCart => new OrderProcessingFailedEvent("Unexpected validated state"),
                    whenCalculatedShoppingCart: calculatedCart => new OrderProcessingFailedEvent("Unexpected calculated state"),
                    whenPaidShoppingCart: paidCart => new OrderProcessingSucceededEvent(paidCart.Csv, paidCart.PayDate)
                );
        }
    }
}
