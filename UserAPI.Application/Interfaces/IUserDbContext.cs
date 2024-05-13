using Microsoft.EntityFrameworkCore;
using UserAPI.Domain;

namespace UserAPI.Application.Interfaces
{
	public interface IUserDbContext
	{
		DbSet<User> Users { get; set; }
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
