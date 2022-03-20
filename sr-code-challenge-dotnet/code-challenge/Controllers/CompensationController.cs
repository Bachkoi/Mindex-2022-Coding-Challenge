using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;
using System.Diagnostics;
using System.Runtime;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        public CompensationController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }
        [HttpGet]
        [Route("post/call/{id}")] // Using this in the same sense as a curl to get to the post
        public IActionResult GetPostCompensation(string id)
        {
            return PostCompensation(id);
        }
        // Compensation ENDPOINT1 to create the compensation endpoint
        
        // Takes in a paramter of a string ID, then it creates a temporary employee variable
        // using the employee service's method GetByID. Then it creates a new compensation,
        // sets the compensation's employee to the temp variable, sets its employeeID to the id
        // then it randomly sets a salary and chooses a random date from the potential dates. Then it 
        // creates the compensation variable and returns what that compensation looks like.
        [HttpPost]
        [Route("post/{id}")]

        public IActionResult PostCompensation(string id)
        {
            Employee tempEmp = _employeeService.GetById(id);
            Compensation comp = new Compensation();
            comp.Employee = tempEmp;
            comp.Employee.EmployeeId = id;
            comp.CompensationEmployeesID = id;
            Random rng = new Random();
            comp.Salary = rng.Next(1,100000000) * rng.NextDouble();

            string[] potentialDates = { "March 6th", "January 3rd", "December 25th", "May 5th", "October 10th" };
            comp.EffectiveDate = potentialDates[rng.Next(0,potentialDates.Length)];

            _employeeService.CreateComp(comp);
            Debug.WriteLine(Ok(comp));


            return CreatedAtRoute("ReadCompensation", new { id = comp.Employee.EmployeeId }, comp);
        }

        // Compensation Endpoint2 to read by EmployeeID
        //[HttpGet("{id}/{salary}", Name = "ReadCompensation")]
        [HttpGet]
        [Route("{id}", Name="ReadCompensation")]

        public IActionResult ReadCompensation(string id)
        {

            Compensation comp = _employeeService.CompensationGetById(id); // Get the compensation from the employeeServices
            Debug.WriteLine(Ok(comp)); // For debug testing
            Debug.WriteLine(comp);

            return Ok(comp);
        }
    }
}
