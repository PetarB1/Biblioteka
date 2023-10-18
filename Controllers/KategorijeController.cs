using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class KategorijeController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Kategorije
        public ActionResult Index()
        {
            return View(db.Kategorije.ToList());
        }

        // GET: Kategorije/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorije kategorije = db.Kategorije.Find(id);
            if (kategorije == null)
            {
                return HttpNotFound();
            }
            return View(kategorije);
        }

        // GET: Kategorije/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kategorije/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv")] Kategorije kategorije)
        {
            if (ModelState.IsValid)
            {
                db.Kategorije.Add(kategorije);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategorije);
        }

        // GET: Kategorije/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorije kategorije = db.Kategorije.Find(id);
            if (kategorije == null)
            {
                return HttpNotFound();
            }
            return View(kategorije);
        }

        // POST: Kategorije/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv")] Kategorije kategorije)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategorije).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategorije);
        }

        // GET: Kategorije/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategorije kategorije = db.Kategorije.Find(id);
            if (kategorije == null)
            {
                return HttpNotFound();
            }
            return View(kategorije);
        }

        // POST: Kategorije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategorije kategorije = db.Kategorije.Find(id);

            bool imaVezaneKnjige = db.Knjige.Any(k => k.KategorijaId == id);

            if (imaVezaneKnjige)
            {
                TempData["ErrorMessage"] = "Nemoguće je izbrisati kategoriju jer postoje vezane knjige.";
                return RedirectToAction("Index");
            }

            db.Kategorije.Remove(kategorije);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
