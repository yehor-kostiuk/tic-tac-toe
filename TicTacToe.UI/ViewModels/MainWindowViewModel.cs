using ReactiveUI;

namespace TicTacToe.UI.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    private ViewModelBase _content = null!;

    public MainWindowViewModel(ViewModelBase startView)
    {
      Content = startView;
    }

    public ViewModelBase Content
    {
      get => _content;
      set => this.RaiseAndSetIfChanged(ref _content, value);
    }
  }
}
