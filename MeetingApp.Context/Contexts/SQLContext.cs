using MeetingApp.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MeetingApp.DAL.Contexts
{
	public class SQLContext : DbContext
	{
		public SQLContext(DbContextOptions<SQLContext> options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Meeting> Meetings { get; set; }
		public DbSet<Log> Logs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}
