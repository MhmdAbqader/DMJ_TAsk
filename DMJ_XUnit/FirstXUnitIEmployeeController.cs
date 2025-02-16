using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMJ__Task.Controllers;
using DMJ_Application.Dtos;
using DMJ_Application.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DMJ_XUnit
{
    public class FirstXUnitIEmployeeController
    {
 
        [Fact]
        public void TestAddWithDI_EmployeeContrller() 
        {
            // arrange
            var mokEmployeeService = new Mock<IEmployeeService>();
            mokEmployeeService.Setup(servic => servic.TestAddWithDI(10,90)).Returns(100);
            var employeeController = new EmployeeController(mokEmployeeService.Object);

            int a_InputControllerAddMethod = 10;
            int b_InputControllerAddMethod = 90;

            // act
            var result = employeeController.Add(a_InputControllerAddMethod,b_InputControllerAddMethod) as OkObjectResult;
            
            // assertion
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(100, result.Value);

        }

        [Fact]
        public void TestNotFoundEmployee_EmployeeContrllerShouldReturnNotFoundEmployee()
        {
            // arrange
            var mokEmployeeService = new Mock<IEmployeeService>();
            mokEmployeeService.Setup(servic => servic.GetEmployeeDetails(10)).Returns((EmployeeDetailsDto)null);
            var employeeController = new EmployeeController(mokEmployeeService.Object);

            int id = 10;
           
            // act
            var result = employeeController.GetEmployeeDetails(id) as NotFoundObjectResult;

            // assertion
            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public void TestNotNullEmployeeDetails_EmployeeContrllerShouldReturnEmployeeDetails()
        {
            // arrange
            var mokEmployeeService = new Mock<IEmployeeService>();
            EmployeeDetailsDto expectedEmployeeDetailsDto = new EmployeeDetailsDto() 
            {
                FirstName="mhmd",LastName ="abqader",
                Salary=24000, DepartmentName="HR",
                JoinDate=DateOnly.FromDateTime(DateTime.Now),Position="dot net developer"
            };

            mokEmployeeService.Setup(servic => servic.GetEmployeeDetails(2)).Returns(expectedEmployeeDetailsDto);
            var employeeController = new EmployeeController(mokEmployeeService.Object);

            int id = 2 ;

            // act

            var result = employeeController.GetEmployeeDetails(id) as OkObjectResult;

            // assertion

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedEmployeeDetailsDto, result.Value);

        }

    }
}
