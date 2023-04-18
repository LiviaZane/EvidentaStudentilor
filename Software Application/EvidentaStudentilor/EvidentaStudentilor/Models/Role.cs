using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.Models
{
    public class Role
    {
        public int Id { get; set; }
        [StringLength(13, ErrorMessage = "Name can't be longer than 13 characters")]
        public string? Name { get; set; }


        // navigation proprierty
        public virtual ICollection<User>? Users { get; set; }
    }
}
