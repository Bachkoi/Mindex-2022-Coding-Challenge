using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;
using challenge.Data;
using challenge.Services;
using challenge.Controllers;


namespace challenge.Models
{
    // This class is where I will be setting up the basics for the 
    // ReportingStructure type.
    public class ReportingStructure
    {
        // Establish necessary getter and setter functions.
        private Employee emp;
        private int numberOfReports;
        public EmployeeController employeeController;
        public EmployeeRespository testEmpRepo;

        public Employee Employee { 
            get 
            {
                return emp;
            }
            set 
            {
                emp = value; 
            }
        }
        public int NumberOfReports
        {
            get
            {
                return numberOfReports;
            }
            set
            {
                numberOfReports = value;
            }
        }


        /// <summary>
        /// getReportingSrtucture Method
        /// </summary>
        /// <param name="emp"></param>
        /// This method returns the number of total reporters (the number of their direct, and their direct reporters reporters, and so on and so forth).
        /// It accepts in a parameter of an Employee Object and first checks to make sure that they won't double the same direct reporter list, to
        /// account for this, we check to see if the numberOfReporters is equal to 0, if so, then we can continue.
        /// We then check to make sure that the Emp's direct reporters isn't null to avoid a null reference error.
        /// Then we create a temporary list called empsReporters and convert Emp's direct reporters to a list.
        /// Then use a for-each loop to loop through each Employee in empsReporters and set the reportingStructure property
        /// equal to that of the direct reporters, as this function will be called recursively to create the depth of multiple levels
        /// of reporters. Then increment the number of Emp's number of reporters, for debugging purposes print the names of the direct reporters
        /// Then add a '_' to know when it has cycled to the next level of depth (I.e ringo -> pete and george) Then call this method
        /// on the direct reporters of emp, so it will add the size of the direct reporters of emp's direct reporters. This also builds the 
        /// ReportingStructure list, which holds the employee structure that is being sought in Task 1. In the debug log, you will see if a name appears 2 times (i.e Ringo) it means
        /// it is the parent of the ones following it.
        /// <returns></returns>
        
