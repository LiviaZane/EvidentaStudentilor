using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public int CurriculaId { get; set; }
        public int TeacherId { get; set; }
        public DateTime Data { get; set; }
        public int HourIn { get; set; }
        public int HourOut { get; set; }
        [StringLength(20, ErrorMessage = "Room can't be longer than 20 chars.")]
        public string Room { get; set; }
        public bool Closed { get; set; }


        //navigation property, for foreign key
        public virtual Curricula? Curricula { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
