using VirtualStore2.Domain;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static VirtualStore2.Domain.Models.ShoppingCartChoice;
using VirtualStore2.Domain.Models;

namespace VirtualStore2.Domain.Models
{
    public record ShoppingCart
    {
        public Client? Client { get; set; }
        public List<Product>? Products { get; set; }

    }
}