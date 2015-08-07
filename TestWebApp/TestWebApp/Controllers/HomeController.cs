using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        dbDealingsEntities db = new dbDealingsEntities();
        
        //
        // GET: /Home/
        public ActionResult Index()
        {
            
            return View(db.PR.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PR pr)
        {
            if(ModelState.IsValid)
            {
                ViewBag.Message = "";

                if(!db.PR.ToList().Exists(x=>x.SNUM==pr.SNUM))
                {
                    db.PR.Add(pr);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Message = "Такой номер уже существует!";
                    return View();
                }   

                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /PR/Details/5
        public ActionResult Details(int id)
        {
            PR pr = db.PR.ToList().Find(x => x.SNUM == id);

            return View(pr);
        }

        //
        // GET: /PR/Edit/5
        public ActionResult Edit(int id)
        {
            PR pr = db.PR.Find(id);

            return View(pr);
        }

        //
        // POST: /PR/Edit/5
        [HttpPost]
        public ActionResult Edit(PR pr)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    PR oldPR = db.PR.ToList().Find(x => x.SNUM == pr.SNUM);
                    PR newPR = pr;

                    db.Entry(oldPR).CurrentValues.SetValues(newPR);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PR/Delete/5
        public ActionResult Delete(int id)
        {
            PR pr = db.PR.ToList().Find(x => x.SNUM == id);

            return View(pr);
        }

        //
        // POST: /PR/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                PR pr = db.PR.ToList().Find(x => x.SNUM == id);

                db.PR.Remove(pr);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
	}
}