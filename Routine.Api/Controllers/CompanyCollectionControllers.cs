using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routine.Api.Entities;
using Routine.Api.Helpers;
using Routine.Api.Services;
using Routine.Api.Models;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("api/companycollections")]
    public class CompanyCollectionControllers : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly ICompanyRepository _companyRepository;
      
        public CompanyCollectionControllers(IMapper mapper,ICompanyRepository companyRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _companyRepository = companyRepository 
                                 ?? throw new ArgumentNullException(nameof(companyRepository));

        }

        //1,2,3,4
        // key1=value1,key2=value2,key3=value3
        [HttpGet("({ids})",Name = nameof(GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection(
            [FromRoute] [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var entities = await _companyRepository.GetCompaniesAsync(ids);

            if (ids.Count() != entities.Count())
            {
                return NotFound();
            }

            var dtoTosReturn = _mapper.Map<IEnumerable<CompanyDto>>(entities);

            return Ok(dtoTosReturn);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection(
            IEnumerable<CompanyAddDto> companyCollection)
        {
            var companyEntities = _mapper.Map<IEnumerable<Company>>(companyCollection);

            foreach (var company in companyEntities)
            {
                _companyRepository.AddCompany(company);
            }

            await _companyRepository.SaveAsync();
            
            var dtosToReturn = _mapper.Map<IEnumerable<CompanyDto>>(companyEntities);

            var idsString = string.Join(",", dtosToReturn.Select(x => x.Id));

            return CreatedAtRoute(nameof(GetCompanyCollection),
                new {ids=idsString},dtosToReturn);
        }
    }
}
