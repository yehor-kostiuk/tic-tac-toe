using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using TicTacToe.BLL.Services;
using TicTacToe.Core.Entities;
using TicTacToe.Core.Interfaces;

namespace TicTacToe.UI.ViewModels
{
  public class GameViewModel : ViewModelBase
  {
    private readonly GameService _gameLogic;
    private readonly User _user;
    private readonly IUserRepository _userRepository;

    public string PlayerName => _user.Username;
    public int CurrentRating => _user.Rating;

    public ObservableCollection<CellViewModel> Board { get; } = new();

    private string _statusMessage = "Гра почалась";
    public string StatusMessage
    {
      get => _statusMessage;
      set => this.RaiseAndSetIfChanged(ref _statusMessage, value);
    }

    private bool _isGameOver;
    public bool IsGameOver
    {
      get => _isGameOver;
      set => this.RaiseAndSetIfChanged(ref _isGameOver, value);
    }

    private string _resultText = "";
    public string ResultText
    {
      get => _resultText;
      set => this.RaiseAndSetIfChanged(ref _resultText, value);
    }

    public Action? OnCloseGame { get; set; }
    public ReactiveCommand<Unit, Unit> CloseCommand { get; }
    public ReactiveCommand<int, Unit> PlayerMoveCommand { get; }

    public GameViewModel(User user, IUserRepository userRepository)
    {
      _user = user;
      _userRepository = userRepository;
      _gameLogic = new GameService();

      PlayerMoveCommand = ReactiveCommand.Create<int>(OnPlayerMove);
      for (int i = 0; i < 9; i++)
      {
        Board.Add(new CellViewModel(i, PlayerMoveCommand));
      }

      CloseCommand = ReactiveCommand.Create(() => OnCloseGame?.Invoke());

      StartGame();
    }

    private async void StartGame()
    {
      _gameLogic.Reset();
      IsGameOver = false;
      var rnd = new Random();
      if (rnd.Next(0, 2) == 1)
      {
        StatusMessage = "Хід бота...";
        await Task.Delay(500);
        BotMove();
      }
      else
      {
        StatusMessage = "Ваш хід (X)";
      }
    }

    private void OnPlayerMove(int index)
    {
      if (IsGameOver)
        return;
      if (_gameLogic.MakeMove(index, Player.X))
      {
        Board[index].Text = "X";
        CheckGameState();
        if (!IsGameOver)
        {
          StatusMessage = "Бот думає...";
          BotMove();
        }
      }
    }

    private void BotMove()
    {
      int botIndex = _gameLogic.GetBotMoveIndex();
      if (botIndex != -1)
      {
        _gameLogic.MakeMove(botIndex, Player.O);
        Board[botIndex].Text = "O";
        StatusMessage = "Ваш хід (X)";
        CheckGameState();
      }
    }

    private void CheckGameState()
    {
      var result = _gameLogic.CheckWinner(Player.X);
      if (result != GameResult.None)
      {
        EndGame(result);
      }
    }

    private void EndGame(GameResult result)
    {
      IsGameOver = true;
      int ratingChange = 0;

      switch (result)
      {
        case GameResult.Win:
          ratingChange = 25;
          ResultText = $"ПЕРЕМОГА (+{ratingChange})";
          break;
        case GameResult.Loss:
          ratingChange = -25;
          ResultText = $"ПОРАЗКА ({ratingChange})";
          break;
        case GameResult.Draw:
          ResultText = "НІЧИЯ (0)";
          break;
      }

      _user.Rating += ratingChange;
      if (_user.Rating < 0)
        _user.Rating = 0;

      _userRepository.Update(_user);

      this.RaisePropertyChanged(nameof(CurrentRating));
    }
  }
}
