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

        public Compensation()
        {
            
        }
        public Compensation(String employeeId)
        {
            // Get Employee based off of employeeID
            // emp = getByID(employeeId);
            // 
        }
    }
}
