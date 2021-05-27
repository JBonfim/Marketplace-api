using SMarketplace.Domain.DTOs;
using SMarketplace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMarketplace.Domain.Interfaces
{
    public interface IProductService
    {
        Task<Product> Get(Guid id);
        Task<IEnumerable<Product>> GetAll();
        Task<bool> Delete(Guid id);
    }
}
