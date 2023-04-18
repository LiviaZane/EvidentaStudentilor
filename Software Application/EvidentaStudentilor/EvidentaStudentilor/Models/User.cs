using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.Models
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        [StringLength(20, ErrorMessage = "Name can't be longer than 20 characters")]
        public string? Name { get; set; }
        [StringLength(35, ErrorMessage = "FirstName can't be longer than 35 characters")]
        public string? FirstName { get; set; }
        [StringLength(30, ErrorMessage = "Email can't be longer than 30 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [StringLength(30, ErrorMessage = "Password can't be longer than 30 characters")]
        public string? Paswword { get; set; }

        //navigation proprierties
        public virtual Role? Role { get; set; }
        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<Teacher>? Teachers { get; set; }
    }
}
