namespace WebApplicationBaseRepo.Models
{
    public class BaseModel
    {
        // Common properties for all models can be defined here
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
