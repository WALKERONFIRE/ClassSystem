using ClassSystem.Core;
using ClassSystem.Core.Consts;
using ClassSystem.Core.Interfaces;
using ClassSystem.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoursesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return  Ok(await _unitOfWork.Courses.GetByIdAsync(id));
        }
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _unitOfWork.Courses.GetAllAsync());
        }
        [HttpGet("FindAsync")]
        public async Task<IActionResult> FindByNameAsync(string name)
        {
            return Ok(await _unitOfWork.Courses.FindAsync(x => x.Name == name, new[] {"Doctor"}));
        }
        [HttpGet("FindAllAsync")]
        public async Task<IActionResult> FindAllByNameAsync(string name)
        {
            return Ok(await _unitOfWork.Courses.FindAllAsync(x => x.Doctor.Name.Contains(name), new[] { "Doctor" }));
        }

        [HttpGet("FindAllOrderedAsync")]
        public async Task<IActionResult> FindAllOrderedAsync(string name)
        {
            return Ok(await _unitOfWork.Courses.FindAllAsync(x => x.Doctor.Name.Contains(name), null, null , x=>x.Id )) ;
        }
    }
}
