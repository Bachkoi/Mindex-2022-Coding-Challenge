using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }
        public List<Employee> SeedStructured()
        {
            EmployeeDataSeeder empDS = new EmployeeDataSeeder(_employeeContext);
            List<Employee> test = empDS.PublicLoad();
            return test;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Compensation CompensationGetById(string id)
        {
            // return _employeeContext.Compensations.SingleOrDefault(e => e.Employee.EmployeeId == id); // gets a 204 error
            Compensation comp = _employeeContext.Compensations.SingleOrDefault(e => e.CompensationEmployeesID == id);
            comp.Employee = GetById(id);
            return comp; // When I do this it returns the salary, effective data, and an empty EMployee

            // return _employeeContext.Compensations.SingleOrDefault(e => e.CompensationEmployeesID == id); // When I do this it returns the salary, effective data, and an empty EMployee

        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        public Compensation Add(Compensation comp)
        {
            comp.Employee.EmployeeId = Guid.NewGuid().ToString();
             _employeeContext.Compensations.Add(comp);
            return comp;
        }
    }
}
