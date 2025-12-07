using System.Reactive;
using ReactiveUI;

namespace TicTacToe.UI.ViewModels
{
  public class AuthViewModel : ViewModelBase
  {
    private string _pageTitle = "Сторінка Входу";

    public string PageTitle
    {
      get => _pageTitle;
      set => this.RaiseAndSetIfChanged(ref _pageTitle, value);
    }

    public ReactiveCommand<Unit, Unit> SwitchPageCommand { get; }

    public AuthViewModel()
    {
      SwitchPageCommand = ReactiveCommand.Create(() =>
      {
        if (PageTitle == "Сторінка Входу")
        {
          PageTitle = "Сторінка Реєстрації";
        }
        else
        {
          PageTitle = "Сторінка Входу";
        }
      });
    }
  }
}
