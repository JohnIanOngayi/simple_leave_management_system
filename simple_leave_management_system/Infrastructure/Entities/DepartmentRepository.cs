using simple_leave_management_system.Infrastructure.Contracts;
using simple_leave_management_system.Models;

namespace simple_leave_management_system.Infrastructure.Entities
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
