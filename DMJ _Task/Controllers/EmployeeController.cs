using DMJ_Application.Dtos;
using DMJ_Application.Repository;
using DMJ_Application.Services.Interfaces;
using DMJ_Application.Utilities;
using DMJ_Domains.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMJ__Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =SDRoles.AdminRole)]
    public class EmployeeController : ControllerBase
    {
        //private readonly IUnitOfWork _unitOfWork;
       private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService/*, IUnitOfWork unitOfWork*/)
        {
            // _unitOfWork = unitOfWork;
            _employeeService = employeeService; 
        }


        // test my XUnit with DI in my controller
        [HttpGet("add")]
        public IActionResult Add(int a, int b)
        {
            return Ok(_employeeService.TestAddWithDI(a, b));
        }

        [HttpGet("employees")]
        public IActionResult GetEmployees()
        {           
                var employeeList = _employeeService.GetEmployees();                
                return Ok(employeeList);
        }



        [HttpGet("employees/{id}")]
        public IActionResult GetEmployeeDetails(int id)
        {
            // BackgroundJob.Enqueue(()=>SendEmail("Hello M AbQader")); 


            Console.WriteLine(DateTime.Now);
   //         BackgroundJob.Schedule(()=> SendEmail("Tezt@@@@@@@@@#################*******************"),TimeSpan.FromSeconds(20));

            RecurringJob.AddOrUpdate(() => SendEmail("Hello M AbQader RecurringJob"),Cron.Minutely);

            if (!_employeeService.IsEmployeeExist(id))
                return NotFound($"no employee with ID= {id}");

            var emp = _employeeService.GetEmployeeDetails(id);
            return Ok(emp);

        }


        [HttpPost("employees")]
        public IActionResult CreateEmployee([FromBody] EmployeeDto empDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else 
            {
                _employeeService.CreateEmployee(empDto);               
                return Ok(empDto);
            }
        }
        [HttpPut("employees/{id}")]
        public IActionResult UpdateEmployee([FromRoute]int id, [FromBody] EmployeeDto empDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isExistEmployee = _employeeService.IsEmployeeExist(id);
            if (!isExistEmployee)
            {
                return NotFound($"No Employee with ID= {id}");
            }
            else
            {
                _employeeService.UpdateEmployee(id,empDto);                 
                return Ok(empDto);
            }
        }

        [HttpDelete("employees/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            bool isDeletedEmployee = _employeeService.DeleteEmployee(id);
            if (isDeletedEmployee)
            {
                return Ok("Employee Deleted successfully");               
            }

            return NotFound($"No Employee with ID= {id}");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void SendEmail(string email) 
        {
            Console.WriteLine(email+" -- "+ DateTime.Now);
        }

    }
}
