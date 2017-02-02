using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kundeverwaltung
{
    class Bestellungen
    {
        List<Bestellung> list;

        public Bestellungen()
        {
            List = new List<Bestellung>();
        }

        public List<Bestellung> List
        {
            get
            {
                return list;
            }

            set
            {
                list = value;
            }
        }

        public void add(Bestellung b)
        {
            List.Add(b);
        }

        public void del(Bestellung b)
        {
            if (List.Contains(b))
                List.Remove(b);
        }

        public int getBestellungen()
        {
            return List.Count();
        }
        public double getKostenOfAllBestellung()
        {
            double i = 0;
            foreach(var item in List)
            {
                i += item.Kosten;
            }
            return i;
        }

        public int dauerKostenBerechnung()
        {
            int i = 0;
            TimeSpan t = new TimeSpan();
            foreach(var item in list)
            {
                if(item.Abgerechnet==false)
                    t.Add(item.Dauer);
            }
            if (t.Minutes >= 30)
                i++;
            i += t.Hours;
            return i;

        }
    }
}
