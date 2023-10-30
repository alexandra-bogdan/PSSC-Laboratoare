using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VirtualStore.Domain;
using static VirtualStore.Domain.ShoppingCart;
using CSharp.Choices;

namespace VirtualStore
{
    class Program
    {
        static void Main(string[] args)
        {
            var account = ReadCartContent();
            UnvalidatedCart unvalidatedCart = (UnvalidatedCart)account.Cart;
            IShoppingCart result = ValidateCart(unvalidatedCart);
            result.Match(
                whenEmptyCart: emptyCart =>
                {
                    Console.WriteLine("The cart is empty!");
                    return emptyCart;
                },
                whenUnvalidatedCart: unvalidatedCart =>
                {
                    Console.WriteLine("The cart is unvalidated!");
                    return unvalidatedCart;
                },
                whenInvalidatedCart: invalidatedCart =>
                {
                    Console.WriteLine("The cart is invalidated!");
                    return invalidatedCart;
                },
                whenValidatedCart: validatedCart =>
                {
                    Console.WriteLine("The cart is validated!");
                    return PaidCart(validatedCart, account.PaymentInfo);
                },
                whenPaidCart: paidCart =>
                {
                    Console.WriteLine("The cart is paid!");
                    return paidCart;
                }
            ); 
        }

        private static Account ReadCartContent()
        {
            var quantityOfProducts = 0;
            List<UnvalidatedProducts> listOfProducts = new();
            var clientName = ReadValue("Client name: ");
            while (string.IsNullOrEmpty(clientName))
            {
                Console.WriteLine("Client name cannot be empty!");
                clientName = ReadValue("Please enter client name: ");
            }
            var clientAddress = ReadValue("Client address: ");
            while (string.IsNullOrEmpty(clientAddress))
            {
                Console.WriteLine("Client address cannot be empty!");
                clientAddress = ReadValue("Please enter client address: ");
            }
            Client client = new(clientName, clientAddress);
            
            do
            {
                var codeProduct = ReadValue("Code product (6 digits): ");
                if (string.IsNullOrEmpty(codeProduct))
                {
                    break;
                }
                var quantityProduct = ReadValue("Quantity of product: ");
                int quantityProductInt = 0;
                if (string.IsNullOrEmpty(codeProduct))
                {
                    break;
                }
                if (!(int.TryParse(quantityProduct, out quantityProductInt)))
                {
                    break;
                }
                
                Product product = new Product(codeProduct, quantityProductInt);
                listOfProducts.Add(new(product));
            } while (true);
            decimal payAmount = 0;
            foreach (var p in listOfProducts)
            {
                payAmount = payAmount + p.product.Price;

            }
            PaymentInfo paymentInfo = new(payAmount);
            UnvalidatedCart shoppingCart = new UnvalidatedCart(listOfProducts);
            Account account = new Account(client, shoppingCart, paymentInfo);
            return account;
        }

        private static IShoppingCart ValidateCart(UnvalidatedCart unvalidatedCart)
        {
            if (unvalidatedCart.ProductsList.Count == 0)
            {
                return new EmptyCart();
            }
            else
            {
                Regex ValidPattern = new("^[0-9]{6}$");
                foreach (var product in unvalidatedCart.ProductsList)
                {
                    if (!(ValidPattern.IsMatch(product.product.Code)))
                    {
                        return new InvalidatedCart(new List<UnvalidatedProducts>(), "Invalid code!");
                    }
                }
                return new ValidatedCart(new List<ValidatedProducts>());
            }
        }

        private static IShoppingCart PaidCart(ValidatedCart validatedCart, PaymentInfo paymentInfo)
        {
            return new PaidCart(new List<ValidatedProducts>(), paymentInfo);
        }
        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
