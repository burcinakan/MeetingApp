using MeetingApp.Api.Repositories;
using MeetingApp.Api.Services;
using MeetingApp.Api.Services.Concrete;
using MeetingApp.Api.Worker;
using MeetingApp.Context.Repositories;
using MeetingApp.DAL.Contexts;
using MeetingApp.Models.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EMailSettings>();
builder.Services.AddSingleton(emailConfig);

// EMAÝL
builder.Services.AddScoped<IMailService, MailService>();


// SERVÝCES
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();

builder.Services.AddDbContext<SQLContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);




builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
	opt.RequireHttpsMetadata = false;
	opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidIssuer = "http://localhost:5243",
		ValidAudience = "http://localhost:5243",
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bubirjwttoken123456bubirjwttoken123456")),
		ValidateIssuerSigningKey = true,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero

	};
});

builder.Services.AddHostedService<WorkerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
