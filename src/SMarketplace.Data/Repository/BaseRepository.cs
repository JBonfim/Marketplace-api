using Microsoft.EntityFrameworkCore;
using SMarketplace.Core.Data;
using SMarketplace.Core.DomainObjects;
using SMarketplace.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace SMarketplace.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : Entity, IAggregateRoot
    {

        protected readonly AplicationContext _context;
        protected DbSet<T> _dataset;

        public BaseRepository(AplicationContext context)
        {
            _context = context;
            _dataset = _context.Set<T>();
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                {
                    return false;
                }

                _dataset.Remove(result);
                return await UnitOfWork.Commit();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await _dataset.AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {


                _dataset.Add(item);

                return await UnitOfWork.Commit() ? item : throw new DomainException("Houve um erro ao persistir os dados.");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertAll(IEnumerable<T> item)
        {
            try
            {
                _dataset.AddRange(item);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await _dataset.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await _dataset.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UpdateAsync(T item)
        {
            try
            {

                //_context.Entry(result).CurrentValues.SetValues(item);
                _dataset.Update(item);
                return await UnitOfWork.Commit();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddEntity(T item)
        {
            _dataset.Add(item);
        }

        public void UpdateEntity(T item)
        {
            _dataset.Update(item);
        }

        public DbConnection ObterConexao() => _context.Database.GetDbConnection();

    }
}
