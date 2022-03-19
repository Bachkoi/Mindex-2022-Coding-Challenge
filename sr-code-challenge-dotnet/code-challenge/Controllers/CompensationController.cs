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
        //[HttpPost("{id}/{salary}/{effectiveDate}", Name = "PostCompensation")] // For some reason the request does not match any routes
        [HttpPost]
        [Route("post/{id}")]
        // [Route("{id}/{salary}/{effectiveDate}")]
        //public IActionResult PostCompensation(string id, double salary, string effectiveDate)
        //public IActionResult PostCompensation(string id, double salary, string effectiveDate)

        public IActionResult PostCompensation(string id)
        {
            //Employee tempEmp = pComp.CompensationEmp;
            Employee tempEmp = _employeeService.GetById(id);
            //double tempSal = pComp.Salary;
            //string tempEff = pComp.EffectiveDate;
            // Compensation comp = new Compensation(tempEmp, salary, effectiveDate);
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
            //Employee tempEmp = _employeeService.GetById(id);
            //Compensation comp = new Compensation(tempEmp, salary, "March5");
            // Should be get comp
            Compensation comp = _employeeService.CompensationGetById(id);
            //_employeeService.CreateComp(comp);
            Debug.WriteLine(Ok(comp));
            Debug.WriteLine(comp);

            return Ok(comp);
        }
    }
}
