using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kundeverwaltung
{
    abstract class Kunde
    {
        String tel;
        String land;
        String ort;
        String name;
        String kundeCode;
        String strasse;
        Intervall haeufigkeitNewsletter;
        float kundenForderung;


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

        public string Ort
        {
            get
            {
                return ort;
            }

            set
            {
                ort = value;
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

        public float KundenForderung
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

        public Kunde(String n,String l, String o, String s, String t, Intervall i)
        {
            Name = n;
            Land = l;
            Ort = o;
            Strasse = s;
            Tel = t;
            KundeCode = createKundeCode(n);
            HaeufigkeitNewsletter = i;

        }
        public Kunde(String n, String l, String o, String s, String t,String k,Intervall i)
        {
            Name = n;
            Land = l;
            Ort = o;
            Strasse = s;
            Tel = t;
            KundeCode = k;
            HaeufigkeitNewsletter = i;
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
        public abstract float kostenBerechnung();

    }

    public enum Intervall
    {
        nie, taeglich, woechentlich, monatlich
    }
}
