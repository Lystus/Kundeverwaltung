using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kundeverwaltung
{
    public abstract class Kunde
    {
        String tel;
        String land;
        String name;
        String kundeCode;
        String strasse;
        Intervall haeufigkeitNewsletter;
        double kundenForderung;
        Bestellungen bestellungen;


        public Intervall HaeufigkeitNewsletter
        {
            get
            {
                return haeufigkeitNewsletter;
            }

            set
            {
                haeufigkeitNewsletter = value;
            }
        }
        public string Strasse
        {
            get
            {
                return strasse;
            }

            set
            {
                strasse = value;
            }
        }

        public string KundeCode
        {
            get
            {
                return kundeCode;
            }

            set
            {
                kundeCode = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != null)
                    name = value;
                else
                    throw new ArgumentNullException("Name darf nicht null sein");
            }
        }


        public string Land
        {
            get
            {
                return land;
            }

            set
            {
                land = value;
            }
        }

        public string Tel
        {
            get
            {
                return tel;
            }

            set
            {
                if (value != null)
                    tel = value;
                else
                    throw new ArgumentNullException("Telefon-nummer darf nicht null sein");
            }
        }

        public double KundenForderung
        {
            get
            {
                return kundenForderung;
            }

            set
            {
                kundenForderung = value;
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

        public Kunde(String n,String l, String s, String t, Intervall i)
        {
            Name = n;
            Land = l;
            Strasse = s;
            Tel = t;
            KundeCode = createKundeCode(n);
            HaeufigkeitNewsletter = i;
            Bestellungen = new Bestellungen();

        }
        public Kunde(String n, String l, String s, String t,String k,Intervall i)
        {
            Name = n;
            Land = l;
            Strasse = s;
            Tel = t;
            KundeCode = k;
            HaeufigkeitNewsletter = i;
            Bestellungen = new Bestellungen();
        }
        public String createKundeCode(String s)
        {
            String code = s.Replace(" ", string.Empty);
            Random r = new Random();
            if (code.Length >= 5)
                code = code.Substring(0, 5);
            code = code.ToUpper();
            code=r.Next(1000, 10000)+"-"+code;
            return code;
        }
        public abstract double kostenBerechnung();

        public override String ToString()
        {
            return "Name: "+name+" Kundecode: "+KundeCode+" Land: "+Land+" Straße: "+Strasse+" Tel: "+Tel+" Newsletter: "+HaeufigkeitNewsletter+" Forderung: "+KundenForderung+"$";
        }
        public override bool Equals(object obj)
        {
            Kunde k = (Kunde)obj;

            return name.Equals(k.name);
        }
    }

    public enum Intervall
    {
        nie, taeglich, woechentlich, monatlich
    }
}
