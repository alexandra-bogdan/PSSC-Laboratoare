using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore.Domain
{
    public record PaymentInfo
    {
        
            public decimal Amount { get; set; }

            public PaymentInfo( decimal amount)
            {
                
                Amount = amount;
            }

        }

    
}