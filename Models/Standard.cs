namespace WebApplicationBaseRepo.Models
{
    public class Standard : BaseModel
    {
        //public int StandardId { get; set; }
        public string? StandardName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
