using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    public class Compensation
    {
        // Establish necessary fields
        private Employee emp;

        private double salary;

        private string effectiveDate;

        // Default constructor for Compensation objs, it will
        // create and assign blank or null values for the time being.
        public Compensation()
        {
            // Create a base compensation object with blank values.
            this.emp = new Employee();
            this.salary = 0.0;
            this.effectiveDate = "";
        }



        // Compensation parameterized constructor based off of 
        // the employeeID passed in as a parameter.
        // From here, it will create the compensation based off of that 
        // employee's data.
        public Compensation(Employee employeeId)
        {
            // Get Employee based off of employeeID
            // this.emp = getByID(employeeId);
            // this.salary = this.emp.salary;
            // this.effectiveDate = this.emp.effectiveDate;
        }

        // Default constructor for Compensation objs, it will
        // create and assign blank or null values for the time being.
        public Compensation(Employee employeeId, double inputSalary, string inputEffectiveDate)
        {
            // Create a base compensation object with blank values.
            this.emp = employeeId;
            this.salary = inputSalary;
            this.effectiveDate = inputEffectiveDate;
        }

        public Compensation GetCompensation(Employee employeeId)
        {
            if(employeeId.EmployeeId == this.emp.EmployeeId)
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
