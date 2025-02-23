using MeetingApp.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingApp.Context.Configurations
{
	public class LogConfiguration : IEntityTypeConfiguration<Log>
	{
		public void Configure(EntityTypeBuilder<Log> builder)
		{
			builder.ToTable("Logs");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.TableName).HasColumnName("TableName");
			builder.Property(x => x.Action).HasColumnName("Action");
			builder.Property(x => x.UserId).HasColumnName("UserId");
			builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);

		}
	}
}
