using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;
using Biblioteka.ViewModels;

namespace Biblioteka.Controllers
{
    public class AutoriController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Autori
        public ActionResult Index()
        {
            return View(db.Autori.ToList());
        }

        // GET: Autori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autori.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View(autori);
        }

        // GET: Autori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Autori/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ime,Prezime")] Autori autori)
        {
            if (ModelState.IsValid)
            {
                db.Autori.Add(autori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autori);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(AutorViewModel viewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var autor = new Autori
        //        {
        //            Ime = viewModel.Ime,
        //            Prezime = viewModel.Prezime
        //        };

        //        db.Autori.Add(autor);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(viewModel);
        //}

        // GET: Autori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autori.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            return View(autori);
        }

        // POST: Autori/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ime,Prezime")] Autori autori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autori);
        }

        // GET: Autori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {   
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Autori autori = db.Autori.Find(id);
            if (autori == null)
            {
                return HttpNotFound();
            }
            
            return View(autori);
        }

        // POST: Autori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Autori autori = db.Autori.Find(id);
            bool imaVezaneKnjige = db.Knjige.Any(k => k.AutorId == id);

            if (imaVezaneKnjige)
            {
                TempData["ErrorMessage"] = "Nemoguće je izbrisati autora jer postoje vezane knjige.";
                return RedirectToAction("Index"); 
            }
            db.Autori.Remove(autori);
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
