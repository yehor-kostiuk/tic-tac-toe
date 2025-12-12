using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;

namespace TicTacToe.UI.ViewModels
{
  public class CellViewModel : ViewModelBase
  {
    private string _text = "";
    public int Index { get; }

    public string Text
    {
      get => _text;
      set => this.RaiseAndSetIfChanged(ref _text, value);
    }

    public ReactiveCommand<Unit, Unit> ClickCommand { get; }

    public CellViewModel(int index, ReactiveCommand<int, Unit> parentCommand)
    {
      Index = index;
      ClickCommand = ReactiveCommand.Create(() =>
      {
        parentCommand.Execute(Index).Subscribe(_ => { });
      });
    }
  }
}
