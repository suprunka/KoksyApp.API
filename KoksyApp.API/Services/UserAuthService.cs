using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

    public string GetTokenUser(string token);
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
        var tokenKey = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT"));
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[] {new Claim(ClaimTypes.Email, email), new Claim(ClaimTypes.NameIdentifier, user.Id)}),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
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

    public string GetTokenUser(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwt =tokenHandler.ReadJwtToken(token);
        return jwt.Claims.First(c=> c.Type.Equals("nameid")).Value;
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