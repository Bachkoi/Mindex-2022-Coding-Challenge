using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;
using System.Diagnostics;



namespace challenge.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }
        // [ActionName("employee")]
        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            // _employeeService.Pass();

            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }
        
        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }
        //[HttpGet("{id}/{position}", Name = "ReadDirectingStructure")]
        //public IActionResult ReadDirectingStructure(string id, string position)
        //{
        //    List<Employee> empList = _employeeService.Pass();
        //    ReportingStructure rs = new ReportingStructure();
        //    for(int i = 0; i < empList.Count; i++)
        //    {
        //        if(empList[i].EmployeeId == id)
        //        {
        //            rs.Employee = empList[i];
        //        }
        //    }
        //    //rs.Employee = _employeeService.GetById(id);
        //    Debug.WriteLine(rs.Employee);
        //    Debug.WriteLine(id);
        //    List<Employee> list = new List<Employee>();
        //    //Employee structEmp = _employeeService.GetById(id); // Doesn't work becasue I can't get the Repo working
        //    Employee structEmp = rs.Employee; // Doesn't work becasue I can't get the Repo working
        //    Debug.WriteLine(structEmp);
        //    if (structEmp.DirectReports != null)
        //    {
        //        List<Employee> empsReporters = structEmp.DirectReports.ToList();
        //        for (int i = 0; i < empsReporters.Count; i++)
        //        {
        //            if (i >= structEmp.DirectReports.Count)
        //            {
        //                return Ok(structEmp.ReportingStructureLToR);
        //            }
        //            structEmp.ReportingStructureLToR = (empsReporters);
        //            structEmp.NumberOfReports++;
        //            if (structEmp.DirectReports[i].DirectReports != null)
        //            {
        //                for (int j = 0; j < structEmp.DirectReports[i].DirectReports.Count; j++)
        //                {
        //                    structEmp.ReportingStructureLToR.Add(structEmp.DirectReports[i].DirectReports[j]);

        //                }
        //                Debug.WriteLine("_");
        //                //structEmp.ReportingStructureLToR.AddRange(empReporter.DirectReports);
        //            }
        //            else
        //            {
        //                // for(int i = 0; i < empReporter.DirectReports.Count; i++)
        //                // {
        //                //     Console.Write(empReporter.FirstName);
        //                // 
        //                // }
        //                // Console.WriteLine();
        //                // numberOfReports += getReportingStructure(empReporter);
        //            }
        //        }
        //    }

        //    //if (structEmp.DirectReports != null) // Right now, it is null bc empDS hasn't connected them yet.
        //    //{
        //    //    List<Employee> empsReporters = structEmp.DirectReports.ToList();
        //    //    foreach (Employee empReporter in empsReporters)
        //    //    {
        //    //        structEmp.ReportingStructureLToR = empsReporters;
        //    //        structEmp.NumberOfReports++;
        //    //        if (empReporter.DirectReports != null)
        //    //        {
        //    //            structEmp.ReportingStructureLToR.AddRange(empReporter.DirectReports);
        //    //            ReadDirectingStructure(empReporter.EmployeeId);
        //    //        }
        //    //        else
        //    //        {
        //    //            // for(int i = 0; i < empReporter.DirectReports.Count; i++)
        //    //            // {
        //    //            //     Console.Write(empReporter.FirstName);
        //    //            // 
        //    //            // }
        //    //            // Console.WriteLine();
        //    //            // numberOfReports += getReportingStructure(empReporter);
        //    //        }
        //    //    }
        //    //}
        //    list = structEmp.ReportingStructureLToR;
        //    Debug.WriteLine(list);
        //    return Ok(list);
        //}

        // Compensation ENDPOINT1 to create the compensation endpoint
        [HttpPost("{id}/{salary}/{effectiveDate}", Name = "PostCompensation")] // For some reason the request does not match any routes
        public IActionResult PostCompensation(string id, double salary, string effectiveDate)
        {
            Employee tempEmp = _employeeService.GetById(id);
            Compensation comp = new Compensation(tempEmp, salary, effectiveDate);

            //return CreatedAtRoute("PostCompensation", new { tempEmp, salary = salary, effectiveDate = effectiveDate }, tempEmp);
             return Ok(comp);
            // return CreatedAtRoute("PostCompensation", new { id = employee.EmployeeId }, employee);
            // return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);

        }

        // Compensation Endpoint2 to read by EmployeeID
        [HttpGet("{id}/{salary}", Name = "ReadCompensation")]
        public IActionResult ReadCompensation(string id, double salary)
        {
            Employee tempEmp = _employeeService.GetById(id);
            Compensation comp = new Compensation(tempEmp, salary, "March5");
            Debug.WriteLine(comp);

            return Ok(comp);
        }
    }
}
