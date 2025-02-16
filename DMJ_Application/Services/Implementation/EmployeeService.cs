using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMJ_Application.Dtos;
using DMJ_Application.Repository;
using DMJ_Application.Services.Interfaces;
using DMJ_Domains.Models;

namespace DMJ_Application.Services.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
   
        public IEnumerable<Employee> GetEmployees()
        {
            var employeeList = _unitOfWork.EmployeeRepository.GetAll(null, "Department");
            return employeeList;
        }



        
        public EmployeeDetailsDto GetEmployeeDetails(int id)
        {
            var emp = _unitOfWork.EmployeeRepository.GetById(e => e.Id == id, "Department");
            EmployeeDetailsDto employeeDetailsDto = new EmployeeDetailsDto();

            employeeDetailsDto.DepartmentName = emp?.Department.Name;
            employeeDetailsDto.FirstName = emp?.FirstName;
            employeeDetailsDto.LastName = emp?.LastName;
            employeeDetailsDto.Salary =emp?.Salary;
            employeeDetailsDto.Position = emp?.Position;
            employeeDetailsDto.JoinDate = emp?.JoinDate;

            return employeeDetailsDto;
        }


     
        public void CreateEmployee(EmployeeDto empDto)
        {         
                var employee = new Employee
                {
                    DepartmentId = empDto.DepartmentId,
                    FirstName = empDto.FirstName,
                    LastName = empDto.LastName,
                    Salary = empDto.Salary,
                    Position = empDto.Position,
                    JoinDate = DateOnly.FromDateTime(DateTime.Now)
                };
                _unitOfWork.EmployeeRepository.Add(employee);
                _unitOfWork.SaveChanges();           
        }
     
        public void UpdateEmployee(int id, EmployeeDto empDto)
        {         
            var employee = _unitOfWork.EmployeeRepository.GetById(e => e.Id == id);
                      
                employee.DepartmentId = empDto.DepartmentId;
                employee.FirstName = empDto.FirstName;
                employee.LastName = empDto.LastName;
                employee.Salary = empDto.Salary;
                employee.Position = empDto.Position;
                employee.JoinDate = employee.JoinDate;

                _unitOfWork.EmployeeRepository.Update(employee);
                _unitOfWork.SaveChanges();
             
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(e => e.Id == id);
            if (employee == null)
            {
                return false;
            }            
            _unitOfWork.EmployeeRepository.Remove(employee);
            _unitOfWork.SaveChanges();
            return true;
        }

        public bool IsEmployeeExist(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(e => e.Id == id);
            if (employee == null)
            {
                return false;
            }
            return true;
        }

        public int TestAddWithDI(int a, int b) => a + b;
        
    }
}
