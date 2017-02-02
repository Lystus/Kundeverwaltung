using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Kundeverwaltung
{
    class Verwaltung
    {
        List<Kunde> kundeListe;

        public List<Kunde> KundeListe
        {
            get
            {
                return kundeListe;
            }

            set
            {
                kundeListe = value;
            }
        }

        public Verwaltung(XElement x)
        {
            KundeListe = new List<Kunde>();
            readStamm(x);
            readNormal(x);
            readNeu(x);
            readPromi(x);
            //fixForderung();
        }
        public void fixForderung()
        {
            foreach (var item in KundeListe)
            {
                double i = 0;
                foreach (var item2 in item.Bestellungen.List)
                {
                    if (!item2.Abgerechnet)
                    {
                        i += item2.Kosten;
                        item2.Abgerechnet = true;
                    }
                }
                item.KundenForderung = i;
            }
            save();
        }
        public void bezahlen(String s)
        {
            //5192-ROBIN
            var k=KundeListe.Where(x=>x.KundeCode.Equals(s)).ToList();
            var o = k.FirstOrDefault();
            if(o is Neukunde)
            {
                Neukunde neu = o as Neukunde;//Neukunde neu = (Neukunde)o;
                if (!neu.RabattBenutz)
                {
                    neu.RabattBenutz = true;
                    neu.KundenForderung = 0;
                }
                else
                {
                    NormalKunde n = new NormalKunde(o.Name, o.Land, o.Strasse, o.Tel, o.HaeufigkeitNewsletter);
                    n.Bestellungen = o.Bestellungen;
                    KundeListe.Add(n);
                }
                return;
            }
            k.FirstOrDefault().KundenForderung = 0;
        }
        public void checkNormalkunden()
        {
            foreach (var item in KundeListe)
            {
                if (item is NormalKunde)
                {
                    if (item.Bestellungen.List.Count > 10)
                    {
                        Stammkunde s = item as Stammkunde;
                        KundeListe.Add(s);
                        KundeListe.Remove(item);
                    }
                }
            }
        }
        public void save()
        {
            var erg = new XElement("Kundenliste",
                from s in KundeListe.Where(x => x is Stammkunde).OfType<Stammkunde>().ToArray()
                select new XElement("Stammkunde",
                    new XAttribute("Name", s.Name),
                    new XAttribute("Kunde-Code", s.KundeCode),
                    new XAttribute("Land", s.Land),
                    new XAttribute("Straße", s.Strasse),
                    new XAttribute("Tel-nr", s.Tel),
                    new XAttribute("Newsletter", s.HaeufigkeitNewsletter),
                    new XAttribute("Forderung", s.KundenForderung),
                    new XAttribute("Rabatt", s.Rabatt),
                    new XElement("Bestellungen",
                        from b in s.Bestellungen.List
                        select new XElement("Bestellung",
                            new XAttribute("Produkt-Name", b.Produkt),
                            new XAttribute("Bestellung-Nr", b.BestellungNr),
                            new XAttribute("Datum", b.Datum),
                            new XAttribute("Dauer", b.Dauer),
                            new XAttribute("Kosten", b.Kosten),
                            new XAttribute("Abgerechnet", b.Abgerechnet)
                        ))
                    ),
                from s in KundeListe.Where(x => x is NormalKunde).OfType<NormalKunde>().ToArray()
                select new XElement("Normalkunde", new XAttribute("Name", s.Name),
                    new XAttribute("Kunde-Code", s.KundeCode),
                    new XAttribute("Land", s.Land),
                    new XAttribute("Straße", s.Strasse),
                    new XAttribute("Tel-nr", s.Tel),
                    new XAttribute("Newsletter", s.HaeufigkeitNewsletter),
                    new XAttribute("Forderung", s.KundenForderung),
                    new XElement("Bestellungen",
                        from b in s.Bestellungen.List
                        select new XElement("Bestellung",
                            new XAttribute("Produkt-Name", b.Produkt),
                            new XAttribute("Bestellung-Nr", b.BestellungNr),
                            new XAttribute("Datum", b.Datum),
                            new XAttribute("Dauer", b.Dauer),
                            new XAttribute("Kosten", b.Kosten),
                            new XAttribute("Abgerechnet", b.Abgerechnet)
                            ))),

                from s in KundeListe.Where(x => x is Neukunde).OfType<Neukunde>().ToArray()
                select new XElement("Neukunde", new XAttribute("Name", s.Name),
                    new XAttribute("Kunde-Code", s.KundeCode),
                    new XAttribute("Land", s.Land),
                    new XAttribute("Straße", s.Strasse),
                    new XAttribute("Tel-nr", s.Tel),
                    new XAttribute("Newsletter", s.HaeufigkeitNewsletter),
                    new XAttribute("Forderung", s.KundenForderung),
                    new XAttribute("Rabatt", s.Rabatt),
                    new XAttribute("Rabattbenutzt", s.RabattBenutz),
                    new XElement("Bestellungen",
                        from b in s.Bestellungen.List
                        select new XElement("Bestellung",
                            new XAttribute("Produkt-Name", b.Produkt),
                            new XAttribute("Bestellung-Nr", b.BestellungNr),
                            new XAttribute("Datum", b.Datum),
                            new XAttribute("Dauer", b.Dauer),
                            new XAttribute("Kosten", b.Kosten),
                            new XAttribute("Abgerechnet", b.Abgerechnet)
                            ))),
                from s in KundeListe.Where(x => x is Promikunde).OfType<Promikunde>().ToArray()
                select new XElement("Promikunde", new XAttribute("Name", s.Name),
                    new XAttribute("Kunde-Code", s.KundeCode),
                    new XAttribute("Land", s.Land),
                    new XAttribute("Straße", s.Strasse),
                    new XAttribute("Tel-nr", s.Tel),
                    new XAttribute("Newsletter", s.HaeufigkeitNewsletter),
                    new XAttribute("Forderung", s.KundenForderung),
                    new XAttribute("Rabatt", s.Rabatt),
                    new XElement("Bestellungen",
                        from b in s.Bestellungen.List
                        select new XElement("Bestellung",
                            new XAttribute("Produkt-Name", b.Produkt),
                            new XAttribute("Bestellung-Nr", b.BestellungNr),
                            new XAttribute("Datum", b.Datum),
                            new XAttribute("Dauer", b.Dauer),
                            new XAttribute("Kosten", b.Kosten),
                            new XAttribute("Abgerechnet", b.Abgerechnet)
                            )))

                );
            erg.Save("../../kundenbestellungen.xml");
        }
        public void readPromi(XElement x)
        {
            var k = from s in x.Descendants("Promikunde")
                    select new
                    {
                        Name = s.Attribute("Name").Value,
                        Land = s.Attribute("Land").Value,
                        Straße = s.Attribute("Straße").Value,
                        Tel = s.Attribute("Tel-nr").Value,
                        Inter = (Intervall)Enum.Parse(typeof(Intervall), s.Attribute("Newsletter").Value),
                        Code = s.Attribute("Kunde-Code").Value,
                        Forderung = Double.Parse(s.Attribute("Forderung").Value),
                        Rabatt = float.Parse(s.Attribute("Rabatt").Value),
                        Best = from b in s.Descendants("Bestellung")
                               select new
                               {
                                   Name = b.Attribute("Produkt-Name").Value,
                                   Nr = Int32.Parse(b.Attribute("Bestellung-Nr").Value),
                                   Dat = Convert.ToDateTime(b.Attribute("Datum").Value),
                                   Dau = b.Attribute("Dauer").Value,
                                   Kosten = Int32.Parse(b.Attribute("Kosten").Value),
                                   Ab = Boolean.Parse(b.Attribute("Abgerechnet").Value)
                               }
                    };
            foreach (var item in k)
            {
                Promikunde s = new Promikunde(item.Name, item.Land, item.Straße, item.Tel, item.Inter,item.Rabatt);
                s.KundeCode = item.Code;
                s.KundenForderung = item.Forderung;
                Bestellungen best = new Bestellungen();
                foreach (var item2 in item.Best)
                {
                    string h = "";
                    string m = "";
                    string sec = "";
                    TimeSpan time = new TimeSpan(0);
                    if (item2.Dau.Substring(0, 2) == "PT")
                    {
                        String t = item2.Dau.Substring(2, item2.Dau.Length - 2);
                        h = t.Split('H')[0];
                        Console.WriteLine(h);
                        m = t.Split('H')[1].Split('M')[0];
                        sec = t.Split('H')[1].Split('M')[1].Split('S')[0];
                        time = new TimeSpan((h != "") ? Int32.Parse(h) : 0, (m != "") ? Int32.Parse(m) : 0, (sec != "") ? Int32.Parse(sec) : 0);
                    }
                    else
                    {
                        String t = item2.Dau.Substring(4, item2.Dau.Length - 4);
                        m = t.Split('M')[0];
                        sec = t.Split('M')[1].Split('S')[0];
                        time = new TimeSpan(0, (m != "") ? Int32.Parse(m) : 0, (sec != "") ? Int32.Parse(sec) : 0);
                    }
                    Bestellung b = new Bestellung(item2.Name, item2.Nr, item2.Dat, time, item2.Kosten);
                    b.Abgerechnet = item2.Ab;
                    best.add(b);
                }
                s.Bestellungen = best;
                KundeListe.Add(s);
            }
        }
        public void readNeu(XElement x)
        {
            var k = from s in x.Descendants("Neukunde")
                    select new
                    {
                        Name = s.Attribute("Name").Value,
                        Land = s.Attribute("Land").Value,
                        Straße = s.Attribute("Straße").Value,
                        Tel = s.Attribute("Tel-nr").Value,
                        Inter = (Intervall)Enum.Parse(typeof(Intervall), s.Attribute("Newsletter").Value),
                        Code = s.Attribute("Kunde-Code").Value,
                        Forderung = Double.Parse(s.Attribute("Forderung").Value),
                        Benutz = Boolean.Parse(s.Attribute("Rabattbenutzt").Value),
                        Best = from b in s.Descendants("Bestellung")
                               select new
                               {
                                   Name = b.Attribute("Produkt-Name").Value,
                                   Nr = Int32.Parse(b.Attribute("Bestellung-Nr").Value),
                                   Dat = Convert.ToDateTime(b.Attribute("Datum").Value),
                                   Dau = b.Attribute("Dauer").Value,
                                   Kosten = Int32.Parse(b.Attribute("Kosten").Value),
                                   Ab = Boolean.Parse(b.Attribute("Abgerechnet").Value)
                               }
                    };
            foreach (var item in k)
            {
                Neukunde s = new Neukunde(item.Name, item.Land, item.Straße, item.Tel, item.Inter);
                s.KundeCode = item.Code;
                s.KundenForderung = item.Forderung;
                Bestellungen best = new Bestellungen();
                foreach (var item2 in item.Best)
                {
                    string h = "";
                    string m = "";
                    string sec = "";
                    TimeSpan time = new TimeSpan(0);
                    if (item2.Dau.Substring(0, 2) == "PT")
                    {
                        String t = item2.Dau.Substring(2, item2.Dau.Length - 2);
                        h = t.Split('H')[0];
                        Console.WriteLine(h);
                        m = t.Split('H')[1].Split('M')[0];
                        sec = t.Split('H')[1].Split('M')[1].Split('S')[0];
                        time = new TimeSpan((h != "") ? Int32.Parse(h) : 0, (m != "") ? Int32.Parse(m) : 0, (sec != "") ? Int32.Parse(sec) : 0);
                    }
                    else
                    {
                        String t = item2.Dau.Substring(4, item2.Dau.Length - 4);
                        m = t.Split('M')[0];
                        sec = t.Split('M')[1].Split('S')[0];
                        time = new TimeSpan(0, (m != "") ? Int32.Parse(m) : 0, (sec != "") ? Int32.Parse(sec) : 0);
                    }
                    Bestellung b = new Bestellung(item2.Name, item2.Nr, item2.Dat, time, item2.Kosten);
                    b.Abgerechnet = item2.Ab;
                    best.add(b);
                }
                s.Bestellungen = best;
                s.RabattBenutz = item.Benutz;
                KundeListe.Add(s);
            }
        }
        public void readNormal(XElement x)
        {
            var k = from s in x.Descendants("Normalkunde")
                    select new
                    {
                        Name = s.Attribute("Name").Value,
                        Land = s.Attribute("Land").Value,
                        Straße = s.Attribute("Straße").Value,
                        Tel = s.Attribute("Tel-nr").Value,
                        Inter = (Intervall)Enum.Parse(typeof(Intervall), s.Attribute("Newsletter").Value),
                        Code = s.Attribute("Kunde-Code").Value,
                        Forderung = Double.Parse(s.Attribute("Forderung").Value),
                        Best = from b in s.Descendants("Bestellung")
                               select new
                               {
                                   Name = b.Attribute("Produkt-Name").Value,
                                   Nr = Int32.Parse(b.Attribute("Bestellung-Nr").Value),
                                   Dat = Convert.ToDateTime(b.Attribute("Datum").Value),
                                   Dau = b.Attribute("Dauer").Value,
                                   Kosten = Int32.Parse(b.Attribute("Kosten").Value),
                                   Ab = Boolean.Parse(b.Attribute("Abgerechnet").Value)
                               }
                    };
            foreach (var item in k)
            {
                NormalKunde s = new NormalKunde(item.Name, item.Land, item.Straße, item.Tel, item.Inter);
                s.KundeCode = item.Code;
                s.KundenForderung = item.Forderung;
                Bestellungen best = new Bestellungen();
                foreach (var item2 in item.Best)
                {
                    string h = "";
                    string m = "";
                    string sec = "";
                    TimeSpan time = new TimeSpan(0);
                    if (item2.Dau.Substring(0, 2) == "PT")
                    {
                        String t = item2.Dau.Substring(2, item2.Dau.Length - 2);
                        h = t.Split('H')[0];
                        Console.WriteLine(h);
                        m = t.Split('H')[1].Split('M')[0];
                        sec = t.Split('H')[1].Split('M')[1].Split('S')[0];
                        time = new TimeSpan((h != "") ? Int32.Parse(h) : 0, (m != "") ? Int32.Parse(m) : 0, (sec != "") ? Int32.Parse(sec) : 0);
                    }
                    else
                    {
                        String t = item2.Dau.Substring(4, item2.Dau.Length - 4);
                        m = t.Split('M')[0];
                        sec = t.Split('M')[1].Split('S')[0];
                        time = new TimeSpan(0, (m != "") ? Int32.Parse(m) : 0, (sec != "") ? Int32.Parse(sec) : 0);
                    }
                    Bestellung b = new Bestellung(item2.Name, item2.Nr, item2.Dat, time, item2.Kosten);
                    b.Abgerechnet = item2.Ab;
                    best.add(b);
                }
                s.Bestellungen = best;
                KundeListe.Add(s);
            }
        }
        public void readStamm(XElement x)
        {
            var k = from s in x.Descendants("Stammkunde")
                    select new
                    {
                        Name = s.Attribute("Name").Value,
                        Land = s.Attribute("Land").Value,
                        Straße = s.Attribute("Straße").Value,
                        Tel = s.Attribute("Tel-nr").Value,
                        Inter = (Intervall)Enum.Parse(typeof(Intervall), s.Attribute("Newsletter").Value),
                        Code = s.Attribute("Kunde-Code").Value,
                        Forderung = Double.Parse(s.Attribute("Forderung").Value),
                        Best = from b in s.Descendants("Bestellung")
                               select new
                               {
                                   Name = b.Attribute("Produkt-Name").Value,
                                   Nr = Int32.Parse(b.Attribute("Bestellung-Nr").Value),
                                   Dat = Convert.ToDateTime(b.Attribute("Datum").Value),
                                   Dau = b.Attribute("Dauer").Value,
                                   Kosten = Int32.Parse(b.Attribute("Kosten").Value),
                                   Ab = Boolean.Parse(b.Attribute("Abgerechnet").Value)
                               }
                    };
            foreach (var item in k)
            {
                Stammkunde s = new Stammkunde(item.Name, item.Land, item.Straße, item.Tel, item.Inter);
                s.KundeCode = item.Code;
                s.KundenForderung = item.Forderung;
                Bestellungen best = new Bestellungen();
                foreach (var item2 in item.Best)
                {
                    string h = "";
                    string m = "";
                    string sec = "";
                    TimeSpan time = new TimeSpan(0);
                    if (item2.Dau.Substring(0, 2) == "PT")
                    {
                        String t = item2.Dau.Substring(2, item2.Dau.Length - 2);
                        h = t.Split('H')[0];
                        Console.WriteLine(h);
                        m = t.Split('H')[1].Split('M')[0];
                        sec = t.Split('H')[1].Split('M')[1].Split('S')[0];
                        time = new TimeSpan((h != "") ? Int32.Parse(h) : 0, (m != "") ? Int32.Parse(m) : 0, (sec != "") ? Int32.Parse(sec) : 0);
                    }
                    else
                    {
                        String t = item2.Dau.Substring(4, item2.Dau.Length - 4);
                        m = t.Split('M')[0];
                        sec = t.Split('M')[1].Split('S')[0];
                        time = new TimeSpan(0, (m != "") ? Int32.Parse(m) : 0, (sec != "") ? Int32.Parse(sec) : 0);
                    }
                    Bestellung b = new Bestellung(item2.Name, item2.Nr, item2.Dat, time, item2.Kosten);
                    b.Abgerechnet = item2.Ab;
                    best.add(b);
                }
                s.Bestellungen = best;
                KundeListe.Add(s);
            }
        }
        public void ausgeben()
        {
            Console.WriteLine(KundeListe.Count);
            foreach (var item in KundeListe)
            {
                Console.WriteLine(item);
            }
        }
    }
}
