using System.Collections.Generic;
using TicTacToe.Core.Entities;

namespace TicTacToe.Core.Interfaces
{
  public interface IUserRepository
  {
    User GetByUsername(string username);
    void Add(User user);
    void Update(User user);
    List<User> GetAll();
  }
}
