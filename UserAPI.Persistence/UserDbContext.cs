using Microsoft.EntityFrameworkCore;
using UserAPI.Application.Interfaces;
using UserAPI.Domain;

namespace UserAPI.Persistence
{
	public class UserDbContext : DbContext, IUserDbContext
	{
		public DbSet<User> Users { get; set; }

		public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			base.OnModelCreating(modelBuilder);
		}

	}
}
