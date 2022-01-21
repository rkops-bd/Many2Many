using System.ComponentModel.DataAnnotations;

namespace Many2Many.Entities
{
    public class CourseSubscription
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int CourseId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }

        public DateTime SubscriptionDate { get; set; }
    }
}
