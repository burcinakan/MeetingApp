using MeetingApp.Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Context.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable("Users");
			builder.HasKey(x => x.ID);
			builder.Property(x => x.Name).HasColumnName("Name");
			builder.Property(x => x.LastName).HasColumnName("LastName");
			builder.Property(x => x.Email).HasColumnName("Email");
			builder.Property(x => x.PhoneNumber).HasMaxLength(11).HasColumnName("PhoneNumber");
			builder.Property(x => x.PasswordSalt).HasColumnName("PasswordSalt");
			builder.Property(x => x.PasswordHash).HasColumnName("PasswordHash");
			builder.Property(x => x.ProfileImage).HasColumnName("ProfileImage");
			builder.HasMany(x => x.Meetings).WithOne(m => m.User).HasForeignKey(m => m.UserId);
		}
	}
}
