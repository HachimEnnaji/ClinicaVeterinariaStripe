using ClinicaVeterinaria.Models;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class HomeController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CercaIlTuoAnimale()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> CercaAnimale(string Microchip)
        {
            var search = await db.Animale.Include(a => a.Ricovero).Include(a => a.Visita).FirstOrDefaultAsync(a => a.Microchip == Microchip && a.Propietario == "rifugio");
            if (search == null)
            {
                System.Diagnostics.Debug.WriteLine("Animale non trovato");
                return HttpNotFound();
            }
            return Json(search, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateCheckoutSession()
        {
            var ListaCarrello = Session["CarrelloSession"] as List<Prodotti>;

            var Options = new SessionCreateOptions
            {
                CustomerEmail = "EmailProva@gmail.it",
                Mode = "payment",
                SuccessUrl = "https://localhost:44343/Home/success",
                CancelUrl = "https://localhost:44343/Home/cancel",
                LineItems = new List<SessionLineItemOptions>()
            };
            foreach (var item in ListaCarrello)
            {
                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Prezzo * 100),
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Nome,
                            Description = item.Descrizione,
                        }
                    },
                    Quantity = 1
                };

                Options.LineItems.Add(sessionLineItem);
            }


            var service = new SessionService();
            var session = service.Create(Options);

            Response.Headers.Add("Location", session.Url);
            return new HttpStatusCodeResult(303);
        }

        [HttpPost]
        public ActionResult GetPayment()
        {
            var service = new PaymentIntentService();
            var options = new PaymentIntentCreateOptions
            {
                Amount = 1099,
                SetupFutureUsage = "off_session",
                Currency = "eur"
            };
            var paymentIntent = service.Create(options);
            return Json(paymentIntent);
        }

        public ActionResult success()
        {
            return View();
        }

        public ActionResult cancel()
        {
            return View();
        }
    }
}