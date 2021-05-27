using System;
using System.Collections.Generic;
using System.Text;

namespace SMarketplace.Domain.DTOs
{
    public class ProdutcDto
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public double Price { get;  set; }
        public string Image { get;  set; }
    }
}
