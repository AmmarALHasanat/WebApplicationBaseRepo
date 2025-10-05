using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationBaseRepo.Models
{
    public class Student : BaseModel
    {
        //public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Height { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public float Weight { get; set; }
        public int? StandardId { get; set; }

    }
}
