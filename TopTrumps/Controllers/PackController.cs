using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopTrumps.Models.Domain;
using TopTrumps.Models;

namespace TopTrumps.Controllers
{
    public class PackController : Controller
    {
        private TopTrumpsContext db = new TopTrumpsContext();

        //
        // GET: /Pack/

        public ActionResult Index()
        {
            return View(db.Packs.ToList());
        }

        //
        // GET: /Pack/Details/5

        public ActionResult Details(int id = 0)
        {
            Pack pack = db.Packs.Find(id);
            if (pack == null)
            {
                return HttpNotFound();
            }
            return View(pack);
        }

        //
        // GET: /Pack/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pack/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pack pack)
        {
            if (ModelState.IsValid)
            {
                db.Packs.Add(pack);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pack);
        }

        //
        // GET: /Pack/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Pack pack = db.Packs.Find(id);
            if (pack == null)
            {
                return HttpNotFound();
            }
            return View(pack);
        }

        //
        // POST: /Pack/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pack pack)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pack).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pack);
        }

        //
        // GET: /Pack/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Pack pack = db.Packs.Find(id);
            if (pack == null)
            {
                return HttpNotFound();
            }
            return View(pack);
        }

        //
        // POST: /Pack/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pack pack = db.Packs.Find(id);
            db.Packs.Remove(pack);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}