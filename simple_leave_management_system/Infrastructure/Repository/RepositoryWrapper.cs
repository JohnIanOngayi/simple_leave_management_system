using simple_leave_management_system.Infrastructure.Contracts;
using simple_leave_management_system.Infrastructure.Entities;

namespace simple_leave_management_system.Infrastructure.Repository
{
    public class RepositoryWrapper(RepositoryContext repositoryContext) : IRepositoryWrapper
    {
        private readonly RepositoryContext _repositoryContext = repositoryContext;
        private IDepartmentRepository? _departments;
        private IEmployeeRepository? _employees;
        private ILeaveTypeRepository? _leaveTypes;
        private ILeaveQuotaRepository? _leaveQuotas;
        private ILeaveApplicationRepository? _leaveApplications;

        public IDepartmentRepository Departments
        {
            get
            {
                _departments ??= new DepartmentRepository(_repositoryContext);
                return _departments;
            }
        }

        public IEmployeeRepository Employees
        {
            get
            {
                _employees ??= new EmployeeRepository(_repositoryContext);
                return _employees;
            }
        }

        public ILeaveTypeRepository LeaveTypes
        {
            get
            {
                _leaveTypes ??= new LeaveTypeRepository(_repositoryContext);
                return _leaveTypes;
            }
        }

        public ILeaveQuotaRepository LeaveQuotas
        {
            get
            {
                _leaveQuotas ??= new LeaveQuotaRepository(_repositoryContext);
                return _leaveQuotas;
            }
        }

        public ILeaveApplicationRepository LeaveApplications
        {
            get
            {
                _leaveApplications ??= new LeaveApplicationRepository(_repositoryContext);
                return _leaveApplications;
            }
        }
    }
}
