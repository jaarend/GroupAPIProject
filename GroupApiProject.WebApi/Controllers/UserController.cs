using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using GroupApiProject.Models.Responses;
using GroupApiProject.Models.Token;
using GroupApiProject.Models.User;
using GroupApiProject.Services.Token;
using GroupApiProject.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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

        private readonly ITokenService _tokenService;

        public UserController(IUserService userService
        , ITokenService tokenService
        )
        {
            _userService = userService;
            _tokenService = tokenService;
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
                // var user = await _tokenService
                return Ok(new TextResponse("User was registered."));
            }

            return BadRequest();
        }

        // added for tokens
        [Authorize, HttpGet("{userId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int userId)
        {
            var userDetail = await _userService.GetUserByIdAsync(userId);

            if (userDetail is null)
            {
                return NotFound();
            }

            return Ok(userDetail);
        }

        [HttpPost("~/api/Token")]
        public async Task<IActionResult> GetToken([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TokenResponse? response = await _tokenService.GetTokenAsync(request);

            if (response is null)
                return BadRequest(new TextResponse("Invalid username or password."));

            return Ok(response);
        }
    }
}