using CQRS_Practice.Command;
using CQRS_Practice.DTOs;
using CQRS_Practice.Repository;
using CQRS_Practice.Utility;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePractice1.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediatar;

        public AuthController(IMediator mediator)
        {
            _mediatar = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginDto)
        {
            var token = await _mediatar.Send(new LoginCommand(loginDto));
            if (token == null)
            {
                return Unauthorized();
            }

           // var token = await Task.Run(() => _jwtTokenHelper.GenerateToken(user));

            // Return the token wrapped in a JSON object
            return Ok(new { Token = token });
        }

    }
}
