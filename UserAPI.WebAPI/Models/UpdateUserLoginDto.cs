using AutoMapper;
using UserAPI.Application.Common.Mappings;
using UserAPI.Application.Users.Commands.UpdateUserLogin;

namespace UserAPI.WebAPI.Models
{
	public class UpdateUserLoginDto : IMapWith<UpdateUserLoginCommand>
	{
		public Guid UserId { get; set; }
		public string Login { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<UpdateUserLoginDto, UpdateUserLoginCommand>()
				.ForMember(updateUserLoginCommand => updateUserLoginCommand.UserId,
					opt => opt.MapFrom(updateUserLoginDto => updateUserLoginDto.UserId))
				.ForMember(updateUserLoginCommand => updateUserLoginCommand.Login,
					opt => opt.MapFrom(updateUserLoginDto => updateUserLoginDto.Login));
		}
	}
}
