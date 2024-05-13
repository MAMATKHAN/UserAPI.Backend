using UserAPI.Domain;

namespace UserAPI.Persistence
{
	public class DbInitializer
	{
		public static void Initialize(UserDbContext context)
		{
			context.Database.EnsureCreated();
			InitializeAdmin(context);
		}

		private static void InitializeAdmin(UserDbContext context)
		{
			var adminIsExist = context.Users.Any(user => user.Login == "admin");

			if (adminIsExist) return;

			var admin = new User
			{
				Login = "admin",
				Password = "admin",
				Name = "admin",
				Gender = 1,
				BirthDay = DateTime.Now,
				Admin = true,
				CreatedBy = "admin",
				CreatedOn = DateTime.Now,
				ModifiedBy = string.Empty,
				ModifiedOn = default(DateTime),
				RevokedBy = string.Empty,
				RevokedOn = default(DateTime),
			};

			context.Users.Add(admin);
			context.SaveChanges();
		}
	}
}
