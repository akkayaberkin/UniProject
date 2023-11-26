using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KocUniversityCourseManagement.Application.Interfaces;
using KocUniversityCourseManagement.Application.Services;
using KocUniversityCourseManagement.Domain;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KocUniversityCourseManagement.Presentation.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserInterface _userService;

        public UsersController(IUserInterface userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUser user)
        {
            if (user == null)
                return BadRequest("User information is required");

            await _userService.CreateUserAsync(user);

            return Ok();
        }
    }
}

