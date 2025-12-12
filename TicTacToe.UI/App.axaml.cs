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
        var menuViewModel = new MenuViewModel(user);

        // Logout
        menuViewModel.OnLogout = () =>
        {
          mainViewModel.Content = authViewModel;
        };

        // VS Bot
        menuViewModel.OnStartPve = () =>
        {
          var gameViewModel = new GameViewModel(user, userRepo);

          gameViewModel.OnCloseGame = () =>
          {
            mainViewModel.Content = menuViewModel;
          };

          mainViewModel.Content = gameViewModel;
        };

        mainViewModel.Content = menuViewModel;
      };

      desktop.MainWindow = new MainWindow { DataContext = mainViewModel };
    }

    base.OnFrameworkInitializationCompleted();
  }
}
