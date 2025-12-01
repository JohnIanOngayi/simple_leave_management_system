using simple_leave_management_system.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace simple_leave_management_system.Models
{
    public class LeaveType : Base
    {
        [Key]
        public int LeaveTypeId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Code cannot be longer than 20 chars")]
        public string? LeaveTypeCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 chars")]
        public string? LeaveTypeName { get; set; }

        public bool IsPaidLeave { get; set; } = true;

        public bool IsActive { get; set; } = true;

        public ICollection<LeaveQuota>? LeaveQuotas { get; set; }
        public ICollection<LeaveApplication>? LeaveApplications { get; set; }
    }
}
