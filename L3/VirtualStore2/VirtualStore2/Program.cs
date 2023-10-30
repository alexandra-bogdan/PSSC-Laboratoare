using VirtualStore2.Domain;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static VirtualStore2.Domain.Models.ShoppingCartChoice;
using VirtualStore2.Domain.Models;

namespace VirtualStoreLab3
{
    class Program
    {
        private static readonly Regex ValidPatternName = new("^[a-zA-Z]{3,20}$");
        private static readonly Regex ValidPatternAddress = new("^[a-zA-Z][a-zA-Z0-9]{5,50}$");
        static void Main(string[] args)
        {
            var clientName = ReadValue("Client name (It must have at least 3 characters): ");
            while (string.IsNullOrEmpty(clientName) || !ValidPatternName.IsMatch(clientName))
            {
                Console.WriteLine("Client name is empty or wrongly formatted!");
                clientName = ReadValue("Please enter client name: ");
            }
            var clientAddress = ReadValue("Client address (It must have at least 5 characters): ");
            while (string.IsNullOrEmpty(clientAddress) || !ValidPatternAddress.IsMatch(clientAddress))
            {
                Console.WriteLine("Client address is empty or wrongly formatted!!");
                clientAddress = ReadValue("Please enter client address: ");
            }
            Client client = new(clientName, clientAddress);

            ShoppingCart newShoppingCart = new()
            {
                Client = client,
                Products = new List<Product>()
            };

            var listOfProducts = ReadListOfProducts().ToArray();
            ProcessOrderCommand command = new(listOfProducts);
            PayShoppingCartWorkflow workflow = new PayShoppingCartWorkflow();
            var result = workflow.Execute(command, (productCode) => true, (quantity) => true);

            result.Match(
                    whenOrderProcessingFailedEvent: @event =>
                    {
                        Console.WriteLine($"Failed payment: {@event.Reason}");
                        return @event;
                    },
                    whenOrderProcessingSucceededEvent: @event =>
                    {
                        Console.WriteLine($"Successful payment: ");
                        Console.WriteLine(@event.Csv);
                        return @event;
                    }
                );
        }

        private static List<UnvalidatedProduct> ReadListOfProducts()
        {
            List<UnvalidatedProduct> listOfProducts = new();
            do
            {
                var productCode = ReadValue("Code product (6 digits): ");
                if (string.IsNullOrEmpty(productCode))
                {
                    break;
                }

                var productQuantity = ReadValue("Quantity of product: ");
                if (string.IsNullOrEmpty(productQuantity))
                {
                    break;
                }

                listOfProducts.Add(new(productCode, productQuantity));
            } while (true);
            return listOfProducts;
        }

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}
