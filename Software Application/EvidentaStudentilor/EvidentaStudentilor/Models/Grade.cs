namespace EvidentaStudentilor.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int ExamId { get; set; }
        public int ActualGrade { get; set; }
        public bool Reexamination { get; set; }
        public bool ApprovedReexam { get; set; }


        //navigation property
        public virtual Student? Student { get; set; }
        public virtual Exam? Exam { get; set; }

    }
}
