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
    public class IzdavackeKuceController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: IzdavackeKuce
        public ActionResult Index()
        {
            return View(db.IzdavackeKuce.ToList());
        }

        // GET: IzdavackeKuce/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IzdavackeKuce izdavackeKuce = db.IzdavackeKuce.Find(id);
            if (izdavackeKuce == null)
            {
                return HttpNotFound();
            }
            return View(izdavackeKuce);
        }

        // GET: IzdavackeKuce/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IzdavackeKuce/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naziv")] IzdavackeKuce izdavackeKuce)
        {
            if (ModelState.IsValid)
            {
                db.IzdavackeKuce.Add(izdavackeKuce);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(izdavackeKuce);
        }

        // GET: IzdavackeKuce/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IzdavackeKuce izdavackeKuce = db.IzdavackeKuce.Find(id);
            if (izdavackeKuce == null)
            {
                return HttpNotFound();
            }
            return View(izdavackeKuce);
        }

        // POST: IzdavackeKuce/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naziv")] IzdavackeKuce izdavackeKuce)
        {
            if (ModelState.IsValid)
            {
                db.Entry(izdavackeKuce).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(izdavackeKuce);
        }

        // GET: IzdavackeKuce/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IzdavackeKuce izdavackeKuce = db.IzdavackeKuce.Find(id);
            if (izdavackeKuce == null)
            {
                return HttpNotFound();
            }
            return View(izdavackeKuce);
        }

        // POST: IzdavackeKuce/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IzdavackeKuce izdavackeKuce = db.IzdavackeKuce.Find(id);

            bool imaVezaneKnjige = db.IzdanjaKnjiga.Any(k => k.IzdavackaKucaId == id);

            if (imaVezaneKnjige)
            {
                TempData["ErrorMessage"] = "Nemoguće je izbrisati izdavačku kuću jer postoje vezane knjige.";
                return RedirectToAction("Index");
            }

            db.IzdavackeKuce.Remove(izdavackeKuce);
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
