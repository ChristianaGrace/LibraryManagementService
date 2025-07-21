using Microsoft.Maui.Controls;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Pages;

public class RegisterPage : ContentPage
{
    private readonly Entry nameEntry = new() { Placeholder = "Enter your name" };
    private readonly Button registerButton = new() { Text = "Register" };

    public RegisterPage()
    {
        registerButton.Clicked += OnRegisterClicked;

        Content = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children =
            {
                new Label { Text = "Library Registration", FontSize = 24, HorizontalOptions = LayoutOptions.Center },
                nameEntry,
                registerButton
            }
        };
    }

    private async void OnRegisterClicked(object? sender, EventArgs e)
    {
        string name = nameEntry.Text?.Trim() ?? "";
        if (string.IsNullOrWhiteSpace(name) || name.Any(char.IsDigit))
        {
            await DisplayAlert("Invalid", "Enter a valid name without numbers, special characters or spaces.", "OK");
            return;
        }

        var library = MauiProgram.ServiceProvider.GetService<LibraryService>()!;
        var member = library.RegisterMember(name);
        await DisplayAlert("Registered", $"Your Member ID: {member.Id}", "OK");

        Application.Current!.Windows[0].Page = new LoginPage();
    }
}
