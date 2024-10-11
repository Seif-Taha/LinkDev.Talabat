using LinkDev.Talabat.Core.Application.Abstraction.Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Employees
{
    public interface IEmployeeService
    {

        Task<IEnumerable<EmployeeToReturnDto>> GetEmployeesAsync();

        Task<EmployeeToReturnDto> GetEmployeeAsync(int id);

    }
}
