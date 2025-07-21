using LibraryManagementSystem.Models;

public class LibraryService
{
    public List<Member> Members { get; private set; } = new();
    public List<Book> Books { get; private set; } = new();

    private int _memberIdCounter = 1;

    public LibraryService()
    {
        SeedBooks();
    }

    public Member RegisterMember(string name)
    {
        var member = new Member
        {
            Name = name,
            Id = _memberIdCounter++
        };

        Members.Add(member);
        return member;
    }

    public Member? GetMember(int id) => Members.FirstOrDefault(m => m.Id == id);

    public List<Book> GetBorrowedBooks(int memberId) =>
        Books.Where(b => b.BorrowedBy == memberId).ToList();

    public List<Book> GetOverdueBooks(int memberId) =>
        Books.Where(b => b.BorrowedBy == memberId && b.BorrowedDate.HasValue && (DateTime.Now - b.BorrowedDate.Value).Days > 14).ToList();

    public List<Book> GetAvailableBooks() =>
        Books.Where(b => b.BorrowedBy == null).ToList();

    private void SeedBooks()
    {
        string[] titles = {
            "The Great Gatsby", "1984", "To Kill a Mockingbird", "Pride and Prejudice",
            "The Catcher in the Rye", "Moby-Dick", "War and Peace", "The Hobbit",
            "Crime and Punishment", "Jane Eyre"
        };

        string[] authors = {
            "F. Scott Fitzgerald", "George Orwell", "Harper Lee", "Jane Austen",
            "J.D. Salinger", "Herman Melville", "Leo Tolstoy", "J.R.R. Tolkien",
            "Fyodor Dostoevsky", "Charlotte Brontë"
        };

        for (int i = 0; i < titles.Length; i++)
        {
            Books.Add(new Book { Id = i + 1, Title = titles[i], Author = authors[i] });
        }
    }
}
