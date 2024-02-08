
using Microsoft.EntityFrameworkCore;
using N.G.HRS.Date;
using N.G.HRS.Models;

namespace N.G.HRS.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var entity = await _context.Set<T>().FindAsync(Id);
            if (entity != null)
            {
                var ent = _context.Remove(entity);
                if (ent != null)
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static implicit operator Repository<T>(Repository<BaseModel> v)
        {
            return new Repository<T>(v._context);
        }
    }
}
