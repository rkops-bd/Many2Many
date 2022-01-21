using System.ComponentModel.DataAnnotations;

namespace Many2Many.Entities
{
    public class Course : BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        public List<CourseSubscription> CourseSubscriptions { get; set; }
    }
}
