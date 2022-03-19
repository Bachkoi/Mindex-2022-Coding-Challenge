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
        [HttpGet("{id}", Name = "ReadDirectingStructure")]
        public IActionResult ReadDirectingStructure(string id)
        {
            List<Employee> empList = _employeeService.Pass();
            ReportingStructure rs = new ReportingStructure();
            for(int i = 0; i < empList.Count; i++)
            {
                if(empList[i].EmployeeId == id)
                {
                    rs.Employee = empList[i];
                }
            }
            //rs.Employee = _employeeService.GetById(id);
            Debug.WriteLine(rs.Employee);
            Debug.WriteLine(id);
            List<Employee> list = new List<Employee>();
            //Employee structEmp = _employeeService.GetById(id); // Doesn't work becasue I can't get the Repo working
            Employee structEmp = rs.Employee; // Doesn't work becasue I can't get the Repo working
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
