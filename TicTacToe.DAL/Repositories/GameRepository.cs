using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TicTacToe.Core.Entities;

namespace TicTacToe.DAL.Repositories
{
  public class GameRepository
  {
    private readonly string _filePath = "games.json";

    public GameRepository()
    {
      if (!File.Exists(_filePath))
        File.WriteAllText(_filePath, "[]");
    }

    public void AddGame(GameSession game)
    {
      var games = GetAll();
      games.Add(game);
      Save(games);
    }

    public List<GameSession> GetGamesByUserId(System.Guid userId)
    {
      return GetAll().Where(g => g.PlayerId == userId).OrderByDescending(g => g.Date).ToList();
    }

    private List<GameSession> GetAll()
    {
      var json = File.ReadAllText(_filePath);
      return JsonSerializer.Deserialize<List<GameSession>>(json) ?? new List<GameSession>();
    }

    private void Save(List<GameSession> games)
    {
      var json = JsonSerializer.Serialize(
        games,
        new JsonSerializerOptions { WriteIndented = true }
      );
      File.WriteAllText(_filePath, json);
    }
  }
}
