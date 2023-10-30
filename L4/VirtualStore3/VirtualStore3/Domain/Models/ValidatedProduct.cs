using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore2.Domain.Models
{
    public record ValidatedProduct(ProductCode Code, ProductQuantity Quantity, ProductPrice Price);
}
