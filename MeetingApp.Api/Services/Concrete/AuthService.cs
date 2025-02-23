using MeetingApp.Api.Repositories;
using MeetingApp.Api.Security;
using MeetingApp.Model.Models;
using MeetingApp.Models.DTO;
using System.Security.Cryptography;
using System.Text;

namespace MeetingApp.Api.Services.Concrete
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly JWTGenerator _jWTGenerator;
		private readonly IMailService _mailService;

		public AuthService(IUserRepository userRepository, IMailService mailService)
		{
			_userRepository = userRepository;
			_jWTGenerator = new JWTGenerator();
			_mailService = mailService;
		}

		public string Login(LoginDto loginDto)
		{
			var user = _userRepository.FirstOrDefault(u => u.Email == loginDto.Email);
			if (user == null)

				throw new Exception("Kullanıcı bulunamadı!");


			if (!VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
				throw new Exception("Hatalı şifre!");

			return _jWTGenerator.GenerateToken(user.ID.ToString());

		}

		public string Register(RegisterDto registerDto)
		{
			var checkUser = _userRepository.FirstOrDefault(u => u.Email == registerDto.Email);
			if (checkUser != null)
				throw new Exception("Bu e-posta adresi zaten kullanılıyor!");

			CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

			var user = new User
			{
				Name = registerDto.Name,
				LastName = registerDto.LastName,
				Email = registerDto.Email,
				PhoneNumber = registerDto.PhoneNumber,
				PasswordHash = passwordHash,
				PasswordSalt = passwordSalt,
				ProfileImage = registerDto.ProfileImage
			};

			_userRepository.Add(user);

			//HoşgeldinizMaili
			_mailService.SendWelcomeEmail(user.Email, user.Name + user.LastName);

			return "Kullanıcı başarıyla kaydedildi.";
		}
		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new HMACSHA256())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}
		private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
		{
			using (var hmac = new HMACSHA256(storedSalt))
			{
				var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(storedHash);
			}
		}

	}
}
