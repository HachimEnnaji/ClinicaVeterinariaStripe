namespace ClinicaVeterinaria.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Dipendenti")]
    public partial class Dipendenti
    {
        [Key]
        public int IdDipendente { get; set; }

        [Required]
        [StringLength(50)]
        public string Ruolo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(100)]
        public string Psw { get; set; }
    }
}
