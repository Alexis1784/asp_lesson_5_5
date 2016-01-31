using asp_lesson_3_1.Models;
using asp_lesson_3_1.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asp_lesson_3_1.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            ViewBag.BookId = id;
            return View();
        }
        
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = getToday();
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }
        private DateTime getToday()
        {
            return DateTime.Now;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //public string Square(int a = 10, int h = 3)
        //{
        //    double s = a * h / 2;
        //    return "<h2>Площадь треугольника с основанием " + a +
        //            " и высотой " + h + " равна " + s + "</h2>";
        //}
        public string Square()
        {
            int a = Int32.Parse(Request.Params["a"]);
            int h = Int32.Parse(Request.Params["h"]);
            double s = a * h / 2;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }
        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2>Привет мир!</h2>");
        }
    }
}