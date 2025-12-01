using simple_leave_management_system.Infrastructure.Contracts;

namespace simple_leave_management_system.Infrastructure.Repository
{
    public interface IRepositoryWrapper
    {
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        ILeaveTypeRepository LeaveTypes { get; }
        ILeaveQuotaRepository LeaveQuotas { get; }
        ILeaveApplicationRepository LeaveApplications { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
