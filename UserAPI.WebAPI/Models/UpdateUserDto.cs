using AutoMapper;
using UserAPI.Application.Common.Mappings;
using UserAPI.Application.Users.Commands.UpdateUser;

namespace UserAPI.WebAPI.Models
{
	public class UpdateUserDto : IMapWith<UpdateUserCommand>
	{
		public Guid UserId { get; set; }
		public string Name { get; set; }
		public int Gender { get; set; }
		public DateTime? BirthDay { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
				.ForMember(updateUserCommand => updateUserCommand.UserId,
					opt => opt.MapFrom(updateUserDto => updateUserDto.UserId))
				.ForMember(UpdateUserCommand => UpdateUserCommand.Name,
					opt => opt.MapFrom(updateUserDto => updateUserDto.Name))
				.ForMember(UpdateUserCommand => UpdateUserCommand.Gender,
					opt => opt.MapFrom(updateUserDto => updateUserDto.Gender))
				.ForMember(UpdateUserCommand => UpdateUserCommand.BirthDay,
					opt => opt.MapFrom(updateUserDto => updateUserDto.BirthDay));
		}
	}
}
