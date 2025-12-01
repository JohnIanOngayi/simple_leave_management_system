using simple_leave_management_system.Infrastructure.Contracts;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Infrastructure.Entities
{
    public class LeaveQuotaRepository : RepositoryBase<LeaveQuota>, ILeaveQuotaRepository
    {
        public LeaveQuotaRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
