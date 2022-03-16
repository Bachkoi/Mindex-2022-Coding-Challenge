using challenge.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace challenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Compensation CompensationGetById(string id);
        Employee Add(Employee employee);
        Compensation Add(Compensation comp);

        Employee Remove(Employee employee);
        Task SaveAsync();
        List<Employee> SeedStructured();

    }
}