using BookManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManager.Controllers
{
    public class BookController : Controller
    {
        BookManagerContext context = new BookManagerContext();
        // GET: Book
        public ActionResult Index()
        {
            var listBook = context.Books.ToList();
            return View(listBook);
        }
        public ActionResult Details(int id)
        {
            var D_Sach = context.Books.Where(m => m.ID == id).First();
            return View(D_Sach);
        }
        [Authorize]
        public ActionResult Buy(int id)
        {
            Book book = context.Books.SingleOrDefault(x => x.ID == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        public ActionResult Edit(int id)
        {
            var book = context.Books.First(x => x.ID == id);
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_sach = context.Books.First(m => m.ID == id);
            var E_Title = collection["Title"];
            var E_Description = collection["Description"];
            var E_Author = collection["Author"];
            var E_Image = collection["Images"];
            var E_Price = Convert.ToInt32(collection["Price"]);
            E_sach.ID = id;
            if (string.IsNullOrEmpty(E_Title))
            {
                ViewData["Error"] = "Không được để trống.";
            }
            else
            {
                E_sach.Title = E_Title;
                E_sach.Description = E_Description;
                E_sach.Author = E_Author;
                E_sach.Images = E_Image;
                E_sach.Price = E_Price;
                UpdateModel(E_sach);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book,FormCollection collection)
        {
            var E_Title = collection["Title"];
            var E_Description = collection["Description"];
            var E_Author = collection["Author"];
            var E_Image = collection["Images"];
            var E_Price = collection["Price"];
            if (string.IsNullOrEmpty(E_Title))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                book.Title = E_Title.ToString();
                book.Description = E_Description.ToString();
                book.Author = E_Author;
                book.Images = E_Image;
                book.Price = int.Parse(E_Price);
                context.Books.AddOrUpdate(book);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Delete(int id)
        {
            var dbDelete = context.Books.First(m => m.ID == id);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult Delete(int id,FormCollection collection)
        {
            var dbDelete = context.Books.Where(m => m.ID == id).First();
            context.Books.Remove(dbDelete);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}