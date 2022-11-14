﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsGyumolcs {
    internal class Gyumolcs {
        int id;
        string nev;
        string egysegar;
        string mennyiseg;

        public int Id { get => id; set => id = value; }
        public string Nev { get => nev; set => nev = value; }
        public string Egysegar { get => egysegar; set => egysegar = value; }
        public string Mennyiseg { get => mennyiseg; set => mennyiseg = value; }

        public Gyumolcs(int id, string rendszam, string ev, string szin)
        {
            Id = id;
            Nev = nev;
            Egysegar = egysegar;
            Mennyiseg = mennyiseg;
        }

        public override string ToString()
        {
            return Nev;
        }
    }
}