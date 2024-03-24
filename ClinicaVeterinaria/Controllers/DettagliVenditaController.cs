using ClinicaVeterinaria.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Admin, Farmacista")]
    public class DettagliVenditaController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: DettagliVendita
        public ActionResult Index()
        {
            var dettagliVendita = db.DettagliVendita.Include(d => d.Prodotti).Include(d => d.Vendite);
            return View(dettagliVendita.ToList());
        }

        // GET: DettagliVendita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliVendita dettagliVendita = db.DettagliVendita.Find(id);
            if (dettagliVendita == null)
            {
                return HttpNotFound();
            }
            return View(dettagliVendita);
        }

        // GET: DettagliVendita/Create
        public ActionResult Create()
        {
            ViewBag.Id_Prodotto_FK = new SelectList(db.Prodotti, "IdProdotto", "Tipo");
            ViewBag.Id_Vendita_FK = new SelectList(db.Vendite, "IdVendita", "Proprietario");
            return View();
        }

        // POST: DettagliVendita/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDettagli,Id_Vendita_FK,Id_Prodotto_FK")] DettagliVendita dettagliVendita)
        {
            if (ModelState.IsValid)
            {
                db.DettagliVendita.Add(dettagliVendita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Prodotto_FK = new SelectList(db.Prodotti, "IdProdotto", "Tipo", dettagliVendita.Id_Prodotto_FK);
            ViewBag.Id_Vendita_FK = new SelectList(db.Vendite, "IdVendita", "Proprietario", dettagliVendita.Id_Vendita_FK);
            return View(dettagliVendita);
        }

        // GET: DettagliVendita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliVendita dettagliVendita = db.DettagliVendita.Find(id);
            if (dettagliVendita == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Prodotto_FK = new SelectList(db.Prodotti, "IdProdotto", "Tipo", dettagliVendita.Id_Prodotto_FK);
            ViewBag.Id_Vendita_FK = new SelectList(db.Vendite, "IdVendita", "Proprietario", dettagliVendita.Id_Vendita_FK);
            return View(dettagliVendita);
        }

        // POST: DettagliVendita/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDettagli,Id_Vendita_FK,Id_Prodotto_FK")] DettagliVendita dettagliVendita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dettagliVendita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Prodotto_FK = new SelectList(db.Prodotti, "IdProdotto", "Tipo", dettagliVendita.Id_Prodotto_FK);
            ViewBag.Id_Vendita_FK = new SelectList(db.Vendite, "IdVendita", "Proprietario", dettagliVendita.Id_Vendita_FK);
            return View(dettagliVendita);
        }

        // GET: DettagliVendita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettagliVendita dettagliVendita = db.DettagliVendita.Find(id);
            if (dettagliVendita == null)
            {
                return HttpNotFound();
            }
            return View(dettagliVendita);
        }

        // POST: DettagliVendita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DettagliVendita dettagliVendita = db.DettagliVendita.Find(id);
            db.DettagliVendita.Remove(dettagliVendita);
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
