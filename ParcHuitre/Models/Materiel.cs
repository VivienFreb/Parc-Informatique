using System;

namespace ParcHuitre.Models
{
    public class Materiel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Marque { get; set; }
        public int Reference { get; set; }
        public int idService { get; set; }
        public DateTime DateHA { get; set; }
        public int DureeGarantie { get; set; }
        public DateTime DateFinGarantie { get; set; }
        public int AChanger { get; set; }
    }
}