        public int getReportingStructure(Employee emp) // Gets and returns the number, and buidls the reporting structure, but not based off the ID of the employee.
        {
            Debug.Write(emp.FirstName);
            if (emp.NumberOfReports == 0)
            {
                if (emp.DirectReports != null)
                {
                    List<Employee> empsReporters = emp.DirectReports.ToList();
                    foreach (Employee empReporter in empsReporters)
                    {
                        emp.ReportingStructure = empsReporters;
                        emp.NumberOfReports++;
                        if (empReporter.DirectReports != null)
                        {
                            Debug.Write(empReporter.FirstName);
                            //emp.ReportingStructure.Add(empReporter);
                            emp.NumberOfReports += getReportingStructure(empReporter);
                            // emp.NumberOfReports += getReportingStructure(empReporter);
                        }
                        else
                        {
                            Debug.Write(empReporter.FirstName);

                        }
                        //if (emp.DirectReports != null)
                        //{
                        //    // Debug.Write(emp.FirstName);
                        //    for (int i = 0; i < emp.DirectReports.Count; i++)
                        //    {
                        //        Debug.Write(emp.DirectReports[i].FirstName);
                        //        if (emp.DirectReports[i].DirectReports != null)
                        //        {
                        //            // Debug.Write(emp.DirectReports[i].FirstName);
                        //            emp.NumberOfReports += getReportingStructure(empReporter);
                        //        }

                        //    }
                        //    Debug.WriteLine("_");
                        //    // emp.NumberOfReports += getReportingStructure(empReporter);
                        //}
                        //else
                        //{
                        // for(int i = 0; i < empReporter.DirectReports.Count; i++)
                        // {
                        //     Console.Write(empReporter.FirstName);
                        // 
                        // }
                        // Console.WriteLine();
                        // numberOfReports += getReportingStructure(empReporter);
                        // }
                    }
                }
                Debug.WriteLine(emp.NumberOfReports);
                return emp.NumberOfReports;
            }
            return emp.NumberOfReports;

        }
        public List<Employee> structBasedOffID(String empID)
        {
            List<Employee> list = new List<Employee>();
            Employee structEmp = testEmpRepo.GetById(empID); // Doesn't work becasue I can't get the Repo working
            if (structEmp.DirectReports != null)
            {
                List<Employee> empsReporters = structEmp.DirectReports.ToList();
                foreach (Employee empReporter in empsReporters)
                {
                    structEmp.ReportingStructure = empsReporters;
                    structEmp.NumberOfReports++;
                    if (structEmp.DirectReports != null)
                    {
                        for (int i = 0; i < emp.DirectReports.Count; i++)
                        {
                            Debug.Write(emp.DirectReports[i].FirstName);

                        }
                        Debug.WriteLine("_");
                        structEmp.ReportingStructureLToR.AddRange(structBasedOffID(empReporter.EmployeeId));
                    }
                    else
                    {
                        // for(int i = 0; i < empReporter.DirectReports.Count; i++)
                        // {
                        //     Console.Write(empReporter.FirstName);
                        // 
                        // }
                        // Console.WriteLine();
                        // numberOfReports += getReportingStructure(empReporter);
                    }
                }
            }
            list = structEmp.ReportingStructureLToR;
            return list;
        }
        public List<Employee> structBasedOffEmployee(Employee emp)
        {
            List<Employee> list = new List<Employee>();
            // Employee structEmp = testEmpRepo.GetById(empID); // Doesn't work becasue I can't get the Repo working
            if (emp.DirectReports != null)
            {
                List<Employee> empsReporters = emp.DirectReports.ToList();
                for(int i = 0; i < empsReporters.Count; i++)
                {
                    if(i >= emp.DirectReports.Count)
                    {
                        return emp.ReportingStructureLToR;
                    }
                    // emp.ReportingStructure = empsReporters;
                    emp.ReportingStructureLToR = empsReporters;
                    emp.NumberOfReports++;
                    if (emp.DirectReports[i].DirectReports != null)
                    {
                        for (int j = 0; j < emp.DirectReports[i].DirectReports.Count; j++)
                        {
                            //Debug.Write(emp.DirectReports[i].FirstName);
                            emp.ReportingStructureLToR.Add(emp.DirectReports[i].DirectReports[j]);
                        }
                        Debug.WriteLine("_");
                        // emp.ReportingStructureLToR.AddRange(structBasedOffEmployee(empReporter));
                    }
                    else
                    {
                        // for(int i = 0; i < empReporter.DirectReports.Count; i++)
                        // {
                        //     Console.Write(empReporter.FirstName);
                        // 
                        // }
                        // Console.WriteLine();
                        // numberOfReports += getReportingStructure(empReporter);
                    }
                }
                //foreach (Employee empReporter in empsReporters)
                //{
                //    // emp.ReportingStructure = empsReporters;
                //    emp.ReportingStructureLToR = empsReporters;
                //    emp.NumberOfReports++;
                //    if (empReporter.DirectReports != null)
                //    {
                //        for (int i = 0; i < empReporter.DirectReports.Count; i++)
                //        {
                //            //Debug.Write(emp.DirectReports[i].FirstName);
                //            emp.ReportingStructureLToR.Add(empReporter.DirectReports[i]);
                //        }
                //        Debug.WriteLine("_");
                //        // emp.ReportingStructureLToR.AddRange(structBasedOffEmployee(empReporter));
                //    }
                //    else
                //    {
                //        // for(int i = 0; i < empReporter.DirectReports.Count; i++)
                //        // {
                //        //     Console.Write(empReporter.FirstName);
                //        // 
                //        // }
                //        // Console.WriteLine();
                //        // numberOfReports += getReportingStructure(empReporter);
                //    }
                //}
            }
            list = emp.ReportingStructureLToR;
            return list;
        }


        //public int getReportingStructure(Employee emp)
        //{
        //    if(emp.DirectReports != null)
        //    {
        //        List<Employee> empsReporters = emp.DirectReports.ToList();
        //        foreach(Employee empReporter in empsReporters)
        //        {
        //            emp.ReportingStructure = empsReporters;
        //            NumberOfReports++;
        //            if(empReporter.DirectReports.Count <= 0)
        //            {
        //                // break;
        //            }
        //            else
        //            {
        //                NumberOfReports += getReportingStructure(empReporter);
        //            }
        //        }
        //    }
        //    Console.WriteLine(NumberOfReports);
        //    return numberOfReports;
        //}
    }
}
