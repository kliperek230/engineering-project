using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using foodmateapp.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace foodmateapp.Controllers
{
    public class LiftsController : Controller
    {
        FoodmateContext db = new FoodmateContext();
        public IActionResult Index()
        {
            return View(db.Lifts.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewLifts(Lifts lifts)
        {
            db.Lifts.Add(lifts);
            db.SaveChanges();
            return RedirectToAction("Index", "Lifts");
        }

        [HttpPost]
        public bool Delete(int id)
        {
            try
            {
                Lifts lift = db.Lifts.Where(s => s.LiftId == id).First();
                db.Lifts.Remove(lift);
                db.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
                        
        }
        public ActionResult Update(int id)
        {
            return View(db.Lifts.Where(s => s.LiftId == id).First());
        }

        [HttpPost]
        public ActionResult UpdateLift (Lifts lift)
        {
            Lifts l = db.Lifts.Where(s => s.LiftId == lift.LiftId).First();
            l.LiftName = lift.LiftName;
            l.MDate = lift.MDate;
            l.Value = lift.Value;
            db.SaveChanges();
            return RedirectToAction("Index", "Lifts");
        }

    }
}