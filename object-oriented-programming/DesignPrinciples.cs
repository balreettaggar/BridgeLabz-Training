using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace object_oriented_programming
{
    internal class Book
    {
        private string name;
        private string author;

        internal Book() : this("", "") { }
        internal Book(string name, string author)
        {
            this.name = name;
            this.author = author;
        }

        internal void Display()
        {
            Console.Write($"Bookname : {name}, Author : {author}");
        }
    }

    internal class LibraryClass
    {
        private string libraryName;
        private List<Book> books;

        internal LibraryClass() : this("")
        {
        }

        internal LibraryClass(string libraryName)
        {
            this.libraryName = libraryName;
            books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }
        
        internal void Display()
        {
            Console.WriteLine($"Library Name : {libraryName}");
            foreach(Book book in books)
            {
                book.Display();
                Console.WriteLine();
            }
        }
    }

    internal class BankClass
    {
        private string bankName;

        internal BankClass() : this("")
        {
        }

        internal BankClass(string bankName)
        {
            this.bankName = bankName;
        }

        internal void OpenAccount(Customer customer)
        {
            Console.WriteLine($"Customer name : {customer.name}\nBank name : {bankName}");
        }
        internal void getName()
        {
            Console.WriteLine($"Bank details : {bankName}");
        }
    }

    internal class Customer
    {
        internal string name;
        private int accountNumber;
        private double balance;

        internal Customer() : this("", 0, 0.0)
        {

        }

        internal Customer(string name, int accountNumber, double balance)
        {
            this.name = name;
            this.accountNumber = accountNumber;
            this.balance = balance;
        }
    }
    internal class DesignPrinciples
    {

        public static void LibBookMethod()
        {
            Book book1 = new Book("Harry Potter", "J K Rowlings");
            Book book2 = new Book("Deep Work", "Carl Newport");

            LibraryClass lib1 = new LibraryClass("Central Library");
            LibraryClass lib2 = new LibraryClass("New City Library");

            lib1.AddBook(book1);
            lib2.AddBook(book2);

            lib1.Display();
            lib2.Display();
        }

        public static void BankCustomerMethod()
        {
            BankClass bank = new BankClass("SBI");
            Customer customer = new Customer("Balreet", 101, 910.50);
            bank.OpenAccount(customer);

        }
    }
}
