using System.Collections.Generic;
using System;
using backend.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace backend.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByID(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Insert(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public async Task Delete(int id)
        {
            var entity = await GetByID(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task Update(T obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetByCondition(Func<T, bool> predicate)
        {
            return await Task.Run(() => _context.Set<T>().Where(predicate).ToList());
        }

    }
}
