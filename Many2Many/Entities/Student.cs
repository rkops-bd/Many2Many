using System.ComponentModel.DataAnnotations;

namespace Many2Many.Entities
{
    public class Student : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
        public List<CourseSubscription> CourseSubscriptions { get; set; }
    }
}
