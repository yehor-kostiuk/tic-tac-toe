using System;
using System.Security.Cryptography;
using System.Text;
using TicTacToe.Core.Entities;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.BLL.Services
{
  public class AuthService
  {
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public User Register(string username, string password, string email)
    {
      if (_userRepository.GetByUsername(username) != null)
      {
        throw new Exception("Користувач з таким іменем вже існує");
      }

      var newUser = new User
      {
        Username = username,
        Email = email,
        PasswordHash = HashPassword(password),
        Rating = 1000,
      };

      _userRepository.Add(newUser);
      return newUser;
    }

    public User Login(string username, string password)
    {
      var user = _userRepository.GetByUsername(username);

      if (user == null)
        throw new Exception("Користувача не знайдено");

      if (user.PasswordHash != HashPassword(password))
        throw new Exception("Невірний пароль");

      return user;
    }

    private string HashPassword(string password)
    {
      using (var sha256 = SHA256.Create())
      {
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
      }
    }
  }
}
