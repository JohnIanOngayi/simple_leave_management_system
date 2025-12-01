using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using simple_leave_management_system.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_leave_management_system.Models
{
    public class LeaveApplication : Base
    {
        [Key]
        public int LeaveApplicationId { get; set; }

        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        [ValidateNever]
        public Employee? Employee { get; set; }

        public int LeaveTypeId { get; set; }
        [ForeignKey("LeaveTypeId")]
        [ValidateNever]
        public LeaveType? LeaveType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal TotalDays { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Reason cannot be longer than 500 chars")]
        public string? Reason { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Status cannot be longer than 20 chars")]
        public string? Status { get; set; } // Pending, Approved, Rejected, Cancelled

        public DateTime AppliedOn { get; set; } = DateTime.Now;

        public int ApprovedBy { get; set; }
        [ForeignKey("ApprovedBy")]
        [ValidateNever]
        public Employee? Approver { get; set; }

        public DateTime ApprovedOn { get; set; }
    }
}
