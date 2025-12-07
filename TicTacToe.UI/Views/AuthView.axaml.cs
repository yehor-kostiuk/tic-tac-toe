using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace TicTacToe.UI.Views
{
  public partial class AuthView : UserControl
  {
    public AuthView()
    {
      InitializeComponent();
    }

    private void InitializeComponent()
    {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
