using simple_leave_management_system.Infrastructure.Contracts;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Infrastructure.Entities
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
