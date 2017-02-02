using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kundeverwaltung
{
    public class NormalKunde : Kunde
    {

        public NormalKunde(String n, String l, String s, String t,Intervall i) : base(n,l,s,t,i)
        {
            
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
            stundenLohn=stundenLohn*Bestellungen.dauerKostenBerechnung();
            foreach (var item in Bestellungen.List)
            {
                if (item.Abgerechnet ==false)
                {
                    i += item.Kosten;
                    item.Abgerechnet = true;
                }
            }
            i = (stundenLohn + i) * gewinnSatz;
            base.KundenForderung = i;
            return i;
        }

        public static explicit operator NormalKunde(Neukunde o)
        {
            NormalKunde output = new NormalKunde(o.Name,o.Land,o.Strasse,o.Tel,o.HaeufigkeitNewsletter);
            return output;
        }
    }

}
