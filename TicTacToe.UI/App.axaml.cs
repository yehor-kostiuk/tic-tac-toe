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

        menuViewModel.OnLogout = () =>
        {
          mainViewModel.Content = authViewModel;
        };

        mainViewModel.Content = menuViewModel;
      };

      desktop.MainWindow = new MainWindow { DataContext = mainViewModel };
    }

    base.OnFrameworkInitializationCompleted();
  }
}
