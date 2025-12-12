using System;
using System.Reactive;
using ReactiveUI;
using TicTacToe.Core.Entities;

namespace TicTacToe.UI.ViewModels
{
  public class MenuViewModel : ViewModelBase
  {
    public User CurrentUser { get; }

    public Action? OnLogout { get; set; }
    public Action? OnStartPve { get; set; }

    public ReactiveCommand<Unit, Unit> LogoutCommand { get; }
    public ReactiveCommand<Unit, Unit> HistoryCommand { get; }
    public ReactiveCommand<Unit, Unit> StartPvpCommand { get; }
    public ReactiveCommand<Unit, Unit> StartPveCommand { get; }

    public MenuViewModel(User user)
    {
      CurrentUser = user;

      LogoutCommand = ReactiveCommand.Create(() => OnLogout?.Invoke());
      StartPveCommand = ReactiveCommand.Create(() => OnStartPve?.Invoke());

      // TEST console TODO: delete
      HistoryCommand = ReactiveCommand.Create(() => Console.WriteLine("Історія"));
      StartPvpCommand = ReactiveCommand.Create(() => Console.WriteLine("Гра 1 на 1"));
    }
  }
}
