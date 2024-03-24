namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vendite")]
    public partial class Vendite
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vendite()
        {
            DettagliVendita = new HashSet<DettagliVendita>();
        }

        [Key]
        public int IdVendita { get; set; }

        [Required]
        [StringLength(200)]
        public string Proprietario { get; set; }

        [Required]
        [StringLength(16)]
        public string CodFiscale { get; set; }

        [StringLength(10)]
        public string NumRicetta { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataVendita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettagliVendita> DettagliVendita { get; set; }
    }
}
