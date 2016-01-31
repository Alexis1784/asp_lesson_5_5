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
        public ActionResult GetImage()
        {
            string path = "../Images/visualstudio.png";
            return new ImageResult(path);
        }

        public ContentResult Square2(int a, int h)
        {
            int s = a * h / 2;
            return Content("<h2>Площадь треугольника с основанием " + a +
                    " и высотой " + h + " равна " + s + "</h2>");
        }

        public ViewResult SomeMethod()
        {
            ViewData["Head"] = "Привет мир!";
            return View("SomeView");
        }

        public ViewResult SomeMethod2()
        {
            ViewBag.Head = "Привет мир!";
            return View("SomeView");
        }

        //http://metanit.com/sharp/mvc5/3.5.php
        public RedirectResult SomeMethod3()
        {
            return Redirect("/Home/Index");
        }

        public RedirectResult SomeMethod4()
        {
            return RedirectPermanent("/Home/Index");
        }

        public ActionResult Buy2(int id)
        {
            if (id > 3)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.BookId = id;
            return View("Buy");
        }
        public RedirectToRouteResult SomeMethod5()
        {
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
        public RedirectToRouteResult SomeMethod6()
        {
            return RedirectToAction("Square", "Home", new { a = 10, h = 12 });
        }

        public ActionResult Check(int age)
        {
            if (age < 21)
            {
                return new HttpStatusCodeResult(404);
            }
            return View("Buy");
        }

        public ActionResult Check2(int age)
        {
            if (age < 21)
            {
                return HttpNotFound();
            }
            return View("Buy");
        }

        public ActionResult Check3(int age)
        {
            if (age < 21)
            {
                return new HttpUnauthorizedResult();
            }
            return View("Buy");
        }
    }
}