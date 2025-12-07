using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
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
      var authViewModel = new TicTacToe.UI.ViewModels.AuthViewModel();
      var mainViewModel = new TicTacToe.UI.ViewModels.MainWindowViewModel(authViewModel);

      desktop.MainWindow = new TicTacToe.UI.Views.MainWindow { DataContext = mainViewModel };
    }

    base.OnFrameworkInitializationCompleted();
  }
}
