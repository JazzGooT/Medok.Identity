using AutoMapper;
using MediatR;
using MedokStore.Application.Common.Mappings;
using MedokStore.Domain.Entity;

namespace MedokStore.Application.Users.Command.Register
{
    public class CreateUserCommand : IRequest<CreateUserVm>, IMapWith<ApplicationUser>
    {
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserCommand, ApplicationUser>()
                .ForMember(applicationUser => applicationUser.UserName, opt => opt.MapFrom(createUserCommand => createUserCommand.UserName))
                .ForMember(applicationUser => applicationUser.LastName, opt => opt.MapFrom(createUserCommand => createUserCommand.LastName))
                .ForMember(applicationUser => applicationUser.Email, opt => opt.MapFrom(createUserCommand => createUserCommand.Email));
        }
    }
}
