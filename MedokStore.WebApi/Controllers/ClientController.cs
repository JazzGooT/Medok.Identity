using AutoMapper;
using MedokStore.Application.Users.Command.ChangePassword;
using MedokStore.Application.Users.Command.ConfirmEmail;
using MedokStore.Application.Users.Command.SendMessageEmail;
using MedokStore.Application.Users.Command.SendMessagePassword;
using MedokStore.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedokStore.Identity.Controllers
{
    [Route("/[controller]")]
    [Authorize(Policy = "Client")]
    public class ClientController : BaseController
    {
        private readonly IMapper _mapper;
        public ClientController(IMapper mapper) => _mapper = mapper;
        [HttpPost("confirm/email")]
        public async Task<IActionResult> SendMessageEmail()
        {
            var command = new SendMessageEmailCommand
            {
                AccessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""),
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("confirm/email/verify")]
        public async Task<IActionResult> ConfirmEmail(string code)
        {
            var command = new ConfirmEmailCommand
            {
                Code = code,
                AccessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "")
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("change/password")]
        public async Task<IActionResult> SendMessagePassword()
        {
            var command = new SendMessagePasswordCommand
            {
                AccessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""),
            };
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("change/password/confirm")]
        public async Task<IActionResult> ChangePassword([FromQuery] ChangePasswordDto changePasswordDto)
        {
            var command = _mapper.Map<ChangePasswordCommand>(changePasswordDto);
            command.AccessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await Mediator.Send(command);
            return Ok(result);
        }

    }
}
