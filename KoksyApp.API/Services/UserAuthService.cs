using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KoksyApp.API.Exceptions;
using KoksyApp.API.Models;
using KoksyApp.API.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace KoksyApp.API.Services;

public interface IUserAuthService
{
    public string Authenticate(string email, string password);
    public Task<bool> Register(string email, string password);
}
public class UserAuthService:IUserAuthService
{
    private readonly IUserRepository userRepository;

    public UserAuthService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public string Authenticate(string email, string password)
    {
        var users = userRepository.Get();
            var user = users.FirstOrDefault(u=> u.Email == email && u.Password == HashPassword(password));
        if (user == null)
            throw new NotFoundException();

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.ASCII.GetBytes("KEY0987654rtyghgtr5456789");
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Email, email)}),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public Task<bool> Register(string email, string password)
    {
        var existingUser = userRepository.Get().FirstOrDefault(u=> u.Email == email);
        if (existingUser != null)
            throw new AllreadyCreated();
        var user = new User()
        {
            Password = HashPassword(password),
            Email = email,
        };
        return userRepository.Add(user);
    }

    private string HashPassword(string password)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes("KEY"),
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}