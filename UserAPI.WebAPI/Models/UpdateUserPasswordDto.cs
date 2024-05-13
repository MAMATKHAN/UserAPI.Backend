using AutoMapper;
using UserAPI.Application.Common.Mappings;
using UserAPI.Application.Users.Commands.UpdateUserPassword;

namespace UserAPI.WebAPI.Models
{
	public class UpdateUserPasswordDto : IMapWith<UpdateUserPasswordCommand>
	{
		public Guid UserId { get; set; }
		public string Password { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<UpdateUserPasswordDto, UpdateUserPasswordCommand>()
				.ForMember(updateUserPasswordCommand => updateUserPasswordCommand.UserId,
					opt => opt.MapFrom(updateUserPasswordDto => updateUserPasswordDto.UserId))
				.ForMember(updateUserPasswordCommand => updateUserPasswordCommand.Password,
					opt => opt.MapFrom(updateUserPasswordDto => updateUserPasswordDto.Password));
		}
	}
}
