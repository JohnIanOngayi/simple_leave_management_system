using simple_leave_management_system.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace simple_leave_management_system.Models
{
    public class Department : Base
    {
        [Key]
        public int DepartmentId { get; set; }

        public bool IsActive { get; set; } = true;

        //Navigation
        public ICollection<Employee>? Employees { get; set; }
    }
}
