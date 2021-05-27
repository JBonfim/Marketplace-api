using SMarketplace.Core.Data;
using SMarketplace.Domain.Interfaces;
using SMarketplace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMarketplace.Application.Services
{
    public class ProductService : IProductService
    {
        private IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<Product> Get(Guid id)
        {
            return await _repository.SelectAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repository.SelectAsync();
        }
    }
}
