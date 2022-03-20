using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;
using System.Diagnostics;

namespace challenge.Controllers
{
    [Route("api/DirectingStructure")]
    public class DirectingStructureController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;
        public DirectingStructureController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        /// <summary>
        /// ReadDirectingStructure
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// This method is the way in which the user is able to actually see the directingStructure of the employee based off of the passed in ID
        /// that matches the empoloyees ID. This method will create a new Reporting Structure object and then to determine what employee is for that
        /// Reporting Structure obj, we will use a for loop to match the employeeIds. Then we will check to see if that employee's DirectReports are null,
        ///  and if they arent then we will continue to make their DirectReports to a list then check to see if they and their "descendents" have directly 
        ///  reporting subbordinates, and if they do, then add it to the list and increment the count.
        /// </returns>
        [HttpGet("{id}", Name = "ReadDirectingStructure")]
        public IActionResult ReadDirectingStructure(string id)
        {
            List<Employee> empList = _employeeService.Pass(); // To get the employees from the repo
            ReportingStructure rs = new ReportingStructure();
            for(int i = 0; i < empList.Count; i++)
            {
                if(empList[i].EmployeeId == id)
                {
                    rs.Employee = empList[i];
                }
            }
            //rs.Employee = _employeeService.GetById(id);
            //Debug.WriteLine(rs.Employee); Used to debug and read it in the console. 
            //Debug.WriteLine(id);
            List<Employee> list = new List<Employee>();
            Employee structEmp = rs.Employee; 
            Debug.WriteLine(structEmp);
            if (structEmp.DirectReports != null)
            {
                List<Employee> empsReporters = structEmp.DirectReports.ToList();
                for (int i = 0; i < empsReporters.Count; i++)
                {
                    if (i >= structEmp.DirectReports.Count)
                    {
                        return Ok(structEmp.ReportingStructureLToR);
                   }
                    structEmp.ReportingStructureLToR = (empsReporters);
                    structEmp.NumberOfReports++;
                    if (structEmp.DirectReports[i].DirectReports != null)
                    {
                        for (int j = 0; j < structEmp.DirectReports[i].DirectReports.Count; j++)
                        {
                            structEmp.ReportingStructureLToR.Add(structEmp.DirectReports[i].DirectReports[j]);
                               }
                        Debug.WriteLine("_");
                    }
                    else
                    {
                    }
                }
            }
            list = structEmp.ReportingStructureLToR;
            Debug.WriteLine(list);
            return Ok(list);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
