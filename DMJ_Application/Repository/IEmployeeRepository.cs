using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMJ_Domains.Models;

namespace DMJ_Application.Repository
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        void Update(Employee employee);

    }
}
