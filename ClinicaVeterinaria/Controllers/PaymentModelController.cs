using Stripe;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ClinicaVeterinaria.Controllers
{
    public class PaymentModelController : Controller
    {


        [HttpPost]
        public async Task<ActionResult> ProcessPayment(PaymentIntentRequest request, string Proprietario, string CodFiscale, string NumRicetta)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = "usd",
                PaymentMethod = request.PaymentMethodId,
                Confirm = true, // Automatically confirm the intent
            });

            if (paymentIntent.Status == "succeeded")
            {
                // The payment was successful
                TempData["payment"] = "Payment succeeded!";
            }
            else
            {
                // Handle different payment statuses and errors
                TempData["payment"] = "Payment failed!";
            }
            return RedirectToAction("Index", "Prodotti");
        }

        public class PaymentIntentRequest
        {
            public string PaymentMethodId { get; set; }
            public long Amount { get; set; }
        }
    }
}