using Microsoft.Maui.Controls;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Pages;

public class LoginPage : ContentPage
{
    private readonly Entry idEntry = new() { Placeholder = "Enter your Member ID" };
    private readonly Button loginButton = new() { Text = "Login" };

    public LoginPage()
    {
        loginButton.Clicked += OnLoginClicked;

        Content = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children =
            {
                new Label { Text = "Library Login", FontSize = 24, HorizontalOptions = LayoutOptions.Center },
                idEntry,
                loginButton
            }
        };
    }

    private async void OnLoginClicked(object? sender, EventArgs e)
{
    string idText = idEntry.Text?.Trim() ?? "";

    if (!int.TryParse(idText, out int memberId))
    {
        await DisplayAlert("Error", "Please enter a valid numeric Member ID.", "OK");
        return;
    }

    var library = MauiProgram.ServiceProvider.GetService<LibraryService>()!;
    var member = library.GetMember(memberId); 

    if (member == null)
    {
        await DisplayAlert("Error", "Member not found. Please register first.", "OK");
        return;
    }

    Application.Current!.Windows[0].Page = new MainPage(member);
}

}
