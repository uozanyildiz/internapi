namespace internapi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using internapi.Model;
    using internapi.Repository.IRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/manager")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepo;
        private readonly IMapper _mapper;

        public ManagerController(IManagerRepository managerRepo, IMapper mapper)
        {
            _managerRepo = managerRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetManagers()
        {
            var managerList = _managerRepo.GetManagers();
            var managerDto = new List<ManagerDto>();
            foreach (var manager in managerList)
            {
                managerDto.Add(_mapper.Map<ManagerDto>(manager));
            }
            return Ok(managerDto);
        }

        [HttpGet("byId/{managerId:int}")]
        public IActionResult GetManager(int managerId)
        {
            var manager = _managerRepo.GetManager(managerId);
            if (manager is null)
                return StatusCode(404);
            var managerDto = _mapper.Map<ManagerDto>(manager);
            return Ok(managerDto);
        }

        [HttpGet("byMail/{mail}")]
        public IActionResult GetManager(string mail)
        {
            var manager = _managerRepo.GetManager(mail);
            if (manager is null)
                return StatusCode(404);
            var managerDto = _mapper.Map<ManagerDto>(manager);
            return Ok(managerDto);
        }

        [HttpPost]
        public IActionResult CreateManager([FromBody] ManagerDto managerDto)
        {
            if (managerDto is null)
                return BadRequest(ModelState);

            if (!_managerRepo.ManagerExists(managerDto.Mail))
            {
                ModelState.AddModelError("", $"A manager already exists with mail {managerDto.Mail}");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var managerObj = _mapper.Map<Manager>(managerDto);

            if (!_managerRepo.CreateManager(managerObj))
            {
                ModelState.AddModelError("", $"Something wrong happened when adding manager {managerObj.Mail}");
            }
            return Ok(managerObj);
        }

        [HttpPatch]
        public IActionResult UpdateManager([FromBody] ManagerDto managerDto)
        {
            if (managerDto is null)
                return BadRequest(ModelState);

            if (!_managerRepo.ManagerExists(managerDto.Id))
            {
                ModelState.AddModelError("", $"Manager doesnt exist with id {managerDto.Id}");
                return StatusCode(404, ModelState);
            }

            var managerObj = _mapper.Map<Manager>(managerDto);

            if (!_managerRepo.UpdateManager(managerObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating manager with id {managerObj.Id}");
                return StatusCode(404, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{managerId:int}")]
        public IActionResult RemoveManager(int managerId)
        {
            if (!_managerRepo.ManagerExists(managerId))
            {
                ModelState.AddModelError("", $"Manager with id of {managerId} doesn't exist");
            }

            var managerObj = _managerRepo.GetManager(managerId);

            if (!_managerRepo.RemoveManager(managerObj))
            {
                ModelState.AddModelError("", $"Something went wrong when deleting manager with id {managerId}");
                return StatusCode(404, ModelState);
            }

            return NoContent();
        }
    }
}