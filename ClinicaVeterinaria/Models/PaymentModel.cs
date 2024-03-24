namespace ClinicaVeterinaria.Models
{
    public class PaymentModel
    {

        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Cvc { get; set; }
        public long Amount { get; set; } // Amount in cents

    }
}