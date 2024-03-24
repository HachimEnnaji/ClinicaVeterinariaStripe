namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Animale")]
    public partial class Animale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animale()
        {
            Ricovero = new HashSet<Ricovero>();
            Visita = new HashSet<Visita>();
        }

        [Key]
        public int IdAnimale { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data Registrazione")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataRegistrazione { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Il nome e' obbligatorio")]
        [StringLength(50, ErrorMessage = "Il nome non deve essere piu' lungo di 50 caratteri")]

        public string Nome { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Il tipo non deve essere piu' lungo di 50 caratteri")]
        //non ho usato l'enum perche' non so come fare a farlo funzionare
        public string Tipo { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Il colore non deve essere piu' lungo di 50 caratteri")]
        public string Colore { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DataNascita { get; set; }

        [StringLength(15, MinimumLength = 15, ErrorMessage = "Il microchip deve contenere 15 caratteri")]
        public string Microchip { get; set; }

        [StringLength(200, ErrorMessage = "Il nome del proprietario non deve essere piu' lungo di 200 caratteri")]
        [Display(Name = "Proprietario")]
        public string Propietario { get; set; } = "Rifugio";

        [StringLength(50)]
        public string Foto { get; set; } = "AnimaleDefault.jpg";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ricovero> Ricovero { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visita> Visita { get; set; }
    }
}
