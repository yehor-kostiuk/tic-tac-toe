using System; // TODO: delete
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using TicTacToe.BLL.Services;
using TicTacToe.DAL.Repositories;
using TicTacToe.UI.ViewModels;
using TicTacToe.UI.Views;

namespace TicTacToe.UI;

public partial class App : Application
{
  public override void Initialize()
  {
    AvaloniaXamlLoader.Load(this);
  }

  public override void OnFrameworkInitializationCompleted()
  {
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      var userRepo = new UserRepository();
      var authService = new AuthService(userRepo);

      var authViewModel = new AuthViewModel(authService);
      var mainViewModel = new MainWindowViewModel(authViewModel);

      authViewModel.OnLoginSuccess = (user) =>
      {
        Console.WriteLine($"user {user.Username}");
        Console.WriteLine($"rate {user.Rating}");
        Console.WriteLine($"id {user.Id}");

        authViewModel.Message = $"Вхід виконано {user.Username}";
      };

      desktop.MainWindow = new MainWindow { DataContext = mainViewModel };
    }

    base.OnFrameworkInitializationCompleted();
  }
}
