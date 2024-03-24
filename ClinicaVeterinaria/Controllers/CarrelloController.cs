using ClinicaVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    [Authorize(Roles = "Admin, Farmacista")]
    public class CarrelloController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        // GET: Carrello
        public ActionResult Index()
        {
            var ListaCarrello = Session["CarrelloSession"] as List<Prodotti>;
            if (ListaCarrello == null || !ListaCarrello.Any())
            {
                return RedirectToAction("Index", "Prodotti");
            }
            return View(ListaCarrello);
        }

        public ActionResult Rimuovi(int id)
        {
            var ListaCarrello = Session["CarrelloSession"] as List<Prodotti>;
            if (ListaCarrello != null)
            {
                var prodotto = ListaCarrello.FirstOrDefault(l => l.IdProdotto == id);
                ListaCarrello.Remove(prodotto);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Acquista(string Proprietario, string CodFiscale, string NumRicetta)
        {
            var ListaCarrello = Session["CarrelloSession"] as List<Prodotti>;
            if (ListaCarrello != null)
            {
                Vendite vendita = new Vendite();
                vendita.Proprietario = Proprietario;
                vendita.CodFiscale = CodFiscale;
                vendita.NumRicetta = NumRicetta;
                vendita.DataVendita = DateTime.Now;
                db.Vendite.Add(vendita);
                db.SaveChanges();

                foreach (var item in ListaCarrello)
                {
                    DettagliVendita DV = new DettagliVendita();
                    DV.Id_Vendita_FK = vendita.IdVendita;
                    DV.Id_Prodotto_FK = item.IdProdotto;
                    db.DettagliVendita.Add(DV);
                    db.SaveChanges();
                }
                ListaCarrello.Clear();
            }
            return RedirectToAction("Index", "Prodotti");
        }
    }
}