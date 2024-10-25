using CQRS_Practice.DTOs;
using CQRS_Practice.Repository;
using CQRS_Practice.Utility;
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
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthController(IUserRepository userRepository, JwtTokenHelper jwtTokenHelper = null)
        {
            _userRepository = userRepository;
            _jwtTokenHelper = jwtTokenHelper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto loginDto)
        {
            var user = await _userRepository.Authenticate(loginDto.Username, loginDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = await Task.Run(() => _jwtTokenHelper.GenerateToken(user));

            // Return the token wrapped in a JSON object
            return Ok(new { Token = token });
        }

    }
}
