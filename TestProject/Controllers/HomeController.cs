using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TestProject.Models;
using TestProject.ViewModels;

namespace TestProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();             
        }

        public ActionResult Action(Subscriber subscriber)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationContext();
                db.Subscribers.Add(subscriber);
                db.SaveChanges();
                ViewBag.Message = "Данные добавлены";
                return View("Index");
            }
            else
            {
                ViewBag.Message = "Введите E-mail";
                return View("Index");
            }
           
        }
    }
}