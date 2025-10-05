using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplicationBaseRepo.Entities;
using WebApplicationBaseRepo.Models;

namespace WebApplicationBaseRepo.BaseRepository
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        protected readonly EFCoreDbContext _context;

        protected BaseRepository(EFCoreDbContext context)
        {
            _context = context;
        }

        public virtual async Task<bool> Add(T entity)
        {
            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<bool> Update(T entity)
        {
            try
            {
                var existingEntity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity.Id);
                if (existingEntity == null) return false;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.CreatedAt = existingEntity.CreatedAt;
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null) return false;

                _context.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
