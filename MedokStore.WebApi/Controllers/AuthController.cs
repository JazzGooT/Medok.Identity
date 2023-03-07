using AutoMapper;
using MedokStore.Application.Users.Command.Login;
using MedokStore.Application.Users.Command.Register;
using MedokStore.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedokStore.Identity.Controllers
{
    [Route("/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMapper _mapper;
        public AuthController(IMapper mapper) => _mapper = mapper;
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromQuery] CreateUserDto createUserDto)
        {
            var command = new CreateUserCommand
            {
                UserName = createUserDto.UserName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
            };
            var result = await Mediator.Send(command);
            if (result.Succeeded) return Ok("Succeeded");
            return BadRequest(result.Errors);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginUserDto createUserDto)
        {
            var command = new LoginUserCommand
            {
                Email = createUserDto.Email,
                Password = createUserDto.Password,
            };
            await Mediator.Send(command);
            return Ok("Login");
        }

    }
}
