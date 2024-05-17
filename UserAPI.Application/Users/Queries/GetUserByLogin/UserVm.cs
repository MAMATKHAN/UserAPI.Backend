using AutoMapper;
using UserAPI.Application.Common.Mappings;
using UserAPI.Domain;

namespace UserAPI.Application.Users.Queries.GetUserByLogin
{
	public class UserVm : IMapWith<User>
	{
		public string Name { get; set; }
		public int Gender { get; set; }
		public DateTime? BirthDay { get; set; }
		public bool userIsActive { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<User, UserVm>()
				.ForMember(userVm => userVm.Name,
					opt => opt.MapFrom(user => user.Name))
				.ForMember(userVm => userVm.Gender,
					opt => opt.MapFrom(user => user.Gender))
				.ForMember(userVm => userVm.BirthDay,
					opt => opt.MapFrom(user => user.BirthDay))
				.ForMember(userVm => userVm.userIsActive,
					opt => opt.MapFrom(user => string.IsNullOrEmpty(user.RevokedBy)));
		}
	}
}
