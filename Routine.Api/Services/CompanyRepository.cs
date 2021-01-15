using Microsoft.EntityFrameworkCore;
using Routine.Api.Data;
using Routine.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutineDbContext _context;
        public CompanyRepository(RoutineDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void AddCompany(Company company)
        {
            //throw new NotImplementedException();
            if(company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            company.Id = Guid.NewGuid();

            foreach (var employee in company.Employees)
            {
                employee.Id = Guid.NewGuid();
            }

            _context.Companies.Add(company);
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            //throw new NotImplementedException();
            if(companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if(employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            employee.CompanyId = companyId;
            _context.Employees.Add(employee);
        }

        public async Task<bool> CompanyExistAsync(Guid companyId)
        {
            //throw new NotImplementedException();
            if(companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Companies.AnyAsync(x => x.Id == companyId);
        }

        public void DeleteCompany(Company company)
        {
            //throw new NotImplementedException();
            if(company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }

            _context.Companies.Remove(company);
        }

        public void DeleteEmployee(Employee employee)
        {
            //throw new NotImplementedException();
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _context.Employees.Remove(employee);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            //throw new NotImplementedException();
            return await _context.Companies.ToListAsync();
        }

        public async Task<IEnumerable<Company>> 
            GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            //throw new NotImplementedException();
            if(companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }
            return await _context.Companies
                .Where(x => companyIds.Contains(x.Id))
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            //throw new NotImplementedException();
            if(companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies
                .FirstOrDefaultAsync(x => x.Id == companyId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId)
        {
            //throw new NotImplementedException();
            if(companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            return await _context.Employees
                .Where(x => x.CompanyId == companyId)
                .OrderBy(x => x.EmployeeNo)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }

            if(employeeId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            return await _context.Employees
                .Where(x => x.CompanyId == companyId && x.Id == employeeId)
                .FirstOrDefaultAsync();
            //throw new NotImplementedException();
        }


        public async Task<bool> SaveAsync()
        {
            //throw new NotImplementedException();
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateCompany(Company company)
        {
            //throw new NotImplementedException();

            //_context.Entry(company).State = EntityState.Modified;
        }

        public void UpdateEmployee(Employee employee)
        {
            //throw new NotImplementedException();

            //_context.Entry(employee).State = EntityState.Modified;
        }
    }
}
