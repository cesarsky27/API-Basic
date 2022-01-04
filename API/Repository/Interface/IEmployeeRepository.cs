using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Employee Get(String NIK);
        int Insert(Employee employee);
        int Update(string NIK, Employee employee);
        int Delete(String NIK);
    }
}
