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
                r.status= "Pending",
                preserve_time = r.preserve_time,
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
            db.SaveChanges();
            return RedirectToAction("ShowRequest");
        }

        public ActionResult DeleteReq(int id)
        {
            var db = new NGOEntities3();
            var i = (from req in db.foods
                     where req.req_id == id
                     select req).ToList();
            foreach (var item in i)
            {
                db.foods.Remove(item);
                db.SaveChanges();

            }
            var r = (from req in db.requests
                     where req.req_id == id
                     select req).SingleOrDefault();
            db.requests.Remove(r);
            db.SaveChanges();


            return RedirectToAction("ShowRequest");
        }

        [HttpGet]
        public ActionResult CheckRequest(int id)
        {
            var db = new NGOEntities3();
            var name = (from n in db.employees
                        where n.e_id == id
                        select n).SingleOrDefault();
            ViewBag.id = id;
            ViewBag.name = name.e_name;
            var req = (from r in db.requests
                       where r.req_id == id
                       select r).ToList();

            return View(req);
        }

        public ActionResult AcceptRequest(int id)
        {
            var db = new NGOEntities3();
            var req = (from r in db.requests
                       where r.req_id == id
                       select r).SingleOrDefault();
            req.status = "Accepted";
            db.SaveChanges();
            return RedirectToAction("ShowEmployee");
        }
        public ActionResult cancleRequest(int id)
        {
            var db = new NGOEntities3();
            var req = (from r in db.requests
                       where r.req_id == id
                       select r).SingleOrDefault();
            req.status = "Rejected";
            db.SaveChanges();
            return RedirectToAction("ShowEmployee");
        }
        public ActionResult CompleteRequest(int id)
        {
            var db = new NGOEntities3();
            var req = (from r in db.requests
                       where r.req_id == id
                       select r).SingleOrDefault();
            req.status = "Completed";
            db.SaveChanges();
            return RedirectToAction("ShowEmployee");
        }

    }
}