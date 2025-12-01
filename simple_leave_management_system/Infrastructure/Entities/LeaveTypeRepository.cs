using simple_leave_management_system.Infrastructure.Contracts;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Infrastructure.Entities
{
    public class LeaveTypeRepository : RepositoryBase<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
