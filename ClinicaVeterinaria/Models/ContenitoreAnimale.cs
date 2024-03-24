using System.Collections.Generic;

namespace ClinicaVeterinaria.Models
{
    public class ContenitoreAnimale
    {
        public Animale Animale { get; set; }
        public List<Ricovero> Ricoveri { get; set; }
        public List<Visita> Visita { get; set; }

    }
}