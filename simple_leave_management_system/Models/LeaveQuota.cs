using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using simple_leave_management_system.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_leave_management_system.Models
{
    //CONSTRAINT UQ_Employee_LeaveType_Year UNIQUE(EmployeeId, LeaveTypeId, LeaveYear)

    public class LeaveQuota : Base
    {
        [Key]
        public int LeaveQuotaId { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        [ValidateNever]
        public Employee? Employee { get; set; }

        public int LeaveTypeId { get; set; }
        [ForeignKey("LeaveTypeId")]
        [ValidateNever]
        public LeaveType? LeaveType { get; set; }

        [Required]
        [Range(2020, 2100, ErrorMessage = "Leave year must be between 2020 and 2100")]
        public int LeaveYear { get; set; } = 2025;

        [Column(TypeName = "decimal(5, 2)")]
        public decimal TotalAllocated { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal TotalUsed { get; set; } = decimal.Zero;
    }
}
