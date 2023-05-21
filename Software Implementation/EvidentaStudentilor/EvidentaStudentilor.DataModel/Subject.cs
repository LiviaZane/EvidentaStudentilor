using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.DataModel
{
    public class Subject
    {
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "Name can't be longer than 30 characters")]
        public string? Name { get; set; }
        public int CourseHours { get; set; }
        public int SeminarHours { get; set; }
        public int LaboratoryHours { get; set; }
        public int ProjectHours { get; set; }
        [StringLength(1, ErrorMessage = "ECP can't be longer than 1 character")]
        public string? ECP { get; set; }
        public int Credits { get; set; }


        // navigation proprierties
        public virtual ICollection<Exam>? Exams { get; set; }
        public virtual ICollection<Curricula>? Curriculas { get; set; }
        public virtual ICollection<Grade>? Grades { get; set; }

    }
}
