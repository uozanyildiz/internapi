using System.Linq;
using System;
using System.Runtime.InteropServices.ComTypes;
namespace internapi.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using internapi.Model;
    using internapi.Repository.IRepository;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/student")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepository studentRepo, IMapper mapper)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult GetStudents()
        {
            var objList = _studentRepo.GetStudents();
            var objDto = new List<StudentDto>();
            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<StudentDto>(obj));
            }
            return Ok(objDto);
        }
        [HttpGet("byId/{studentId:int}")]
        public IActionResult GetStudentById(int studentId)
        {
            var obj = _studentRepo.GetStudent(studentId);
            if (obj is null)
                return NotFound();
            var objDto = _mapper.Map<StudentDto>(obj);
            return Ok(objDto);
        }

        [HttpGet("byName/name={name}&surname={surname}")]
        public IActionResult GetStudentByName(string name, string surname)
        {
            var obj = _studentRepo.GetStudent(name, surname);
            if (obj is null)
                return NotFound();
            var objDto = _mapper.Map<StudentDto>(obj);
            return Ok(objDto);
        }

        [HttpGet("byMail/{mail}")]
        public IActionResult GetStudentByMail(string mail)
        {
            var obj = _studentRepo.GetStudent(mail);
            if (obj is null)
                return NotFound();
            var objDto = _mapper.Map<StudentDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] StudentDto studentDto)
        {
            //Validations 
            if (studentDto == null)
                return BadRequest(ModelState);
            if (_studentRepo.StudentExists(studentDto.Mail))
            {
                ModelState.AddModelError("", "E-mail is already registered");
                return StatusCode(404, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var studentObj = _mapper.Map<Student>(studentDto);
            if (!_studentRepo.CreateStudent(studentObj))
            {
                ModelState.AddModelError("", $"Something happened when adding student {studentObj.Name} {studentObj.Surname}");
                return StatusCode(500, ModelState);
            }
            return Ok(studentObj);

        }

        [HttpPatch]
        public IActionResult UpdateStudent([FromBody] StudentDto studentDto)
        {
            var isIdSafe = User.Claims.Any(x => x.Type == ClaimTypes.Sid && x.Value == studentDto.Id.ToString());

            //If student is trying to update an ID other than himself, deny it.
            if (User.IsInRole("Student") && !isIdSafe)
                return Forbid();

            if (studentDto == null && !_studentRepo.StudentExists(studentDto.Id))
                return BadRequest(ModelState);
            if (!_studentRepo.StudentExists(studentDto.Mail))
            {
                ModelState.AddModelError("", $"Student doesnt exists with mail {studentDto.Mail}");
                return StatusCode(404, ModelState);
            }

            var studentObj = _mapper.Map<Student>(studentDto);
            if (!_studentRepo.UpdateStudent(studentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating student {studentObj.Name} {studentObj.Surname}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudent(int id)
        {
            if (!_studentRepo.StudentExists(id))
            {
                ModelState.AddModelError("", $"Student doesnt exists with id {id}");
                return StatusCode(404, ModelState);
            }

            var studentObj = _studentRepo.GetStudent(id);
            if (!_studentRepo.RemoveStudent(studentObj))
            {
                ModelState.AddModelError("", $"Something went wrong when updating student {studentObj.Name} {studentObj.Surname}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("addMultiple")]
        public IActionResult CreateMultipleStudents([FromBody] List<StudentDto> studentList)
        {
            if (studentList.Count <= 0 && studentList is null)
                return BadRequest();
            if (!ModelState.IsValid)
                return StatusCode(404, ModelState);
            var studentObjList = new List<Student>();
            foreach (var studentDto in studentList)
            {
                studentObjList.Add(_mapper.Map<Student>(studentDto));
            }
            if (!_studentRepo.CreateMultipleStudents(studentObjList))
            {
                ModelState.AddModelError("", $"Something went wrong when adding students");
                return StatusCode(500, ModelState);
            }
            return Ok(studentObjList);
        }

    }
}