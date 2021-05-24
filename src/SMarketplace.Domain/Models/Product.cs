using SMarketplace.Core.Data;
using SMarketplace.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMarketplace.Domain.Models
{
    public class Product : Entity, IAggregateRoot
    {

        public string Name { get; private set; }
        public double Price { get; private set; }
        public string Image { get; private set; }


    }
}
