using ClinicaVeterinaria.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Admin, Farmacista")]
    public class ProdottiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Prodotti
        public ActionResult Index()
        {
            var prodotti = db.Prodotti.Include(p => p.Armadietti).Include(p => p.Cassetti).Include(p => p.Fornitori);
            return View(prodotti.ToList());
        }

        // GET: Prodotti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // GET: Prodotti/Create
        public ActionResult Create()
        {
            ViewBag.Id_Armadietto_FK = new SelectList(db.Armadietti, "IdArmadietto", "IdArmadietto");
            ViewBag.Id_Cassetto_FK = new SelectList(db.Cassetti, "IdCassetto", "IdCassetto");
            ViewBag.IdFornitore_FK = new SelectList(db.Fornitori, "IdFornitori", "Nome");
            return View();
        }

        // POST: Prodotti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProdotto,Tipo,Nome,IdFornitore_FK,Descrizione,Quantita,Prezzo,Id_Armadietto_FK,Id_Cassetto_FK")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Prodotti.Add(prodotti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Armadietto_FK = new SelectList(db.Armadietti, "IdArmadietto", "IdArmadietto", prodotti.Id_Armadietto_FK);
            ViewBag.Id_Cassetto_FK = new SelectList(db.Cassetti, "IdCassetto", "IdCassetto", prodotti.Id_Cassetto_FK);
            ViewBag.IdFornitore_FK = new SelectList(db.Fornitori, "IdFornitori", "Recapito", prodotti.IdFornitore_FK);
            return View(prodotti);
        }

        // GET: Prodotti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Armadietto_FK = new SelectList(db.Armadietti, "IdArmadietto", "IdArmadietto", prodotti.Id_Armadietto_FK);
            ViewBag.Id_Cassetto_FK = new SelectList(db.Cassetti, "IdCassetto", "IdCassetto", prodotti.Id_Cassetto_FK);
            ViewBag.IdFornitore_FK = new SelectList(db.Fornitori, "IdFornitori", "Recapito", prodotti.IdFornitore_FK);
            return View(prodotti);
        }

        // POST: Prodotti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProdotto,Tipo,Nome,IdFornitore_FK,Descrizione,Quantita,Prezzo,Id_Armadietto_FK,Id_Cassetto_FK")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodotti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Armadietto_FK = new SelectList(db.Armadietti, "IdArmadietto", "IdArmadietto", prodotti.Id_Armadietto_FK);
            ViewBag.Id_Cassetto_FK = new SelectList(db.Cassetti, "IdCassetto", "IdCassetto", prodotti.Id_Cassetto_FK);
            ViewBag.IdFornitore_FK = new SelectList(db.Fornitori, "IdFornitori", "Recapito", prodotti.IdFornitore_FK);
            return View(prodotti);
        }

        // GET: Prodotti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
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

        public ActionResult AggiungiAlCarrello(int id)
        {
            var prodotto = db.Prodotti.Find(id);
            if (prodotto == null)
            {
                return RedirectToAction("Index");
            }
            var ListaCarrello = Session["CarrelloSession"] as List<Prodotti> ?? new List<Prodotti>();
            ListaCarrello.Add(prodotto);
            Session["CarrelloSession"] = ListaCarrello;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> CercaProdotto(string Nome)
        {
            var search = await db.Prodotti.Where(p => p.Nome == Nome).FirstOrDefaultAsync();
            return Json(search, JsonRequestBehavior.AllowGet);

        }
    }
}
