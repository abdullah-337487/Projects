///////LIBRARY MANAGEMENT PROJECT
using System;
using System.Collections.Generic;

// Abstract base class for Book
public abstract class Book
{
    //defining the attributes(called properties in concept of Encapsulation) of the class Book
    //We cannot create the objects of an abstract class
    public string Title { get; set; }
    public string Author { get; set; }
    public int BookID { get; set; }

    //Initializing a constructor of Book class 
    //Constructor should always has the same name as that of class
    public Book(string title, string author, int bookID)
    {
        //getting its arguments saved to the attributes of the class
        Title = title;
        Author = author;
        BookID = bookID;
    }
    //Here in this case abstract method is defined to print the book's information but it is implemented by its derived classes
    //that is why it is labelled as abstract base class
    //generally an abstract method is made in an abstract class and its implementation can be done in any other class
    public abstract void DisplayInfo();
}

// Derived class FictionBook inheriting from Book
public class FictionBook : Book
{
    //defining the attribute(called properties in concept of Encapsulation) of the FictionBook Book
    public string Genre { get; set; }

    //Initializing a constructor of FictionBook class 
    public FictionBook(string title, string author, int bookID, string genre)
        : base(title, author, bookID)//from base class
    {
        Genre = genre;//three argumnents are already saved to the attributes of the parent class and this forth argument genre is saved to the only single attribute in this derived class
    }
    //overriding to display the info for this specific derived class FictionBook
    public override void DisplayInfo()
    {
        Console.WriteLine(" -----------------------");
        Console.WriteLine($"|  Book ID: {BookID}");
        Console.WriteLine($"|  Title  : {Title}");
        Console.WriteLine($"|  Author : {Author}");
        Console.WriteLine($"|  Genre  : {Genre}");
        Console.WriteLine(" -----------------------");
    }
}

// Derived class NonFictionBook inheriting from Book
public class NonFictionBook : Book
{
    //defining the attribute(called properties in concept of Encapsulation) of the NonFictionBook Book
    public string Subject { get; set; }

    //Initializing a constructor of NonFictionBook class 
    public NonFictionBook(string title, string author, int bookID, string subject)
        : base(title, author, bookID)//from base class
    {
        Subject = subject;//three argumnents are already saved to the attributes of the parent class and this forth argument subject is saved to the only single attribute in this derived class
    }
    //overriding to display the info for this specific derived class NonFictionBook
    public override void DisplayInfo()
    {
        Console.WriteLine(" -----------------------");
        Console.WriteLine($"|  Book ID  : {BookID}");
        Console.WriteLine($"|  Title    : {Title}");
        Console.WriteLine($"|  Author   : {Author}");
        Console.WriteLine($"|  Subject  : {Subject}");
        Console.WriteLine(" -----------------------");
    }
}

// Base class Person
public class Person
{
    //defining the attributes(called properties in concept of Encapsulation) of the class Person
    public string Name { get; set; }
    public int Age { get; set; }
    public int PersonID { get; set; }

    //Initializing a constructor of Person class 
    //Constructor should always has the same name as that of class
    public Person(string name, int age, int personID)
    {
        //getting its arguments saved to the attributes of the class
        Name = name;
        Age = age;
        PersonID = personID;
    }
    //its  a base class because its derived classes inherit all the attributes from it 
}

// Derived class Librarian inheriting from Person
public class Librarian : Person
{
    //defining the attribute(called property in concept of Encapsulation) of the Librarian class
    public int EmployeeID { get; set; }

    public Librarian(string name, int age, int personID, int employeeID)
        : base(name, age, personID)
    {
        EmployeeID = employeeID;
    }

    public void IssueBook(Book book, Person user)
    {
        Console.WriteLine("BOOK ISSUED SUCCESSFULLY!!!");
        Console.WriteLine(" ---------------------------");
        Console.WriteLine($"| Book       : {book.Title}");
        Console.WriteLine($"| Issued To  : {user.Name}");
        Console.WriteLine($"| Issued By  : {Name} (ID: {EmployeeID})");
        Console.WriteLine(" ---------------------------");
    }

    public void ReturnBook(Book book, Person user)
    {
        Console.WriteLine("BOOK RETURNED SUCCESSFULLY!!!");
        Console.WriteLine(" -----------------------------");
        Console.WriteLine($"| Book         : {book.Title}");
        Console.WriteLine($"| Returned By  : {user.Name}");
        Console.WriteLine($"| Returned To  : {Name} (ID: {EmployeeID})");
        Console.WriteLine(" -----------------------------");
    }
}

// Library class
public class Library
{
    public string LibraryName { get; set; }
    public int LibraryID { get; set; }
    private List<Book> books;
    private Librarian librarian;
    private Dictionary<Book, Person> issuedBooks;
    private List<string> transactionHistory; //list to store transaction history
    private int nextBookID;//to assign a unique id to every book when it is added

    //Constructor for Library class
    public Library(string libraryName, int libraryID, Librarian librarian)
    {
        LibraryName = libraryName;
        LibraryID = libraryID;
        books = new List<Book>();
        this.librarian = librarian;
        issuedBooks = new Dictionary<Book, Person>();
        transactionHistory = new List<string>(); // Initialize transaction history list
        nextBookID = 1;
    }


