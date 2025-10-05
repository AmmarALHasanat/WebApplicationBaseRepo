namespace WebApplicationBaseRepo.Models
{
    public class StudentSearchViewModel
    {
        public string? Name { get; set; }
        public decimal? MinHeight { get; set; }
        public decimal? MaxHeight { get; set; }
        public IEnumerable<Student> Students { get; set; } = new List<Student>();
    }
}
