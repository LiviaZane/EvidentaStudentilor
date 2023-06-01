using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.DataModel
{
    public class Teacher
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string? Name { get; set; }
        [StringLength(35, ErrorMessage = "FirstName can't be longer than 35 characters")]
        public string? FirstName { get; set; }
        public int DepartmentId { get; set; }
        public string? Title { get; set; }


        //navigation proprierties
        public virtual User? User { get; set; }
        public virtual ICollection<Exam>? Exams { get; set; }
        public virtual ICollection<Grade>? Grades { get; set; }
        public virtual Department? Department { get; set; }

    }

}
