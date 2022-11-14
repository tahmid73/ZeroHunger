using assignments.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace assignments.Controllers
{
    public class NgoController : Controller
    {
        // GET: Ngo
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ShowRestaurant()
        {
            var db = new NGOEntities3();
            return View(db.restaurants.ToList());
        }

        [HttpGet]
        public ActionResult ShowRequest()
        {
            var db = new NGOEntities3();
            return View(db.requests.ToList());
        }

        [HttpGet]
        public ActionResult ShowEmployee()
        {
            var db = new NGOEntities3();
            return View(db.employees.ToList());
        }

        [HttpGet]
        public ActionResult ShowFood()
        {
            var db = new NGOEntities3();
            return View(db.foods.ToList());
        }

        [HttpGet]
        public ActionResult AddRestaurant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRestaurant(restaurant r)
        {
            var db = new NGOEntities3();
            db.restaurants.Add(r);
            db.SaveChanges();
            return RedirectToAction("ShowRestaurant");
        }

        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(employee e)
        {
            var db = new NGOEntities3();
            db.employees.Add(e);
            db.SaveChanges();
            return RedirectToAction("ShowEmployee");
        }


        [HttpGet]
        public ActionResult AddRequest(int id)
        {
            var db = new NGOEntities3();
            var resId = (from r in db.restaurants
                         where r.r_id == id
                         select r).SingleOrDefault();
            return View(resId);
        }

        [HttpPost]
        public ActionResult AddRequest(request r, string[]name, int[]quantity)
        {
            var db = new NGOEntities3();
            var req = new request();
            {
                r_id = r.r_id,
                r.status= "Pending"
            };
            db.requests.Add(req);
            db.SaveChanges();
            for(int i = 0; i < name.Length; i++)
            {
                if (!name[i].Equals(""))
                {
                    db.foods.Add(new food()
                    {
                        req_id = req.r_id,
                        f_name = name[i],
                    });
                }
            }
            return RedirectToAction("ShowRequest");
        }
    }
}