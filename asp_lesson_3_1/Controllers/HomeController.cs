using asp_lesson_3_1.Models;
using asp_lesson_3_1.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        //http://metanit.com/sharp/mvc5/3.6.php

        public FileResult GetFile()
        {
            // Путь к файлу
            string file_path = Server.MapPath("~/Files/PDFIcon.pdf");
            // Тип файла - content-type
            string file_type = "application/pdf";
            // Имя файла - необязательно
            string file_name = "_Дейл Карнеги, Прихоти удачи.pdf";
            return File(file_path, file_type, file_name);
        }

        // Отправка массива байтов
        public FileResult GetBytes()
        {
            string path = Server.MapPath("~/Files/PDFIcon.pdf");
            byte[] mas = System.IO.File.ReadAllBytes(path);
            string file_type = "application/pdf";
            string file_name = "_Дейл Карнеги, Прихоти удачи.pdf";
            return File(mas, file_type, file_name);
        }

        // Отправка потока
        public FileResult GetStream()
        {
            string path = Server.MapPath("~/Files/PDFIcon.pdf");
            // Объект Stream
            FileStream fs = new FileStream(path, FileMode.Open);
            string file_type = "application/pdf";
            string file_name = "_Дейл Карнеги, Прихоти удачи.pdf";
            return File(fs, file_type, file_name);
        }
        //http://metanit.com/sharp/mvc5/3.7.php
        public string Index2()
        {
            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";
        }

        public string ContextData()
        {
            HttpContext.Response.Write("<h1>Hello World</h1>");

            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";
        }

        public void ContextData2()
        {
            HttpContext.Response.Write("<h1>Hello World</h1>");
        }

        public void WorkingWithCookies()
        {
            HttpContext.Response.Cookies["id"].Value = "ca-4353w";
            string id = HttpContext.Request.Cookies["id"].Value;
            HttpContext.Response.Write("<h1>Value of cookie id is: " + id + "</h1>");
        }

        public ActionResult Index3()
        {
            Session["name"] = "Tom";
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View("Index");
        }

        public string GetName()
        {
            var val = Session["name"];
            return val.ToString();
        }

        // асинхронный метод
        public async Task<ActionResult> BookList()
        {
            IEnumerable<Book> books = await Task.Run(() => db.Books);
            ViewBag.Books = books;
            return View("Index");
        }

        //http://metanit.com/sharp/mvc5/4.1.php
        //http://metanit.com/sharp/mvc5/4.2.php
        public ActionResult BookList2()
        {
            return View(db.Books);
        } 

        //http://metanit.com/sharp/mvc5/4.4.php
        public ActionResult Partial()
        {
            ViewBag.Message = "Это частичное представление.";
            return PartialView();
        }
        public ActionResult Index4()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            ViewBag.Message = "Это вызов частичного представления из обычного";
            return View("Index");
        }
        
        //http://metanit.com/sharp/mvc5/4.5.php
        public ActionResult Index5()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            ViewBag.Message = "Это вызов частичного представления из обычного";
            return View();
        }

        public ActionResult Index6()
        {
            return View();
        }
        public ActionResult Index7()
        {
            return View();
        }

        //http://metanit.com/sharp/mvc5/4.6.php
        public ActionResult Index8()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View("Index8");
        }

        [HttpGet]
        public ActionResult Buy3(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        [HttpPost]
        public string Buy3(Purchase purchase)
        {
            purchase.Date = getToday();
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }

        public ActionResult Index9()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View("Index9");
        }

        [HttpGet]
        public ActionResult Buy4(int id)
        {
            ViewBag.BookId = id;
            return View();
        }

        [HttpPost]
        public string Buy4(Purchase purchase)
        {
            purchase.Date = getToday();
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return "Спасибо, " + purchase.Person + ", за покупку!";
        }

        public ActionResult Index10()
        {
            SelectList books = new SelectList(db.Books, "Author", "Name");
            ViewBag.Books = books;
            return View("Index10");
        }
        [HttpPost]
        public string Index11(string[] countries)
        {
            string result = "";
            foreach (string c in countries)
            {
                result += c;
                result += ";";
            }
            return "Вы выбрали: " + result;
        }
    }
}