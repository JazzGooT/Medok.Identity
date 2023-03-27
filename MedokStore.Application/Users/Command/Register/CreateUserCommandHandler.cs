using AutoMapper;
using MediatR;
using MedokStore.Application.Common.Helpers;
using MedokStore.Domain.Entity;
using Microsoft.AspNetCore.Identity;

namespace MedokStore.Application.Users.Command.Register
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserVm>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
            => (_userManager, _mapper) = (userManager, mapper);
        public async Task<CreateUserVm> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request);
            var resultCreate = await _userManager.CreateAsync(user, request.Password);
            if (resultCreate.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Client");
                await _userManager.UpdateAsync(user);
                var token = TokenManager.GenerateToken(user, "Client");
                var result = new CreateUserVm
                {
                    Result = resultCreate.ToString(),
                    AccessToken = token.Token,
                    Expires = token.Expires,
                };
                return result;
            }
            else
            {
                var result = new CreateUserVm
                {
                    Result = resultCreate.ToString(),
                    AccessToken = null,
                    Expires = null
                };
                return result;
            }
        }
    }

}
