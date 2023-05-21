namespace EvidentaStudentilor.DataModel
{
    public class Grade
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? ExamId { get; set; }
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }
        public int? CurriculaId { get; set; }
        public int? ProfileId { get; set; }
        public int Year { get; set; }
        public int Semester { get; set; }
        public int FormerGrade { get; set; }
        public int ActualGrade { get; set; }
        public bool Reexamination { get; set; }
        public bool ApprovedReexam { get; set; }


        //navigation property
        public virtual Student? Student { get; set; }
        public virtual Exam? Exam { get; set; }
        public virtual Subject? Subject { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Curricula? Curricula { get; set; }
        public virtual Profile? Profile { get; set; }
    }
}
