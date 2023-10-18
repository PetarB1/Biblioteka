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
    public class KnjigeController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: Knjige
        public ActionResult Index()
        {
            var knjige = db.Knjige.Include(k => k.Autori).Include(k => k.Kategorije).ToList();
            return View(knjige);
        }

        // GET: Knjige/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjige knjige = db.Knjige.Find(id);
            if (knjige == null)
            {
                return HttpNotFound();
            }
            return View(knjige);
        }

        // GET: Knjige/Create
        public ActionResult Create()
        {
            var autori = db.Autori.Select(a => new
            {
                Id = a.Id,
                PunNaziv = a.Ime + " " + a.Prezime
            }).ToList();

            ViewBag.AutoriList = new SelectList(autori, "Id", "PunNaziv");
            ViewBag.KategorijeList = new SelectList(db.Kategorije, "Id", "Naziv");
            return View();
        }

        // POST: Knjige/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Naslov,AutorId,GodinaOriginala,KategorijaId,BrojNaStanju,BrojIzdatih")] Knjige knjige)
        {
            if (ModelState.IsValid)
            {
                db.Knjige.Add(knjige);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AutorId = new SelectList(db.Autori, "Id", "Ime", knjige.AutorId);
            ViewBag.KategorijaId = new SelectList(db.Kategorije, "Id", "Naziv", knjige.KategorijaId);
            return View(knjige);
        }

        // GET: Knjige/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjige knjige = db.Knjige.Find(id);
            if (knjige == null)
            {
                return HttpNotFound();
            }

            var autori = db.Autori.Select(a => new
            {
                Id = a.Id,
                PunNaziv = a.Ime + " " + a.Prezime
            }).ToList();

            ViewBag.AutoriList = new SelectList(autori, "Id", "PunNaziv");
            ViewBag.KategorijaId = new SelectList(db.Kategorije, "Id", "Naziv");
            return View(knjige);
        }

        // POST: Knjige/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Naslov,AutorId,GodinaOriginala,KategorijaId,BrojNaStanju,BrojIzdatih")] Knjige knjige)
        {
            if (ModelState.IsValid)
            {
                db.Entry(knjige).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AutorId = new SelectList(db.Autori, "Id", "Ime", knjige.AutorId);
            ViewBag.KategorijaId = new SelectList(db.Kategorije, "Id", "Naziv", knjige.KategorijaId);
            return View(knjige);
        }

        // GET: Knjige/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Knjige knjige = db.Knjige.Find(id);
            if (knjige == null)
            {
                return HttpNotFound();
            }
            
            return View(knjige);
        }

        // POST: Knjige/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Knjige knjige = db.Knjige.Find(id);

            bool imajuIzdanja = db.IzdanjaKnjiga.Any(x => x.KnjigeId == id);

            if (imajuIzdanja)
            {
                TempData["ErrorMessage"] = "Ne možete obrisati knjigu jer već postoje njena izdanja.";
                return RedirectToAction("Index");
            }

            db.Knjige.Remove(knjige);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public FileResult GetSlikaKorica(int izdanjeId)
        //{
        //    var izdanje = db.IzdanjaKnjiga.FirstOrDefault(i => i.Id == izdanjeId);

        //    if (izdanje != null && !string.IsNullOrEmpty(izdanje.SlikaKorica))
        //    {
        //        byte[] imageBytes = System.IO.File.ReadAllBytes(izdanje.SlikaKorica);
        //        return File(imageBytes, "image/jpeg"); // Prilagodite tip fajla ako je drugačiji
        //    }

        //    return null;
        //}

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
