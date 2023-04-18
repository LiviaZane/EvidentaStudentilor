namespace EvidentaStudentilor.Models
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AdmisionYear { get; set; }
        public int CurrentYear { get; set; }
        public int ProfileId { get; set; }
        public bool Budget { get; set; }


        // navigation proprierties
        public virtual User? User { get; set; }
        public virtual Profile? Profile { get; set; }

    }
}
