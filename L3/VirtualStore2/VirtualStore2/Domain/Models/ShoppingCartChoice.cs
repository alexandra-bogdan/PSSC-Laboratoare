﻿using CSharp.Choices;
using VirtualStore2.Domain;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static VirtualStore2.Domain.Models.ShoppingCartChoice;
using VirtualStore2.Domain.Models;

namespace VirtualStore2.Domain.Models
{
    [AsChoice]
    public static partial class ShoppingCartChoice
    {
        public interface IShoppingCart { }
        public record EmptyShoppingCart : IShoppingCart
        {
            public ShoppingCart EmptyCart { get; }
            public EmptyShoppingCart(ShoppingCart emptyCart)
            {
                EmptyCart = emptyCart;
            }
        }

        public record UnvalidatedShoppingCart : IShoppingCart
        {
            public IReadOnlyCollection<UnvalidatedProduct> ProductsList { get; }
            public UnvalidatedShoppingCart(IReadOnlyCollection<UnvalidatedProduct> productsList)
            {
                ProductsList = productsList;
            }
        }

        public record InvalidatedShoppingCart : IShoppingCart
        {
            public IReadOnlyCollection<UnvalidatedProduct> ProductsList { get; }
            public string Reason { get; }
            public InvalidatedShoppingCart(IReadOnlyCollection<UnvalidatedProduct> productsList, string reason)
            {
                ProductsList = productsList;
                Reason = reason;
            }
        }

        public record ValidatedShoppingCart : IShoppingCart
        {
            public IReadOnlyCollection<ValidatedProduct> ProductsList { get; }
            public ValidatedShoppingCart(IReadOnlyCollection<ValidatedProduct> productsList)
            {
                ProductsList = productsList;
            }
        }

        public record CalculatedShoppingCart : IShoppingCart
        {
            public IReadOnlyCollection<PriceCalculated> ProductsList { get; }
            public CalculatedShoppingCart(IReadOnlyCollection<PriceCalculated> productsList)
            {
                ProductsList = productsList;
            }
        }

        public record PaidShoppingCart : IShoppingCart
        {
            public IReadOnlyCollection<PriceCalculated> ProductsList { get; }
            public string Csv { get; }
            public DateTime PayDate { get; }
            public PaidShoppingCart(IReadOnlyCollection<PriceCalculated> productsList, string csv, DateTime payDate)
            {
                ProductsList = productsList;
                Csv = csv;
                PayDate = payDate;
            }
        }
    }
}