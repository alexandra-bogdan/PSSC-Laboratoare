using System;
using System.Collections.Generic;
using System.Linq;
using VirtualStore2.Domain;
using System.Text;
using System.Threading.Tasks;

namespace VirtualStore2.Domain.Models
{

    public record Client
    {

        public string? Name { get; set; }
        public string? Address { get; set; }

        public Client(string? name, string? address)
        {
            Name = name;
            Address = address;
        }


    }
}
