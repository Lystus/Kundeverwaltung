using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kundeverwaltung
{
    class Promikunde : Kunde
    {
        float rabatt;
        Bestellungen bestellungen;
        public Promikunde(String n, String l, String o, String s, String t, Intervall i, float r) : base(n, l, o, s, t, i)
        {
            Rabatt = r;
            Bestellungen = new Bestellungen();
        }

        public float Rabatt
        {
            get
            {
                return rabatt;
            }

            set
            {
                if (value <= 50 && value > 0)
                    rabatt = value;
                else
                    throw new ArgumentOutOfRangeException("Rabatt muss größer als 0 und kleiner als 50 Prozent sein");
            }
        }

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
        public override float kostenBerechnung()
        {
            float stundenLohn = 7.5f;
            float gewinnSatz = 1.05f;
            float i = 0;
            stundenLohn = stundenLohn * bestellungen.dauerKostenBerechnung();
            foreach (var item in bestellungen.List)
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
