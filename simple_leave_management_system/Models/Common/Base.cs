using System.ComponentModel.DataAnnotations;

namespace simple_leave_management_system.Models.Common
{
    public abstract class Base
    {
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ScaffoldColumn(false)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
