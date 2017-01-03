using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kundeverwaltung
{
    class Bestellung
    {
        String produkt;
        int bestellungNr;
        DateTime datum;
        TimeSpan dauer;
        float kosten;
        Boolean abgerechnet;

        public Bestellung(String p,int b, DateTime dt, TimeSpan d,float k)
        {
            Produkt = p;
            BestellungNr = b;
            Datum = dt;
            Dauer = d;
            Kosten = k;
            Abgerechnet = false;
            
        }
        public string Produkt
        {
            get
            {
                return produkt;
            }

            set
            {
                produkt = value;
            }
        }

        public int BestellungNr
        {
            get
            {
                return bestellungNr;
            }

            set
            {
                bestellungNr = value;
            }
        }

        public DateTime Datum
        {
            get
            {
                return datum;
            }

            set
            {
                datum = value;
            }
        }

        public TimeSpan Dauer
        {
            get
            {
                return dauer;
            }

            set
            {
                dauer = value;
            }
        }

        public float Kosten
        {
            get
            {
                return kosten;
            }

            set
            {
                kosten = value;
            }
        }

        public Boolean Abgerechnet
        {
            get
            {
                return abgerechnet;
            }

            set
            {
                abgerechnet = value;
            }
        }
    }

}
