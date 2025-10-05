using Microsoft.EntityFrameworkCore;
using WebApplicationBaseRepo.BaseRepository;
using WebApplicationBaseRepo.Entities;
using WebApplicationBaseRepo.Models;

namespace WebApplicationBaseRepo.Repositories
{
    public class StandardRepository : BaseRepository<Standard>
    {
        public StandardRepository(EFCoreDbContext context) : base(context)
        {
        }

        public async Task<Standard?> GetByIdWithStudents(int id)
        {
            var standard = await _context.Standards
                .Include(s => s.Students)
                .FirstOrDefaultAsync(s => s.Id == id);
            return standard;
        }


        public async Task<bool> UnlinkStudent(int standardId, int studentId)
        {

            var r = await _context.Students
                .Where(s => s.StandardId == standardId && s.Id == studentId)
                        .ExecuteUpdateAsync(up => up.SetProperty(s => s.StandardId, s => (int?)null));
            return r > 0;
        }
    }
}
