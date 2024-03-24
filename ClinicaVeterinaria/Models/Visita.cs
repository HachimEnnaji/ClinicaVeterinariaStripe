namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visita")]
    public partial class Visita
    {
        [Key]
        public int IdVisita { get; set; }

        [Column(TypeName = "date")]
        public DateTime DataVisita { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Esame { get; set; }

        public string DescrizioneCura { get; set; }

        public int IdAnimale_Fk { get; set; }

        public virtual Animale Animale { get; set; }
    }
}
