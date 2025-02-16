using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMJ_Domains.Models;

namespace DMJ_Application.Repository
{
    public interface IDepartmentRepository: IGenericRepository<Department>
    {
        void Update(Department department);
    
    }
    
}
