using AutoMapper;
using MedokStore.Application.Common.Mappings;
using MedokStore.Application.Users.Command.ChangePassword;
using System.ComponentModel.DataAnnotations;

namespace MedokStore.Identity.Models
{
    public class ChangePasswordDto : IMapWith<ChangePasswordCommand>
    {
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "NewPassword is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangePasswordDto, ChangePasswordCommand>()
                .ForMember(changePasswordCommand => changePasswordCommand.Password, opt => opt.MapFrom(changePasswordDto => changePasswordDto.Password))
                .ForMember(changePasswordCommand => changePasswordCommand.NewPassword, opt => opt.MapFrom(changePasswordDto => changePasswordDto.NewPassword))
                .ForMember(changePasswordCommand => changePasswordCommand.Code, opt => opt.MapFrom(changePasswordDto => changePasswordDto.Code));
        }
    }
}
