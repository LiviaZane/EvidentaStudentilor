using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "Name can't be longer than 30 characters")]
        public string Name { get; set; }


        //navigation property
        public virtual ICollection<Student>? Students { get; set; }
        //public virtual ICollection<Exam>? Exams { get; set; }
        public virtual ICollection<Schedule>? Schedules { get; set; }
        public virtual ICollection<Curricula>? Curricula { get; set; }
    }
}
