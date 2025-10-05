using WebApplicationBaseRepo.BaseRepository;
using WebApplicationBaseRepo.Entities;
using WebApplicationBaseRepo.Models;

namespace WebApplicationBaseRepo.Repositories
{
    public class StudentRepository : BaseRepository<Student>
    {
        public StudentRepository(EFCoreDbContext context) : base(context)
        {
        }
    }
}
