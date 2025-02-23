using MeetingApp.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingApp.Context.Configurations
{
	public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
	{
		public void Configure(EntityTypeBuilder<Meeting> builder)
		{
			builder.ToTable("Meetings");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.Title).HasColumnName("Title");
			builder.Property(x => x.StartDate).HasColumnName("StartDate");
			builder.Property(x => x.EndDate).HasColumnName("EndDate");
			builder.Property(x => x.Description).HasColumnName("Description");
			builder.Property(x => x.DocumentUrl).HasColumnName("DocumentUrl");
			builder.Property(x => x.UserId).HasColumnName("UserId");
			builder.HasOne(m => m.User).WithMany(u => u.Meetings).HasForeignKey(u => u.UserId);
		}
	}
}
