using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.DataModel
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string? Name { get; set; }
        [StringLength(35, ErrorMessage = "FirstName can't be longer than 35 characters")]
        public string? FirstName { get; set; }
        public int AdmisionYear { get; set; }
        public int CurrentYear { get; set; }
        public int ProfileId { get; set; }
        public bool Budget { get; set; }


        // navigation proprierties
        public virtual User? User { get; set; }
        public virtual Profile? Profile { get; set; }

    }
}
