using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using simple_leave_management_system.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace simple_leave_management_system.Models
{
    public class Employee : Base
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Code cannot be longer than 50 characters")]
        public string? EmployeeCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "First Name cannot be longer than 50 characters")]
        public string? FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Last Name cannot be longer than 50 characters")]
        public string? LastName { get; set; }

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        [ValidateNever]
        public required Department Department { get; set; }

        public DateTime DateOfJoining { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<LeaveType>? LeaveTypes { get; set; }
        public ICollection<LeaveQuota>? LeaveQuotas { get; set; }
        public ICollection<LeaveApplication>? LeaveApplications { get; set; }

        [NotMapped]
        public string? EmployeeName => $"{FirstName} {LastName}".Trim();
        [NotMapped]
        public string? DisplayText => $"{EmployeeCode} - {FirstName} {LastName}".Trim();
    }
}
