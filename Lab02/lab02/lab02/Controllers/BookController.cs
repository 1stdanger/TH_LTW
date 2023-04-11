using lab02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab02.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public string HelloTeacher(string univesity)
        {
            return "Hello" + univesity;
        }
        public ActionResult ListBook()
        {
            var books = new List<string>();
            books.Add("HTML5 & CSS The Complete Manual - Author Name Book 1");
            books.Add("HTML5 & CSS Resposive web Design cookbook - Author Name Book 2");
            books.Add("Professional ASP.NET MVC5 - Author Name Book 2");
            ViewBag.Books = books;
            return View();
        }
        public ActionResult ListBookModel() 
        { 
            var books = new List<Book>();
            books.Add(new Book(1,"HTML5 & CSS The Complete Manual", "Author Name Book 1", "/Content/Images/book1.jpg"));
            books.Add(new Book(2, "HTML5 & CSS Resposive web Design cookbook", "Author Name Book 2", "/Content/Images/book2.jpg"));
            books.Add(new Book(3, "Professional ASP.NET MVC5", "Author Name Book 3", "/Content/Images/book3.jpg"));
            return View(books);
        }

        public ActionResult EditBook(int id)
        {
            var books = new List<Book>();
            books.Add(new Book(1, "HTML5 & CSS The Complete Manual", "Author Name Book 1", "/Content/Images/book1.jpg"));
            books.Add(new Book(2, "HTML5 & CSS Resposive web Design cookbook", "Author Name Book 2", "/Content/Images/book2.jpg"));
            books.Add(new Book(3, "Professional ASP.NET MVC5", "Author Name Book 3", "/Content/Images/book3.jpg"));
            Book book = new Book();
            foreach (Book b in books)
            {
                if (b.Id == id)
                {
                    book = b;
                    break;
                }
            }
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);

        }
        [HttpPost, ActionName("EditBook")]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(int id, string Title, string Author, string ImageCover)
        {
            var books = new List<Book>();
            books.Add(new Book(1, "HTML5 & CSS The Complete Manual", "Author Name Book 1", "/Content/Images/book1.jpg"));
            books.Add(new Book(2, "HTML5 & CSS Resposive web Design cookbook", "Author Name Book 2", "/Content/Images/book2.jpg"));
            books.Add(new Book(3, "Professional ASP.NET MVC5", "Author Name Book 3", "/Content/Images/book3.jpg"));
            if (id == null)
            {
                return HttpNotFound();
            }
            foreach (Book b in books)
            {
                if (b.Id == id)
                {
                    b.Title = Title;
                    b.Author = Author;
                    b.ImageCover = ImageCover;
                    break;
                }
            }
            return View("ListBookModel",books);
        }
        public ActionResult CreateBook() 
        {
            return View();
        }
        [HttpPost, ActionName("CreateBook")]
        [ValidateAntiForgeryToken]
        public ActionResult Contact([Bind(Include = "Id,Title,Author,ImageCover")]Book book) 
        {
            var books = new List<Book>();
            books.Add(new Book(1, "HTML5 & CSS The Complete Manual", "Author Name Book 1", "/Content/Images/book1.jpg"));
            books.Add(new Book(2, "HTML5 & CSS Resposive web Design cookbook", "Author Name Book 2", "/Content/Images/book2.jpg"));
            books.Add(new Book(3, "Professional ASP.NET MVC5", "Author Name Book 3", "/Content/Images/book3.jpg"));
            try
            {
                if (ModelState.IsValid)
                {
                    books.Add(book);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Error Save Data");
            }
            return View("ListBookModel",books);
        }
        public ActionResult DeleteBook(int? id)
        {
            var books = new List<Book>();
            books.Add(new Book(1, "HTML5 & CSS The Complete Manual", "Author Name Book 1", "/Content/Images/book1.jpg"));
            books.Add(new Book(2, "HTML5 & CSS Resposive web Design cookbook", "Author Name Book 2", "/Content/Images/book2.jpg"));
            books.Add(new Book(3, "Professional ASP.NET MVC5", "Author Name Book 3", "/Content/Images/book3.jpg"));

            var st = books.Find(x => x.Id == id);
            books.Remove(st);

            return View("ListBookModel", books);
        }
    }
}