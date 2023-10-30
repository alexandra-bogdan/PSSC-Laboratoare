using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore.Domain
{
    public record Client
    {
        public string? Name { get; set; }
        public string? Adress { get; set; }

        public Client(string? name, string? adress)
        {
            Name = name;
            Adress = adress;

        }
    }
}
