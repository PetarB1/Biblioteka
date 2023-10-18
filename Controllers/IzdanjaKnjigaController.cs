using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka.Models;

namespace Biblioteka.Controllers
{
    public class IzdanjaKnjigaController : Controller
    {
        private BibliotekaEntities db = new BibliotekaEntities();

        // GET: IzdanjaKnjiga
        public ActionResult Index()
        {
            var izdanjaKnjiga = db.IzdanjaKnjiga.Include(i => i.IzdavackeKuce).Include(i => i.Knjige);
            return View(izdanjaKnjiga.ToList());
        }

        // GET: IzdanjaKnjiga/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IzdanjaKnjiga izdanjaKnjiga = db.IzdanjaKnjiga.Find(id);
            if (izdanjaKnjiga == null)
            {
                return HttpNotFound();
            }
            return View(izdanjaKnjiga);
        }

        // GET: IzdanjaKnjiga/Create
        public ActionResult Create()
        {
            ViewBag.IzdavackaKucaId = new SelectList(db.IzdavackeKuce, "Id", "Naziv");
            ViewBag.KnjigeId = new SelectList(db.Knjige, "Id", "Naslov");
            return View();
        }

        // POST: IzdanjaKnjiga/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IzdanjaKnjiga izdanjaKnjigaModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(izdanjaKnjigaModel.FileSlikaKorica.FileName);
            string extension = Path.GetExtension(izdanjaKnjigaModel.FileSlikaKorica.FileName);

            fileName = fileName + DateTime.Now.ToString("yyMMddHHmmssfff") + extension;
            izdanjaKnjigaModel.SlikaKorica = "~/Images/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
            izdanjaKnjigaModel.FileSlikaKorica.SaveAs(fileName);

            using (var _context = new BibliotekaEntities())
            {
                _context.IzdanjaKnjiga.Add(izdanjaKnjigaModel);
                _context.SaveChanges();
            }
            ModelState.Clear();
            return View();
        }
        // GET: IzdanjaKnjiga/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IzdanjaKnjiga izdanjaKnjiga = db.IzdanjaKnjiga.Find(id);
            if (izdanjaKnjiga == null)
            {
                return HttpNotFound();
            }
            ViewBag.IzdavackaKucaId = new SelectList(db.IzdavackeKuce, "Id", "Naziv", izdanjaKnjiga.IzdavackaKucaId);
            ViewBag.KnjigeId = new SelectList(db.Knjige, "Id", "Naslov", izdanjaKnjiga.KnjigeId);
            return View(izdanjaKnjiga);
        }

        // POST: IzdanjaKnjiga/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KnjigeId,IzdavackaKucaId,SlikaKorica,Godina,BrojNaStanju,BrojIzdatih")] IzdanjaKnjiga izdanjaKnjiga)
        {
            if (ModelState.IsValid)
            {
                db.Entry(izdanjaKnjiga).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IzdavackaKucaId = new SelectList(db.IzdavackeKuce, "Id", "Naziv", izdanjaKnjiga.IzdavackaKucaId);
            ViewBag.KnjigeId = new SelectList(db.Knjige, "Id", "Naslov", izdanjaKnjiga.KnjigeId);
            return View(izdanjaKnjiga);
        }

        // GET: IzdanjaKnjiga/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IzdanjaKnjiga izdanjaKnjiga = db.IzdanjaKnjiga.Find(id);
            if (izdanjaKnjiga == null)
            {
                return HttpNotFound();
            }
            return View(izdanjaKnjiga);
        }

        // POST: IzdanjaKnjiga/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IzdanjaKnjiga izdanjaKnjiga = db.IzdanjaKnjiga.Find(id);
            db.IzdanjaKnjiga.Remove(izdanjaKnjiga);
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
