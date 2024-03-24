namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            DettagliVendita = new HashSet<DettagliVendita>();
        }

        [Key]
        public int IdProdotto { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        public int IdFornitore_FK { get; set; }

        [Required]
        public string Descrizione { get; set; }

        public int Quantita { get; set; }

        public decimal Prezzo { get; set; }
        public int Id_Armadietto_FK { get; set; }

        public int Id_Cassetto_FK { get; set; }

        public virtual Armadietti Armadietti { get; set; }

        public virtual Cassetti Cassetti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettagliVendita> DettagliVendita { get; set; }

        public virtual Fornitori Fornitori { get; set; }
    }
}
