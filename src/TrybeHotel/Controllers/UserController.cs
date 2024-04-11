using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("user")]

    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        public IActionResult GetUsers(){
            try
            {
                var myUsers = _repository.GetUsers();
                return Ok(myUsers);
            }
            catch (Exception e)
            {
                return Unauthorized(new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserDtoInsert user)
        {
            try
            {
                return Created("", _repository.Add(user));
            }
            catch (Exception e)
            {
                
                return Conflict(new { message = e.Message });
            }
        }
    }
}