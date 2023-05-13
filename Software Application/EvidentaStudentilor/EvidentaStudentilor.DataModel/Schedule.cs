using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.DataModel
{
    public class Schedule
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [StringLength(20, ErrorMessage = "FileName can't be longer than 20 characters")]
        public string? FileName { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }

        public virtual Profile? Profile { get; set; }

    }
}
