namespace ClinicaVeterinaria.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Ricovero")]
    public partial class Ricovero
    {
        [Key]
        public int IdRicovero { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name = "Data Inizio Ricovero")]
        public DateTime DataInizio { get; set; }

        [Column(TypeName = "money")]
        public decimal Costo { get; set; }

        [Display(Name = "Ancora in rifugio?")]
        public bool IsAttivo { get; set; }

        [Display(Name = "Nome Animale")]
        public int id_Animale_FK { get; set; }

        public virtual Animale Animale { get; set; }
    }
}
