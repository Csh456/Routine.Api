using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Services;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    //[Route("api/[controller]")]   不建议，因为controller可能更改
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public CompaniesController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository ??
                                     throw new ArgumentNullException(nameof(companyRepository));
        }
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await companyRepository.GetCompaniesAsync();
            //return new JsonResult(companies);
            return Ok(companies);//返回202
            //return NotFound(companies);//返回404
        }
        [HttpGet("{companyId}")]    //api/Companies/{companyId}
        public async Task<IActionResult> GetCompany(Guid companyId)
        {
            //var exist = await companyRepository.CompanyExistAsync(companyId);
            //if (!exist)
            //{
            //    return NotFound();
            //}
            var company = await companyRepository.GetCompanyAsync(companyId);
            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }
    }
}
