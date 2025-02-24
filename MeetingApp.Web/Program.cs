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
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
	c.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });
});



var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EMailSettings>();
builder.Services.AddSingleton(emailConfig);

// EMA�L
builder.Services.AddScoped<IMailService, MailService>();


// SERV�CES
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


// CORS ayarlar�n� ekle
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin", policy =>
	{
		policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
			  .AllowAnyMethod()
			  .AllowAnyHeader()
			  .AllowCredentials();
	});
});

var app = builder.Build();


app.UseCors("AllowSpecificOrigin");

// Resources dizinini statik dosya olarak sunmak i�in
var pathToResources = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(pathToResources),
	RequestPath = "/Resources"
});

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
