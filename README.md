# LibraryManagementService
A simple Library Management System built in .NET 9.0 using C# and Mac Catalyst (no XAML). Features include user registration, login, book borrowing and returning, overdue tracking, and a clean, responsive UI. Designed as a  project with full object-oriented structure and no persistent storage.


## 🛠️ Technologies Used

- **.NET 9.0**
- **C# (no XAML)**
- **.NET MAUI with Mac Catalyst**
- **Visual Studio Code (macOS)**

---

## ✅ Features

- Register new library members and generate unique IDs
- Secure login using member ID
- View all available books with title and author
- Search books by keyword
- Borrow and return books
- Automatic overdue tracking (14-day return policy)
- View personal list of borrowed and overdue books
- Real-time UI updates with a user-friendly layout

---

## 💡 Object-Oriented Principles Applied

- **Encapsulation**: Each class (e.g., `Member`, `Book`, `LibraryService`) handles its own data and behaviors.
- **Abstraction**: Users interact only with the interface, not the internal logic.
- **Inheritance and Polymorphism** are prepared for but not deeply used due to the project’s scope.
- **Separation of Concerns**: Logic, UI, and data models are kept in separate files for clarity and maintainability.

---

## 🧮 Use of Math

- Calculates the number of days since a book was borrowed using `DateTime.Now - BorrowedDate`.
- Automatically flags books as overdue if the borrowing period exceeds 14 days.
- Generates sequential member IDs and book IDs.

---

## 🚀 How to Run the Project

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/LibraryManagementSystem.git
