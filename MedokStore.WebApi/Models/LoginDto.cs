using AutoMapper;
using MedokStore.Application.Common.Mappings;
using MedokStore.Application.Users.Command.Login;
using System.ComponentModel.DataAnnotations;

namespace MedokStore.Identity.Models
{
    public class LoginDto : IMapWith<LoginUserCommand>
    {
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginDto, LoginUserCommand>()
                .ForMember(loginUserCommand => loginUserCommand.UserName, opt => opt.MapFrom(loginDto => loginDto.UserName))
                .ForMember(loginUserCommand => loginUserCommand.Password, opt => opt.MapFrom(loginDto => loginDto.Password));
        }
    }
}