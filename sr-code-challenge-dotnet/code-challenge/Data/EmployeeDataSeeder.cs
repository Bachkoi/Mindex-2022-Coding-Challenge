using challenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Data
{
    public class EmployeeDataSeeder
    {
        private EmployeeContext _employeeContext;
        private const String EMPLOYEE_SEED_DATA_FILE = "resources/EmployeeSeedData.json";
        public EmployeeDataSeeder(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public async Task Seed()
        {
            if(!_employeeContext.Employees.Any())
            {
                List<Employee> employees = LoadEmployees();
                _employeeContext.Employees.AddRange(employees);

                await _employeeContext.SaveChangesAsync();
            }
        }

        private List<Employee> LoadEmployees()
        {
            using (FileStream fs = new FileStream(EMPLOYEE_SEED_DATA_FILE, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonReader jr = new JsonTextReader(sr))
            {
                JsonSerializer serializer = new JsonSerializer();

                List<Employee> employees = serializer.Deserialize<List<Employee>>(jr);
                FixUpReferences(employees);
                //employees[0].NumberOfReports = getReportingStructure(employees[0]);
                // employees.ForEach(e => getReportingStructure(e)); // Right now this is double calling it.
                ReportingStructure rs = new ReportingStructure();
                
                // List<Employee> testList = rs.structBasedOffID(employees[0].EmployeeId);
                rs.getReportingStructure(employees[0]);
                // List<Employee> rsReturn = rs.structBasedOffEmployee(employees[0]);
                return employees;
            }
        }

        private void FixUpReferences(List<Employee> employees)
        {
            var employeeIdRefMap = from employee in employees
                                select new { Id = employee.EmployeeId, EmployeeRef = employee };

            employees.ForEach(employee =>
            {
                
                if (employee.DirectReports != null)
                {
                    var referencedEmployees = new List<Employee>(employee.DirectReports.Count);
                    employee.DirectReports.ForEach(report =>
                    {
                        var referencedEmployee = employeeIdRefMap.First(e => e.Id == report.EmployeeId).EmployeeRef;
                        referencedEmployees.Add(referencedEmployee);
                    });
                    employee.DirectReports = referencedEmployees;
                }
            });
        }

        //public int getReportingStructure(Employee emp)
        //{
        //    if(numberOfReports == 0)
        //    {
        //        if (emp.DirectReports != null)
        //        {
        //            List<Employee> empsReporters = emp.DirectReports.ToList();
        //            foreach (Employee empReporter in empsReporters)
        //            {
        //                emp.ReportingStructure = empsReporters;
        //                emp.NumberOfReports++;
        //                if (empReporter.DirectReports != null)
        //                {
        //                    for (int i = 0; i < empReporter.DirectReports.Count; i++)
        //                    {
        //                        Console.Write(empReporter.FirstName);

        //                    }
        //                    Console.WriteLine();
        //                    emp.NumberOfReports += getReportingStructure(empReporter);
        //                }
        //                else
        //                {
        //                    // for(int i = 0; i < empReporter.DirectReports.Count; i++)
        //                    // {
        //                    //     Console.Write(empReporter.FirstName);
        //                    // 
        //                    // }
        //                    // Console.WriteLine();
        //                    // numberOfReports += getReportingStructure(empReporter);
        //                }
        //            }
        //        }
        //        Console.WriteLine(numberOfReports);
        //        return emp.NumberOfReports;
        //    }
        //    return emp.NumberOfReports;

        //}
    }
}
