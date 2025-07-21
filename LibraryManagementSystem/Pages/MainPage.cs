using Microsoft.Maui.Controls;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Pages;

public class MainPage : ContentPage
{
    private readonly LibraryService library;
    private readonly Member currentMember;

    private readonly Entry searchEntry = new() { Placeholder = "Search by title or author" };
    private readonly Button searchButton = new() { Text = "Search" };
    private readonly Button viewAllButton = new() { Text = "View All Books" };
    private readonly Button viewAvailableButton = new() { Text = "View Available Books" };
    private readonly Button viewBorrowedButton = new() { Text = "My Borrowed Books" };
    private readonly Button viewOverdueButton = new() { Text = "My Overdue Books" };

    private readonly ListView bookListView = new();
    private List<Book> currentBookList = new();

    public MainPage(Member member)
    {
       library = MauiProgram.ServiceProvider.GetService<LibraryService>()!;

        currentMember = member;

        searchButton.Clicked += OnSearchClicked;
        viewAllButton.Clicked += (s, e) => LoadBooks(library.Books);
        viewAvailableButton.Clicked += (s, e) => LoadBooks(library.GetAvailableBooks());
        viewBorrowedButton.Clicked += (s, e) => LoadBooks(library.GetBorrowedBooks(currentMember.Id));
        viewOverdueButton.Clicked += (s, e) => LoadBooks(library.GetOverdueBooks(currentMember.Id));

        bookListView.ItemTemplate = new DataTemplate(() =>
        {
            var titleLabel = new Label { FontAttributes = FontAttributes.Bold };
            titleLabel.SetBinding(Label.TextProperty, "Title");

            var authorLabel = new Label();
            authorLabel.SetBinding(Label.TextProperty, "Author");

            var idLabel = new Label { FontSize = 12, TextColor = Colors.Gray };
            idLabel.SetBinding(Label.TextProperty, new Binding("Id", stringFormat: "Book ID: {0}"));

            return new ViewCell
            {
                View = new VerticalStackLayout
                {
                    Padding = new Thickness(10),
                    Children = { titleLabel, authorLabel, idLabel }
                }
            };
        });

        bookListView.ItemSelected += OnBookSelected;

        LoadBooks(library.Books);

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 15,
                Children =
                {
                    new Label
                    {
                        Text = $"Welcome, {currentMember.Name} (ID: {currentMember.Id})",
                        FontSize = 22,
            
                        HorizontalOptions = LayoutOptions.Center
                    },
                    searchEntry,
                    searchButton,
                    viewAllButton,
                    viewAvailableButton,
                    viewBorrowedButton,
                    viewOverdueButton,
                    bookListView
                }
            }
        };
    }

    private void LoadBooks(List<Book> books)
    {
        currentBookList = books;
        bookListView.ItemsSource = currentBookList;
    }

    private void OnSearchClicked(object? sender, EventArgs e)
    {
        var query = searchEntry.Text?.ToLower() ?? "";
        var filtered = library.Books
            .Where(b => b.Title.ToLower().Contains(query) || b.Author.ToLower().Contains(query))
            .ToList();

        LoadBooks(filtered);
    }

    private async void OnBookSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is not Book selectedBook)
            return;

        bookListView.SelectedItem = null;

        string action;
        if (selectedBook.BorrowedBy == null)
        {
            action = await DisplayActionSheet("Borrow this book?", "Cancel", null, "Borrow");
            if (action == "Borrow")
            {
                selectedBook.BorrowedBy = currentMember.Id;
                selectedBook.BorrowedDate = DateTime.Now;
                await DisplayAlert("Success", "Book borrowed successfully. Please return within 14 days.", "OK");
            }
        }
        else if (selectedBook.BorrowedBy == currentMember.Id)
        {
            action = await DisplayActionSheet("Return this book?", "Cancel", null, "Return");
            if (action == "Return")
            {
                selectedBook.BorrowedBy = null;
                selectedBook.BorrowedDate = null;
                await DisplayAlert("Returned", "Book returned successfully.", "OK");
            }
        }
        else
        {
            await DisplayAlert("Not Available", "This book is borrowed by another member.", "OK");
        }

        LoadBooks(currentBookList); 
    }
}
