using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KocUniversityCourseManagement.Application.Interfaces;
using KocUniversityCourseManagement.Application.Services;
using KocUniversityCourseManagement.Domain;
using KocUniversityCourseManagement.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KocUniversityCourseManagement.Presentation.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUserInterface _userService;
        private readonly IKeycloakService _keycloakService;

        public UsersController(IUserInterface userService, IKeycloakService keycloakService)
        {
            _userService = userService;
            _keycloakService = keycloakService;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var result =  await _keycloakService.RegisterUserAsync(model);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}

