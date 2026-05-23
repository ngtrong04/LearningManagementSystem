using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Repositories.Data;
using PRN232.LMS.Repositories.IRepositories;

namespace PRN232.LMS.Repositories.Repositories
{
    public class GenericRepositories<T> : IGenericRepositories<T> where T : class
    {
        public readonly LmsDbContext _lmsdbContext;
        protected readonly DbSet<T> _dbSet;


        public GenericRepositories(LmsDbContext lmsdbContext)
        {
            _lmsdbContext = lmsdbContext;
            _dbSet = _lmsdbContext.Set<T>();
        }



        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _lmsdbContext.Set<T>().AddAsync(entity);
                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var existingEntity = await GetByIdAsync(id);
            if (existingEntity != null)
            {
                _lmsdbContext.Remove(existingEntity);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _lmsdbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await _lmsdbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                _lmsdbContext.Set<T>().Update(entity);
                return entity;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
