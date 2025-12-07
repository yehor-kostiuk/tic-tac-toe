using System;
using System.Reactive;
using ReactiveUI;
using TicTacToe.BLL.Services;
using TicTacToe.Core.Entities;

namespace TicTacToe.UI.ViewModels
{
  public class AuthViewModel : ViewModelBase
  {
    private readonly AuthService _authService;

    public Action<User>? OnLoginSuccess { get; set; }

    private string _username = string.Empty;
    private string _password = string.Empty;
    private string _email = string.Empty;
    private string _message = string.Empty;
    private bool _isRegisterMode;

    public AuthViewModel(AuthService authService)
    {
      _authService = authService;

      SubmitCommand = ReactiveCommand.Create(ExecuteSubmit);

      ToggleModeCommand = ReactiveCommand.Create(() =>
      {
        IsRegisterMode = !IsRegisterMode;
        Message = string.Empty;
      });
    }

    public string Username
    {
      get => _username;
      set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string Password
    {
      get => _password;
      set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public string Email
    {
      get => _email;
      set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    public string Message
    {
      get => _message;
      set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    public bool IsRegisterMode
    {
      get => _isRegisterMode;
      set
      {
        this.RaiseAndSetIfChanged(ref _isRegisterMode, value);
        this.RaisePropertyChanged(nameof(MainButtonText));
        this.RaisePropertyChanged(nameof(ToggleModeText));
      }
    }

    public string MainButtonText => IsRegisterMode ? "Зареєструватися" : "Увійти";
    public string ToggleModeText =>
      IsRegisterMode ? "Вже є акаунт? Увійти" : "Немає акаунту? Реєстрація";

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }
    public ReactiveCommand<Unit, Unit> ToggleModeCommand { get; }

    private void ExecuteSubmit()
    {
      if (IsRegisterMode)
        Register();
      else
        Login();
    }

    private void Login()
    {
      try
      {
        var user = _authService.Login(Username, Password);
        OnLoginSuccess?.Invoke(user);
      }
      catch (Exception ex)
      {
        Message = $"Помилка входу: {ex.Message}";
      }
    }

    private void Register()
    {
      try
      {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
          Message = "Введіть логін та пароль";
          return;
        }

        _authService.Register(Username, Password, Email);

        Message = "Успішно! Тепер увійдіть";
        IsRegisterMode = false;
        Password = "";
      }
      catch (Exception ex)
      {
        Message = $"Помилка реєстрації: {ex.Message}";
      }
    }
  }
}
