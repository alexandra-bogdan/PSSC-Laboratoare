using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VirtualStore.Domain.ShoppingCart;

namespace VirtualStore.Domain
{
    class Account
    {
        public Client? Client { get; set; }
        public IShoppingCart Cart { get; set; }
        public PaymentInfo PaymentInfo { get; set; }
        public Account(Client client, IShoppingCart cart, PaymentInfo paymentInfo)
        {
            Client = client;
            Cart = cart;
            PaymentInfo = paymentInfo;
        }
    }
}
