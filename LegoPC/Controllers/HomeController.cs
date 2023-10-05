using LegoPC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LegoPC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        AccessoryContext db = new AccessoryContext();
        public ActionResult Index()
        {
            // Отримуємо з БД всі об'єкти Book
            IEnumerable<Accessory> accessories = db.Accessories;
            // Передаємо всі об'єкти в динамічну властивість Books в ViewBag
            ViewBag.Accessories = accessories;
            // Повертаємо представлення
            return View();
        }
    }
}