using ClinicaVeterinaria.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Admin, Veterinario")]
    public class RicoveroController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Ricovero
        public ActionResult Index()
        {
            var ricovero = db.Ricovero.Include(r => r.Animale);
            return View(ricovero.ToList());
        }

        // GET: Ricovero/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricovero ricovero = db.Ricovero.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            return View(ricovero);
        }

        // GET: Ricovero/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                //TO DO: migliorare la select list per renderla readonly
                ViewBag.id_Animale_FK = new SelectList(db.Animale.Where(a => a.IdAnimale == id), "IdAnimale", "Nome");
                TempData["NomeAnimale"] = db.Animale.Where(a => a.IdAnimale == id).Select(a => a.Nome).FirstOrDefault();
                TempData["idAnimale"] = id;
            }
            return View();
        }

        // POST: Ricovero/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdRicovero,DataInizio,Costo,IsAttivo,id_Animale_FK")] Ricovero ricovero)
        {
            var idAnimale = Convert.ToInt32(TempData["idAnimale"]);
            Debug.WriteLine("idAnimale: " + idAnimale);
            if (ModelState.IsValid)
            {
                var animale = db.Animale.FirstOrDefault(a => a.IdAnimale == idAnimale);
                if (animale == null)
                {
                    // L'animale con l'ID specificato non esiste.
                    // Puoi gestire questo caso come preferisci, ad esempio reindirizzando a una pagina di errore o restituendo un messaggio di errore.
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "L'animale specificato non esiste.");
                }

                var dataNascita = animale.DataNascita;
                if (dataNascita > ricovero.DataInizio)
                {
                    ricovero.DataInizio = dataNascita;
                    
                }
                ricovero.id_Animale_FK = idAnimale;
                db.Ricovero.Add(ricovero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_Animale_FK = new SelectList(db.Animale, "IdAnimale", "Nome", ricovero.id_Animale_FK);
            return View(ricovero);
        }

        // GET: Ricovero/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricovero ricovero = db.Ricovero.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_Animale_FK = new SelectList(db.Animale, "IdAnimale", "Nome", ricovero.id_Animale_FK);
            return View(ricovero);
        }

        // POST: Ricovero/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdRicovero,DataInizio,Costo,IsAttivo,id_Animale_FK")] Ricovero ricovero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ricovero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_Animale_FK = new SelectList(db.Animale, "IdAnimale", "Nome", ricovero.id_Animale_FK);
            return View(ricovero);
        }

        // GET: Ricovero/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ricovero ricovero = db.Ricovero.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            return View(ricovero);
        }

        // POST: Ricovero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ricovero ricovero = db.Ricovero.Find(id);
            db.Ricovero.Remove(ricovero);
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

        [HttpGet]
        public JsonResult RicoveroAttivo(int? month)
        {

            var ricovero = db.Ricovero.Include(r => r.Animale)
                          .Where(r => r.DataInizio.Month == month && r.IsAttivo == true)
                          .Select(r => r.Costo)
                          .DefaultIfEmpty(0)
                          .Sum();
            string mese = string.Empty;
            switch (month)
            {
                case 1:
                    mese = "Gennaio";
                    break;
                case 2:
                    mese = "Febbraio";
                    break;
                case 3:
                    mese = "Marzo";
                    break;
                case 4:
                    mese = "Aprile";
                    break;
                case 5:
                    mese = "Maggio";
                    break;
                case 6:
                    mese = "Giugno";
                    break;
                case 7:
                    mese = "Luglio";
                    break;
                case 8:
                    mese = "Agosto";
                    break;
                case 9:
                    mese = "Settembre";
                    break;
                case 10:
                    mese = "Ottobre";
                    break;
                case 11:
                    mese = "Novembre";
                    break;
                case 12:
                    mese = "Dicembre";
                    break;

            }
            if (ricovero == 0)
            {
                return Json($"Nessun pagamento per il mese di {mese} ", JsonRequestBehavior.AllowGet);
            }
            return Json(ricovero, JsonRequestBehavior.AllowGet);


        }
    }
}
