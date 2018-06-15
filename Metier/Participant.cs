using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage_Thomas_Nicolas.Metier
{
    public class Participant : Personne
    {
        public bool Reduction
        {
            get
            {
                if (Age <= 12)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int IdReservation { get; set; }
    }
}
