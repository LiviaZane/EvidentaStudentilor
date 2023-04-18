using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvidentaStudentilor.Models
{
    public class Department
    {
        public int Id { get; set; }
        [StringLength(25, ErrorMessage = "Name can't be longer than 25 characters")]
        public string? Name { get; set; }

        //navigation property
        public virtual ICollection<Teacher>? Teachers { get; set; }
    }
}
