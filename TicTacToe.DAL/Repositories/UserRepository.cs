using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TicTacToe.Core.Entities;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.DAL.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly string _filePath = "users.json";

    public UserRepository()
    {
      if (!File.Exists(_filePath))
      {
        File.WriteAllText(_filePath, "[]");
      }
    }

    public void Add(User user)
    {
      var users = GetAll();
      users.Add(user);
      Save(users);
    }

    public List<User> GetAll()
    {
      var json = File.ReadAllText(_filePath);
      return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    public User GetByUsername(string username)
    {
      return GetAll().FirstOrDefault(u => u.Username == username);
    }

    public void Update(User user)
    {
      var users = GetAll();
      var existingUser = users.FirstOrDefault(u => u.Id == user.Id);
      if (existingUser != null)
      {
        users.Remove(existingUser);
        users.Add(user);
        Save(users);
      }
    }

    private void Save(List<User> users)
    {
      var json = JsonSerializer.Serialize(
        users,
        new JsonSerializerOptions { WriteIndented = true }
      );
      File.WriteAllText(_filePath, json);
    }
  }
}
