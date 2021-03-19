namespace internapi.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using internapi.Repository.IRepository;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/company")]
    [ApiController]
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
            return Ok(_companyRepo.GetCompanies());
        }
    }
}