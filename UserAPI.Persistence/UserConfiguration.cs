using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAPI.Domain;

namespace UserAPI.Persistence
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(b => b.Guid);
			builder.HasIndex(b => b.Guid);
			builder.Property(b => b.Name).HasMaxLength(256);
			builder.Property(b => b.Login).HasMaxLength(256);
			builder.Property(b => b.Password).HasMaxLength(256);
		}
	}
}
