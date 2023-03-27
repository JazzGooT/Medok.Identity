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
        public async Task<IActionResult> Register([FromQuery] RegisterDto createUserDto)
        {
            var command = _mapper.Map<CreateUserCommand>(createUserDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromQuery] LoginDto loginUserDto)
        {
            var command = _mapper.Map<LoginUserCommand>(loginUserDto);
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}