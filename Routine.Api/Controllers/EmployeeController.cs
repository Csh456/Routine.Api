using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routine.Api.Services;
using Routine.Api.Models;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/employees")]
    public class EmployeeController:ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICompanyRepository companyRepository;

        public EmployeeController(IMapper mapper,ICompanyRepository companyRepository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.companyRepository = companyRepository 
                                    ?? throw new ArgumentNullException(nameof(companyRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> 
            GetEmployeesForCompany(Guid companyId,
                [FromQuery(Name ="gender")] string genderDisplay,string q)
        {
            if (! await companyRepository.CompanyExistAsync(companyId))
            {
                return NotFound();
            }
            var employees = await companyRepository.GetEmployeesAsync(companyId,genderDisplay,q);

            var employeeDtos = mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);
        }
        [HttpGet("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>>
            GetEmployeeForCompany(Guid companyId,Guid employeeId)
        {
            if (!await companyRepository.CompanyExistAsync(companyId))
            {
                return NotFound();
            }

            var employee = await companyRepository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }

         

            var employeeDto = mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }
    }
}
