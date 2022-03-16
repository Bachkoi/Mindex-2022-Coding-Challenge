using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace challenge.Models
{
    public class Compensation
    {
        // Establish necessary fields
        [Key]
        public string CompensationEmployeesID { get; set; }

        public Employee CompensationEmp { get; set; }
        

        private double Compensationsalary { get; set; }

        
        private string CompensationeffectiveDate { get; set; }

        public Employee Employee
        {
            get
            {
                return CompensationEmp;
            }

            set
            {
                CompensationEmp = value;
            }
            
        }

        public double Salary
        {
            get
            {
                return Compensationsalary;
            }

            set
            {
                Compensationsalary = value;
            }
        }

        public string EffectiveDate
        {
            get
            {
                return CompensationeffectiveDate;
            }

            set
            {
                CompensationeffectiveDate = value;
            }
        }
        // Default constructor for Compensation objs, it will
        // create and assign blank or null values for the time being.
        public Compensation()
        {
            // Create a base compensation object with blank values.
            this.CompensationEmp = new Employee();
            this.Compensationsalary = 0.0;
            this.CompensationeffectiveDate = "";
        }



        // Compensation parameterized constructor based off of 
        // the employeeID passed in as a parameter.
        // From here, it will create the compensation based off of that 
        // employee's data.
        //public Compensation(Employee employeeId)
        //{
        //    // Get Employee based off of employeeID
        //    // this.emp = getByID(employeeId);
        //    // this.salary = this.emp.salary;
        //    // this.effectiveDate = this.emp.effectiveDate;
        //}

        // Default constructor for Compensation objs, it will
        // create and assign blank or null values for the time being.
        public Compensation(Employee employeeId, double inputSalary, string inputEffectiveDate)
        {
            // Create a base compensation object with blank values.
            this.CompensationEmp = employeeId;
            this.Compensationsalary = inputSalary;
            this.CompensationeffectiveDate = inputEffectiveDate;
        }

        public Compensation GetCompensation(Employee employeeId)
        {
            if(employeeId.EmployeeId == this.CompensationEmp.EmployeeId)
            {
                return this;
            }
            else
            {
                return null;
            }
        }

    }
}
