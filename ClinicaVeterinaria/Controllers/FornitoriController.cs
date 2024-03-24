﻿using ClinicaVeterinaria.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FornitoriController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Fornitori
        public ActionResult Index()
        {
            return View(db.Fornitori.ToList());
        }

        // GET: Fornitori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornitori fornitori = db.Fornitori.Find(id);
            if (fornitori == null)
            {
                return HttpNotFound();
            }
            return View(fornitori);
        }

        // GET: Fornitori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornitori/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFornitori,Recapito,Nome,Indirizzo")] Fornitori fornitori)
        {
            if (ModelState.IsValid)
            {
                db.Fornitori.Add(fornitori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fornitori);
        }

        // GET: Fornitori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornitori fornitori = db.Fornitori.Find(id);
            if (fornitori == null)
            {
                return HttpNotFound();
            }
            return View(fornitori);
        }

        // POST: Fornitori/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFornitori,Recapito,Nome,Indirizzo")] Fornitori fornitori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornitori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornitori);
        }

        // GET: Fornitori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornitori fornitori = db.Fornitori.Find(id);
            if (fornitori == null)
            {
                return HttpNotFound();
            }
            return View(fornitori);
        }

        // POST: Fornitori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fornitori fornitori = db.Fornitori.Find(id);
            db.Fornitori.Remove(fornitori);
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
