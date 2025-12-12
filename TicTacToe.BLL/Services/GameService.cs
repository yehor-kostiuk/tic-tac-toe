using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.BLL.Services
{
  public enum GameResult
  {
    None,
    Win,
    Loss,
    Draw,
  }

  public enum Player
  {
    X,
    O,
  }

  public class GameService
  {
    private Player?[] _board = new Player?[9];
    private Random _random = new Random();

    public void Reset()
    {
      _board = new Player?[9];
    }

    public bool MakeMove(int index, Player player)
    {
      if (index < 0 || index >= 9 || _board[index] != null)
        return false;
      _board[index] = player;
      return true;
    }

    public int GetBotMoveIndex()
    {
      var emptyIndices = new List<int>();
      for (int i = 0; i < 9; i++)
      {
        if (_board[i] == null)
          emptyIndices.Add(i);
      }

      if (emptyIndices.Count == 0)
        return -1;
      return emptyIndices[_random.Next(emptyIndices.Count)];
    }

    public GameResult CheckWinner(Player humanPlayer)
    {
      int[][] lines =
      {
        new[] { 0, 1, 2 },
        new[] { 3, 4, 5 },
        new[] { 6, 7, 8 },
        new[] { 0, 3, 6 },
        new[] { 1, 4, 7 },
        new[] { 2, 5, 8 },
        new[] { 0, 4, 8 },
        new[] { 2, 4, 6 },
      };

      foreach (var line in lines)
      {
        if (
          _board[line[0]] != null
          && _board[line[0]] == _board[line[1]]
          && _board[line[1]] == _board[line[2]]
        )
        {
          return _board[line[0]] == humanPlayer ? GameResult.Win : GameResult.Loss;
        }
      }

      if (!_board.Any(x => x == null))
        return GameResult.Draw;

      return GameResult.None;
    }
  }
}
