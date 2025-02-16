using DMJ_Application.Repository;
using DMJ_Domains.Models;
using DMJ_Infrastructure.Data;

namespace DMJ_Infrastructure.Implementation
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }
    }
}