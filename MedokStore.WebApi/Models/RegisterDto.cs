using AutoMapper;
using MedokStore.Application.Common.Mappings;
using MedokStore.Application.Users.Command.Register;
using System.ComponentModel.DataAnnotations;

namespace MedokStore.Identity.Models
{
    public class RegisterDto : IMapWith<CreateUserCommand>
    {
        [Required(ErrorMessage = "Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<RegisterDto, CreateUserCommand>()
                .ForMember(createUserCommand => createUserCommand.UserName, opt => opt.MapFrom(registerDto => registerDto.UserName))
                .ForMember(createUserCommand => createUserCommand.LastName, opt => opt.MapFrom(registerDto => registerDto.LastName))
                .ForMember(createUserCommand => createUserCommand.Email, opt => opt.MapFrom(registerDto => registerDto.Email))
                .ForMember(createUserCommand => createUserCommand.Password, opt => opt.MapFrom(registerDto => registerDto.Password));
        }
    }
}
