using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public int? ProfileId { get; set; }
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }
        public int? CurriculaId { get; set; }
        public int StudyYear { get; set; }
        public int Semester { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        public int HourIn { get; set; }
        public int HourOut { get; set; }
        [StringLength(20, ErrorMessage = "Room can't be longer than 20 chars.")]
        public string Room { get; set; }
        public bool Closed { get; set; }


        //navigation property, for foreign key
        public virtual Profile? Profile { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Curricula? Curricula { get; set; }
    }
}
