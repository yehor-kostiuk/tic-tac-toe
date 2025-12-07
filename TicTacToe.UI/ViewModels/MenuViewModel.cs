using TicTacToe.Core.Entities;

namespace TicTacToe.UI.ViewModels
{
  public class MenuViewModel : ViewModelBase
  {
    public User CurrentUser { get; }

    public MenuViewModel(User user)
    {
      CurrentUser = user;
    }
  }
}
