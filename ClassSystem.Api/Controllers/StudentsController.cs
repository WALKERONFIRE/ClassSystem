using ClassSystem.Core;
using ClassSystem.Core.Interfaces;
using ClassSystem.Core.Models;
using ClassSystem.EF.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await _unitOfWork.Students.GetByIdAsync(id));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _unitOfWork.Students.GetAllAsync());
        }
        [HttpGet("FindByName")]
        public async Task<IActionResult> FindByNameAsync(string name)
        {
            return Ok(await _unitOfWork.Students.FindAsync(x=>x.Name== name));
        }
        [HttpPost("AddOneAsync")]
        public async Task<IActionResult> AddOneAsync(StudentDTO dto)
        {
            var student = await _unitOfWork.Students.AddAsync(new Student { Name = dto.Name });
            _unitOfWork.Complete();
            return Ok(student);
        }

       


    }
}
