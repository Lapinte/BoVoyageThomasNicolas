﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyage_Thomas_Nicolas.Metier
{
    public class Client : Personne
    {
        public string Email { get; set; }

        public DossierReservation DossierReservation { get; set; } 
    }
}