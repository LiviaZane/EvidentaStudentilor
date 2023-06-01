using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.DataModel
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
       
        [StringLength(30, ErrorMessage = "Email can't be longer than 30 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [StringLength(30, ErrorMessage = "Password can't be longer than 30 characters")]
        public string? Paswword { get; set; }

        //navigation proprierties
        public virtual Role? Role { get; set; }
        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
