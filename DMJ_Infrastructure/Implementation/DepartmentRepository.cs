using DMJ_Application.Repository;
using DMJ_Domains.Models;
using DMJ_Infrastructure.Data;

namespace DMJ_Infrastructure.Implementation
{
    public class DepartmentRepository :GenericRepository<Department>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context): base(context) 
        {
            _context = context;            
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
        }
    }
}