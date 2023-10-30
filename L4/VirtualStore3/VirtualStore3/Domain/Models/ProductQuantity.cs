﻿using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace VirtualStore2.Domain.Models
{
    public record ProductQuantity
    {
        private static readonly Random random = new Random();
        public static int Stock = 12;

        public int Value { get; set; }

        public ProductQuantity(int value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidProductQuantityException("");
            }
        }

        public int ReturnQuantity()
        {
            return Value;
        }

        private static bool IsValid(int numericQuantity) => numericQuantity < Stock && numericQuantity > 0;


        public static Option<ProductQuantity> TryParse(string valueString)
        {
            if (int.TryParse(valueString, out int numericQuantity) && IsValid(numericQuantity))
            {
                return Some<ProductQuantity>(new(numericQuantity));
            }
            else
            {
                return None;
            }
        }
    }
}
