using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMJ_Application.Dtos;
using DMJ_Domains.Models;

namespace DMJ_Application.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        EmployeeDetailsDto GetEmployeeDetails(int id);
        void CreateEmployee(EmployeeDto empDto);
        void UpdateEmployee(int id, EmployeeDto empDto);
        bool DeleteEmployee(int id);
        bool IsEmployeeExist(int id);

        int TestAddWithDI(int a ,int b);

    }
}