    public void AddBook(Book book, int librarianID)
    {
        if (librarian.EmployeeID != librarianID)
        {
            Console.WriteLine("Invalid librarian ID.");
            return;
        }

        book.BookID = nextBookID++;
        books.Add(book);
        Console.WriteLine($"Book '{book.Title}' added successfully to {LibraryName} by {librarian.Name} (ID: {librarian.EmployeeID}) with Book ID {book.BookID}.");
    }

    public void RemoveBook(int bookID, int librarianID)
    {
        if (librarian.EmployeeID != librarianID)
        {
            Console.WriteLine("Invalid librarian ID.");
            return;
        }

        Book bookToRemove = books.Find(b => b.BookID == bookID);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"Book '{bookToRemove.Title}' removed successfully from {LibraryName} by {librarian.Name} (ID: {librarian.EmployeeID}).");
        }
        else
        {
            Console.WriteLine("Book not found in the library.");
        }
    }
    // Method to log transaction history
    public void LogTransaction(string transactionDetails)
    {
        transactionHistory.Add(transactionDetails);
    }

    // Method to view transaction history (accessible only to librarian)
    public void ViewTransactionHistory(int librarianID)
    {
        if (librarian.EmployeeID == librarianID)
        {
            Console.WriteLine("\nTransaction History:");
            foreach (var transaction in transactionHistory)
            {
                Console.WriteLine(transaction);
            }
        }
        else
        {
            Console.WriteLine("Invalid librarian ID. Access denied.");
        }
    }
    public void ViewBooks()
    {
        if (books.Count == 0)//checks books from list books
        {
            Console.WriteLine("No books registered in the library.");
        }
        else
        {
            Console.WriteLine($"Books in {LibraryName}:");
            foreach (var book in books)
            {
                book.DisplayInfo();
            }
        }
    }

    public void ListBooksBySection(string section)
    {
        bool booksFound = false;

        if (section.Equals("Fiction", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Fiction Books:");
            foreach (var book in books)
            {
                if (book is FictionBook)
                {
                    book.DisplayInfo();
                    booksFound = true;
                }
            }
        }
        else if (section.Equals("Non Fiction", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Non Fiction Books:");
            foreach (var book in books)
            {
                if (book is NonFictionBook)
                {
                    book.DisplayInfo();
                    booksFound = true;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid section.");
            return;
        }

        if (!booksFound)
        {
            Console.WriteLine($"No {section} books added at this time.");
        }
    }

    // Method to search for a book by its title
    public Book SearchBookByTitle(string bookTitle)
    {
        Book foundBook = books.Find(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
        return foundBook;
    }
    public void IssueBook(string bookTitle, string userName, int userAge, int userID, int librarianID)
    {
        if (librarian.EmployeeID != librarianID)
        {
            Console.WriteLine("Invalid librarian ID.");
            return;
        }

        Book bookToIssue = books.Find(b => b.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase));
        Person user = new Person(userName, userAge, userID);

        if (bookToIssue == null)
        {
            Console.WriteLine("Book not found in the library.");
        }
        else
        {
            books.Remove(bookToIssue);//removes book from the library after it is issued
            librarian.IssueBook(bookToIssue, user);
            issuedBooks.Add(bookToIssue, user);//adds that book to the issuedBooks list
        }
    }

    public void ReturnBook(string bookTitle, string userName, int userAge, int userID, int librarianID)
    {
        if (librarian.EmployeeID != librarianID)
        {
            Console.WriteLine("Invalid librarian ID.");
            return;
        }

        Book bookToReturn = null;
        foreach (var book in issuedBooks.Keys)
        {
            if (book.Title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase))
            {
                bookToReturn = book;
                break;
            }
        }

        if (bookToReturn == null)
        {
            Console.WriteLine("Book not found in issued books.");
            return;
        }

        Person user = new Person(userName, userAge, userID);
        librarian.ReturnBook(bookToReturn, user);//book is returned through librarian
        issuedBooks.Remove(bookToReturn);//remove book from the issuedBooks list
        books.Add(bookToReturn);//add book to the library after it is returned
    }

    public void ViewIssuedBooks()
    {
        if (issuedBooks.Count == 0)
        {
            Console.WriteLine("No books issued.");
        }
        else
        {
            Console.WriteLine("Issued Books:");
            foreach (var entry in issuedBooks)
            {
                entry.Key.DisplayInfo();
                Console.WriteLine($"Issued to: {entry.Value.Name} (ID: {entry.Value.PersonID}, Age: {entry.Value.Age})");
                Console.WriteLine("--------------------------------------");
            }
        }
    }
}

// Main class with entry point
public class Program
{
    public static void Main()
    {
        // Initializing a librarian object from the class Librarian using its constructor
        Librarian librarian = new Librarian("Mr. Akbar", 35, 1, 1001);

        // Initializing a library object from the class Library using its constructor
        Library library = new Library("Central Library", 101, librarian);

        //Menu for user input
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nLibrary Management System");
            Console.WriteLine("1. View Books");
            Console.WriteLine("2. List Books by Section");
            Console.WriteLine("3. Issue Book");
            Console.WriteLine("4. Return Book");
            Console.WriteLine("5. Add Book");
            Console.WriteLine("6. Remove Book");
            Console.WriteLine("7. View Issued Books");
            Console.WriteLine("8. Search Book by Title");
            Console.WriteLine("9. View Transaction History"); 
            Console.WriteLine("10. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    library.ViewBooks();
                    break;
                case "2":
                    Console.Clear();
                    Console.Write("Enter section (Fiction/Non Fiction): ");
                    string section = Console.ReadLine();
                    library.ListBooksBySection(section);
                    break;
                case "3":
                    Console.Clear();
                    Console.Write("Enter book title: ");
                    string issueTitle = Console.ReadLine();
                    Console.Write("Enter borrower name: ");
                    string issueUserName = Console.ReadLine();
                    Console.Write("Enter borrower age: ");
                    int issueUserAge = int.Parse(Console.ReadLine());
                    Console.Write("Enter borrower ID: ");
                    int issueUserID = int.Parse(Console.ReadLine());
                    Console.Write("Enter librarian ID: ");
                    int issueLibrarianID = int.Parse(Console.ReadLine());
                    library.IssueBook(issueTitle, issueUserName, issueUserAge, issueUserID, issueLibrarianID);
                    library.LogTransaction($"Book '{issueTitle}' issued by {librarian.Name} (ID: {librarian.EmployeeID})");//transaction history is stored as string in log transaction
                    break;
                case "4":
                    Console.Clear();
                    Console.Write("Enter book title: ");
                    string returnTitle = Console.ReadLine();
                    Console.Write("Enter borrower name: ");
                    string returnUserName = Console.ReadLine();
                    Console.Write("Enter borrower age: ");
                    int returnUserAge = int.Parse(Console.ReadLine());
                    Console.Write("Enter borrower ID: ");
                    int returnUserID = int.Parse(Console.ReadLine());
                    Console.Write("Enter librarian ID: ");
                    int returnLibrarianID = int.Parse(Console.ReadLine());
                    library.ReturnBook(returnTitle, returnUserName, returnUserAge, returnUserID, returnLibrarianID);
                    library.LogTransaction($"Book '{returnTitle}' received {librarian.Name} (ID: {librarian.EmployeeID})");
                    break;
                case "5":
                    Console.Clear();
                    Console.Write("Enter book type (Fiction/Non Fiction): ");
                    string bookType = Console.ReadLine();
                    Console.Write("Enter book title: ");
                    string newTitle = Console.ReadLine();
                    Console.Write("Enter book author: ");
                    string newAuthor = Console.ReadLine();
                    Console.Write("Enter librarian ID: ");
                    int addLibrarianID = int.Parse(Console.ReadLine());

                    if (bookType.Equals("Fiction", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("Enter genre: ");
                        string genre = Console.ReadLine();
                        FictionBook newFictionBook = new FictionBook(newTitle, newAuthor, 0, genre);
                        library.AddBook(newFictionBook, addLibrarianID);
                        library.LogTransaction($"Fiction Book '{newTitle}' added by {librarian.Name} (ID: {librarian.EmployeeID})");
                    }
                    else if (bookType.Equals("Non Fiction", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Write("Enter subject: ");
                        string subject = Console.ReadLine();
                        NonFictionBook newNonFictionBook = new NonFictionBook(newTitle, newAuthor, 0, subject);
                        library.AddBook(newNonFictionBook, addLibrarianID);
                        library.LogTransaction($"Non Fiction Book '{newTitle}' added by {librarian.Name} (ID: {librarian.EmployeeID})");
                    }
                    else
                    {
                        Console.WriteLine("Invalid book type.");
                    }
                    break;
                case "6":
                       Console.Clear();
                    Console.Write("Enter book ID to remove: ");
                    int bookIDToRemove = int.Parse(Console.ReadLine());
                    Console.Write("Enter librarian ID: ");
                    int removeLibrarianID = int.Parse(Console.ReadLine());
                    library.RemoveBook(bookIDToRemove, removeLibrarianID);
                    library.LogTransaction($"Book ID '{bookIDToRemove}' removed by {librarian.Name} (ID: {librarian.EmployeeID})");
                    break;
                case "7":
                    Console.Clear();
                    library.ViewIssuedBooks();
                    break;
                case "8":
                    Console.Clear();
                    Console.Write("Enter book title to search: ");
                    string searchTitle = Console.ReadLine();
                    Book foundBook = library.SearchBookByTitle(searchTitle);
                    if (foundBook != null)
                    {
                        Console.WriteLine("Book Found:");
                        foundBook.DisplayInfo();
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                    break;
                case "9":
                    Console.Clear();
                    Console.Write("Enter librarian ID to view transaction history: ");
                    int viewHistoryLibrarianID = int.Parse(Console.ReadLine());
                    library.ViewTransactionHistory(viewHistoryLibrarianID);
                    break;
                case "10":
                    Console.Clear();
                    running = false;
                    break;
                default:
                    Console.Clear() ;
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
