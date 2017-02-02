using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kundeverwaltung
{
    public class Neukunde : Kunde
    {
        float rabatt = 0.9f;
        Boolean rabattBenutz;

        public bool RabattBenutz
        {
            get
            {
                return rabattBenutz;
            }

            set
            {
                rabattBenutz = value;
            }
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
        public Neukunde(String n, String l, String s, String t, Intervall i) : base(n, l, s, t, i)
        {
            RabattBenutz = false;
        }
        public override double kostenBerechnung()
        {
            float stundenLohn = 7.5f;
            float gewinnSatz = 1.05f;
            double i = 0;
            stundenLohn = stundenLohn * Bestellungen.dauerKostenBerechnung();
            foreach (var item in Bestellungen.List)
            {
                if (item.Abgerechnet == false)
                {
                    i += item.Kosten;
                    item.Abgerechnet = true;
                }
            }
            i = (stundenLohn + i) * gewinnSatz;
            if (RabattBenutz == false)
            {
                i = i * Rabatt;
                RabattBenutz = true;
            }
            base.KundenForderung = i;
            return i;
        }

        public override string ToString()
        {
            return base.ToString()+" Rabattbenutz:"+RabattBenutz;
        }
    }
}
