using simple_leave_management_system.Infrastructure.Contracts;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Infrastructure.Entities
{
    public class LeaveApplicationRepository : RepositoryBase<LeaveApplication>, ILeaveApplicationRepository
    {
        public LeaveApplicationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
