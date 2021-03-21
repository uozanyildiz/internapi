using System.Security.Claims;
using System.Linq;
using System;
namespace internapi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using internapi.Model;
    using internapi.Repository.IRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/internship")]
    [ApiController]
    [Authorize]
    public class InternshipController : ControllerBase
    {
        private readonly IInternshipRepository _internshipRepo;
        private readonly IMapper _mapper;

        public InternshipController(IInternshipRepository internshipRepo, IMapper mapper)
        {
            _internshipRepo = internshipRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetInternships()
        {
            var internshipList = _internshipRepo.GetInternships();
            var internshipDtoList = new List<InternshipDto>();
            foreach (var internship in internshipList)
            {
                internshipDtoList.Add(_mapper.Map<InternshipDto>(internship));
            }
            return Ok(internshipDtoList);
        }

        [HttpGet("byId/{internshipId:int}")]
        public IActionResult GetInternship(int internshipId)
        {
            var internship = _internshipRepo.GetInternship(internshipId);
            if (internship is null)
                return StatusCode(404);
            var internshipDto = _mapper.Map<InternshipDto>(internship);
            return Ok(internshipDto);
        }

        [HttpGet("byCompanyId/{companyId:int}")]
        public IActionResult GetInternshipByCompanyID(int companyId)
        {
            var internshipList = _internshipRepo.GetInternshipsByCompanyId(companyId);

            if (internshipList.Count <= 0)
                return StatusCode(404);

            var internshipDtoList = new List<InternshipDto>();
            foreach (var internship in internshipList)
            {
                internshipDtoList.Add(_mapper.Map<InternshipDto>(internship));
            }
            return Ok(internshipDtoList);
        }

        [HttpGet("byStudentId/{studentId:int}")]
        public IActionResult GetInternshipByStudentID(int studentId)
        {
            var internshipList = _internshipRepo.GetInternshipByStudentId(studentId);

            if (internshipList.Count <= 0)
                return StatusCode(404);

            var internshipDtoList = new List<InternshipDto>();
            foreach (var internship in internshipList)
            {
                internshipDtoList.Add(_mapper.Map<InternshipDto>(internship));
            }
            return Ok(internshipDtoList);
        }

        [HttpGet("byDate/start={startDate}&end={endDate}")]
        public IActionResult GetInternshipByStudentID(string startDate, string endDate)
        {

            var startDateTime = new DateTime();
            var endDateTime = new DateTime();

            var startDateResult = DateTime.TryParse(startDate, out startDateTime);
            var endDateResult = DateTime.TryParse(endDate, out endDateTime);

            if (!startDateResult || !endDateResult)
            {
                ModelState.AddModelError("", $"One of the following dates are couldn't be parsed: {startDate} - {endDate}");
                return StatusCode(404, ModelState);
            }


            var internshipList = _internshipRepo.GetInternshipByDate(startDateTime, endDateTime);

            if (internshipList.Count <= 0)
                return StatusCode(404);

            var internshipListDto = new List<InternshipDto>();

            foreach (var internship in internshipList)
            {
                internshipListDto.Add(_mapper.Map<InternshipDto>(internship));
            }

            return Ok(internshipListDto);
        }

        [HttpPost]
        public IActionResult CreateInternship([FromBody] InternshipDto internshipDto)
        {
            //Checking if user creating an internship for himself or not
            var isIdSafe = User.Claims.Any(x => x.Type == ClaimTypes.Sid && x.Value == internshipDto.StudentId.ToString());
            if (User.IsInRole("Student") && !isIdSafe)
                return Forbid();

            if (internshipDto is null)
                return BadRequest();
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);

            var internshipObj = _mapper.Map<Internship>(internshipDto);

            if (!_internshipRepo.CreateInternship(internshipObj))
            {
                ModelState.AddModelError("", $"Something went wrong when adding internship");
                return StatusCode(404, ModelState);
            }
            return Ok(internshipObj);
        }

        [HttpPatch]
        public IActionResult UpdateInternship([FromBody] InternshipDto internshipDto)
        {
            //Checking if user creating an internship for himself or not
            var isIdSafe = User.Claims.Any(x => x.Type == ClaimTypes.Sid && x.Value == internshipDto.StudentId.ToString());
            if (User.IsInRole("Student") && !isIdSafe)
                return Forbid();

            if (internshipDto is null)
                return BadRequest();
            if (!_internshipRepo.InternshipExists(internshipDto.Id))
            {
                ModelState.AddModelError("", $"Internship doesn't exists with id of {internshipDto.Id}");
                return StatusCode(404, ModelState);
            }

            var internshipObj = _mapper.Map<Internship>(internshipDto);

            if (!_internshipRepo.UpdateInternship(internshipObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating model with id of {internshipObj.Id}");
                return StatusCode(404, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{internshipId}")]
        public IActionResult DeleteInternship(int internshipId)
        {

            if (!_internshipRepo.InternshipExists(internshipId))
            {
                ModelState.AddModelError("", $"Internship doesn't exists with id of {internshipId}");
                return StatusCode(404, ModelState);
            }

            var internshipObj = _internshipRepo.GetInternship(internshipId);

            //Checking if user creating an internship for himself or not
            var isIdSafe = User.Claims.Any(x => x.Type == ClaimTypes.Sid && x.Value == internshipObj.StudentId.ToString());
            if (User.IsInRole("Student") && !isIdSafe)
                return Forbid();

            if (!_internshipRepo.DeleteInternship(internshipObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating model with id of {internshipId}");
                return StatusCode(404, ModelState);
            }

            return NoContent();
        }

        [HttpPost("addMultiple")]
        public IActionResult CreateMultipleInternships([FromBody] List<InternshipDto> internshipList)
        {
            if (internshipList.Count <= 0 && internshipList is null)
                return BadRequest();
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);
            var internshipObjList = new List<Internship>();
            foreach (var internshipDto in internshipList)
            {
                internshipObjList.Add(_mapper.Map<Internship>(internshipDto));
            }
            if (!_internshipRepo.CreateMultipleInternships(internshipObjList))
            {
                ModelState.AddModelError("", $"Something went wrong when adding internships");
            }
            return Ok(internshipObjList);
        }
    }
}