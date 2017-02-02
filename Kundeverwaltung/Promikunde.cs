using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kundeverwaltung
{
    public class Promikunde : Kunde
    {
        float rabatt;
        public Promikunde(String n, String l, String s, String t, Intervall i, float r) : base(n, l, s, t, i)
        {
            Rabatt = r;
        }

        public float Rabatt
        {
            get
            {
                return rabatt;
            }

            set
            {
                rabatt = value;
            }
        }

        public void addBestellung(Bestellung b)
        {
            if (b != null)
                base.Bestellungen.add(b);
        }

        public void delBestellung(Bestellung b)
        {
            if (b != null)
                base.Bestellungen.del(b);
        }
        public override double kostenBerechnung()
        {
            float stundenLohn = 7.5f;
            float gewinnSatz = 1.05f;
            double i = 0;
            stundenLohn = stundenLohn * base.Bestellungen.dauerKostenBerechnung();
            foreach (var item in base.Bestellungen.List)
            {
                if (item.Abgerechnet == false)
                {
                    i += item.Kosten;
                    item.Abgerechnet = true;
                }
            }
            i = ((stundenLohn + i) * gewinnSatz)*rabatt;
            base.KundenForderung = i;
            return i;
        }
        public override string ToString()
        {
            return base.ToString()+" Rabatt:"+Rabatt;
        }
    }
}
