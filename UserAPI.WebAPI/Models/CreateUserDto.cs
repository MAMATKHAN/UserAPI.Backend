using AutoMapper;
using UserAPI.Application.Common.Mappings;
using UserAPI.Application.Users.Commands.CreateUser;

namespace UserAPI.WebAPI.Models
{
	public class CreateUserDto : IMapWith<CreateUserCommand>
	{
		public string Login { get; set; }
		public string Password { get; set; }
		public string Name { get; set; }
		public int Gender { get; set; }
		public DateTime? BirthDay { get; set; }
		public bool Admin { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<CreateUserDto, CreateUserCommand>()
				.ForMember(createUserCommand => createUserCommand.Login,
					opt => opt.MapFrom(createUserDto => createUserDto.Login))
				.ForMember(createUserCommand => createUserCommand.Password,
					opt => opt.MapFrom(createUserDto => createUserDto.Password))
				.ForMember(createUserCommand => createUserCommand.Name,
					opt => opt.MapFrom(createUserDto => createUserDto.Name))
				.ForMember(createUserCommand => createUserCommand.Gender,
					opt => opt.MapFrom(createUserDto => createUserDto.Gender))
				.ForMember(createUserCommand => createUserCommand.BirthDay,
					opt => opt.MapFrom(createUserDto => createUserDto.BirthDay))
				.ForMember(createUserCommand => createUserCommand.Admin,
					opt => opt.MapFrom(createUserDto => createUserDto.Admin));
		}
	}
}
