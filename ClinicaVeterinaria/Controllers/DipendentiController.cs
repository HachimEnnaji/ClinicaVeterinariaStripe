using ClinicaVeterinaria.Models;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DipendentiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Dipendenti
        public ActionResult Index()
        {
            return View(db.Dipendenti.ToList());
        }

        // GET: Dipendenti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dipendenti dipendenti = db.Dipendenti.Find(id);
            if (dipendenti == null)
            {
                return HttpNotFound();
            }
            return View(dipendenti);
        }

        // GET: Dipendenti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dipendenti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDipendente,Ruolo,Nome,Cognome,Username,Psw")] Dipendenti dipendenti)
        {
            dipendenti.Psw = dipendenti.Nome + dipendenti.Cognome;
            dipendenti.Psw = Password.HashPassword(dipendenti.Psw);
            if (ModelState.IsValid)
            {

                db.Dipendenti.Add(dipendenti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dipendenti);
        }

        // GET: Dipendenti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dipendenti dipendenti = db.Dipendenti.Find(id);
            if (dipendenti == null)
            {
                return HttpNotFound();
            }
            return View(dipendenti);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]

        [HttpPost]
        public ActionResult Login(string username, string psw)
        {

            if (ModelState.IsValid)
            {

                var dip = db.Dipendenti.Where(d => d.Username == username).SingleOrDefault();

                if (dip != null)
                {
                    var checkPsw = Password.VerifyPassword(psw, dip.Psw);
                    //var psw = Password.VerifyPassword(password, dip.Psw);
                    if (checkPsw)
                    {
                        Session["IdDipendente"] = dip.IdDipendente;
                        Session["Ruolo"] = dip.Ruolo;

                        FormsAuthentication.SetAuthCookie(username, false);
                        return RedirectToAction("Index", "Home");
                    }


                }
                else
                {
                    TempData["error"] = "Username o password errati";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            TempData["info"] = "Logout effettuato";
            return RedirectToAction("Index", "Home");
        }



        // GET: Dipendenti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dipendenti dipendenti = db.Dipendenti.Find(id);
            if (dipendenti == null)
            {
                return HttpNotFound();
            }
            return View(dipendenti);
        }

        // POST: Dipendenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dipendenti dipendenti = db.Dipendenti.Find(id);
            db.Dipendenti.Remove(dipendenti);
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
