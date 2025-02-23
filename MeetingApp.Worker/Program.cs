using MeetingApp.Api.Repositories;
using MeetingApp.Api.Services;
using MeetingApp.Api.Services.Concrete;
using MeetingApp.Context.Repositories;
using MeetingApp.DAL.Contexts;
using MeetingApp.Worker;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<SQLContext>(options =>
	options.UseSqlServer("DefaultConnection"));

builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();

var host = builder.Build();
host.Run();
