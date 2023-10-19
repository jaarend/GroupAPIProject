using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.User;
using GroupApiProject.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;

namespace GroupApiProject.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser ([FromBody] RegisterUser model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerResult = await _userService.RegisterUserAsync(model);
            if(registerResult)
            {
                var TextResponse = "User was registered";
                return Ok(TextResponse);
            }

            return BadRequest();
        }
    }
}