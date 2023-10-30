using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualStore2.Domain;

namespace VirtualStore2.Domain.Models
{
    public record ProcessOrderCommand
    {

        public ProcessOrderCommand(IReadOnlyCollection<UnvalidatedProduct> inputShoppingCart)
        {
            InputShoppingCart = inputShoppingCart;
        }

        public IReadOnlyCollection<UnvalidatedProduct> InputShoppingCart { get; }
    }
}
