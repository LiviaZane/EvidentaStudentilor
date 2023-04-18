namespace EvidentaStudentilor.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public string? Title { get; set; }



        //navigation proprierties
        public virtual User? User { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
        public virtual ICollection<Grade>? Grades { get; set; }
        public virtual Department? Department { get; set; }

    }

}
