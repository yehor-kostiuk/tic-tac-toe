using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using TicTacToe.Core.Entities;
using TicTacToe.DAL.Repositories;

namespace TicTacToe.UI.ViewModels
{
  public class HistoryViewModel : ViewModelBase
  {
    public List<GameSession> Games { get; }

    public Action? OnClose { get; set; }
    public ReactiveCommand<Unit, Unit> CloseCommand { get; }

    public HistoryViewModel(User user, GameRepository gameRepo)
    {
      Games = gameRepo.GetGamesByUserId(user.Id);

      CloseCommand = ReactiveCommand.Create(() => OnClose?.Invoke());
    }
  }
}
