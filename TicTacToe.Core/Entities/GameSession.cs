using System;

namespace TicTacToe.Core.Entities
{
  public class GameSession
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public string OpponentName { get; set; } = string.Empty;
    public int RatingChange { get; set; }
    public int PlayerRating { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public bool IsWin { get; set; }
  }
}
