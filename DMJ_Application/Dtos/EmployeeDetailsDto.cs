using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMJ_Application.Dtos
{
    public class EmployeeDetailsDto
    {
        public string FirstName { get; set; }
 
        public string LastName { get; set; }

       
        public string Position { get; set; }

   
        public decimal? Salary { get; set; }

       
        public string DepartmentName { get; set; }
        public DateOnly? JoinDate { get; set; }
    }
}
