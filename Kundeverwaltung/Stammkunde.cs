using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kundeverwaltung
{
    class Stammkunde : Kunde
    {
        float rabatt = 0.95f;
        Bestellungen bestellungen;

        public Bestellungen Bestellungen
        {
            get
            {
                return bestellungen;
            }

            set
            {
                bestellungen = value;
            }
        }
        public void addBestellung(Bestellung b)
        {
            bestellungen.add(b);
        }

        public void delBestellung(Bestellung b)
        {
            bestellungen.del(b);
        }
        public Stammkunde(String n, String l, String o, String s, String t, Intervall i) : base(n, l, o, s, t, i)
        {
            Bestellungen = new Bestellungen();
        }

        public override float kostenBerechnung()
        {
            float stundenLohn = 7.5f;
            float gewinnSatz = 1.05f;
            float i = 0;
            stundenLohn = stundenLohn * Bestellungen.dauerKostenBerechnung();
            foreach (var item in Bestellungen.List)
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
    }
}
