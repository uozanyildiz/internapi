namespace internapi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using internapi.Model;
    using internapi.Repository.IRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/company")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository companyRepo, IMapper mapper)
        {
            _companyRepo = companyRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companyList = _companyRepo.GetCompanies();
            var companyDtoList = new List<CompanyDto>();
            foreach (var company in companyList)
            {
                companyDtoList.Add(_mapper.Map<CompanyDto>(company));
            }
            return Ok(companyDtoList);
        }

        [HttpGet("byId/{companyId:int}")]
        public IActionResult GetCompany(int companyId)
        {
            var company = _companyRepo.GetCompany(companyId);
            if (company is null)
                return StatusCode(404);
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }


        [HttpGet("byName/{companyName}")]
        public IActionResult GetCompany(string companyName)
        {
            var company = _companyRepo.GetCompany(companyName);
            if (company is null)
                return StatusCode(404);
            var companyDto = _mapper.Map<CompanyDto>(company);
            return Ok(companyDto);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyDto companyDto)
        {
            //Validations 
            if (companyDto == null)
                return BadRequest(ModelState);
            if (_companyRepo.CompanyExists(companyDto.Name))
            {
                ModelState.AddModelError("", "A company with the same name already exists");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyObj = _mapper.Map<Company>(companyDto);
            if (!_companyRepo.CreateCompany(companyObj))
            {
                ModelState.AddModelError("", $"Something happened when adding company {companyObj.Name}");
                return StatusCode(500, ModelState);
            }
            return Ok(companyObj);
        }

        [HttpPatch]
        public IActionResult UpdateCompany([FromBody] CompanyDto companyDto)
        {
            if (companyDto == null && !_companyRepo.CompanyExists(companyDto.Id))
                return BadRequest(ModelState);

            var companyObj = _mapper.Map<Company>(companyDto);

            if (!_companyRepo.UpdateCompany(companyObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating company {companyObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCompany(int id)
        {
            if (!_companyRepo.CompanyExists(id))
            {
                ModelState.AddModelError("", $"Company doesnt exists with id {id}");
                return StatusCode(404, ModelState);
            }

            var companyObj = _companyRepo.GetCompany(id);
            if (!_companyRepo.RemoveCompany(companyObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating company {companyObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPost("createMultiple")]
        public IActionResult CreateMultipleCompany([FromBody] List<CompanyDto> companyDtoList)
        {
            //Validations 
            if (companyDtoList == null && companyDtoList.Count <= 0)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var companyObjList = new List<Company>();
            foreach (var company in companyDtoList)
            {
                companyObjList.Add(_mapper.Map<Company>(company));
            }
            if (!_companyRepo.CreateMultipleCompany(companyObjList))
            {
                ModelState.AddModelError("", $"Something happened when adding multiple companies");
                return StatusCode(500, ModelState);
            }
            return Ok(companyObjList);
        }
    }
}