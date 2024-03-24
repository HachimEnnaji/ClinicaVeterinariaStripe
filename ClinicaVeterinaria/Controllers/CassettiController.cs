using ClinicaVeterinaria.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CassettiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Cassetti
        public ActionResult Index()
        {
            var cassetti = db.Cassetti.Include(c => c.Armadietti);
            return View(cassetti.ToList());
        }

        // GET: Cassetti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cassetti cassetti = db.Cassetti.Find(id);
            if (cassetti == null)
            {
                return HttpNotFound();
            }
            return View(cassetti);
        }

        // GET: Cassetti/Create
        public ActionResult Create()
        {
            ViewBag.Id_Armadietto_FK = new SelectList(db.Armadietti, "IdArmadietto", "IdArmadietto");
            return View();
        }

        // POST: Cassetti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCassetto,Id_Armadietto_FK,NumeroCassetto")] Cassetti cassetti)
        {
            if (ModelState.IsValid)
            {
                db.Cassetti.Add(cassetti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Armadietto_FK = new SelectList(db.Armadietti, "IdArmadietto", "IdArmadietto", cassetti.Id_Armadietto_FK);
            return View(cassetti);
        }

        // GET: Cassetti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cassetti cassetti = db.Cassetti.Find(id);
            if (cassetti == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Armadietto_FK = new SelectList(db.Armadietti, "IdArmadietto", "IdArmadietto", cassetti.Id_Armadietto_FK);
            return View(cassetti);
        }

        // POST: Cassetti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCassetto,Id_Armadietto_FK,NumeroCassetto")] Cassetti cassetti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cassetti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Armadietto_FK = new SelectList(db.Armadietti, "IdArmadietto", "IdArmadietto", cassetti.Id_Armadietto_FK);
            return View(cassetti);
        }

        // GET: Cassetti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cassetti cassetti = db.Cassetti.Find(id);
            if (cassetti == null)
            {
                return HttpNotFound();
            }
            return View(cassetti);
        }

        // POST: Cassetti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cassetti cassetti = db.Cassetti.Find(id);
            db.Cassetti.Remove(cassetti);
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
