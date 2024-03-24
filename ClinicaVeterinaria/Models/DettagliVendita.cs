namespace ClinicaVeterinaria.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettagliVendita")]
    public partial class DettagliVendita
    {
        [Key]
        public int IdDettagli { get; set; }

        public int Id_Vendita_FK { get; set; }

        public int Id_Prodotto_FK { get; set; }

        public virtual Prodotti Prodotti { get; set; }

        public virtual Vendite Vendite { get; set; }
    }
}
