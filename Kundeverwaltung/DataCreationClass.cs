using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Kundeverwaltung
{
    class DataCreationClass
    {
        Random r = new Random();
        string[] namen = new string[] { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King", "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter", "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King", "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter", "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Sophia", "Emma", "Isabella", "Olivia", "Ava", "Lily", "Chloe", "Madison", "Emily", "Abigail", "Addison", "Mia", "Madelyn", "Ella", "Hailey", "Kaylee", "Avery", "Kaitlyn", "Riley", "Aubrey", "Brooklyn", "Peyton", "Layla", "Hannah", "Charlotte", "Bella", "Natalie", "Sarah", "Grace", "Amelia", "Kylie", "Arianna", "Anna", "Elizabeth", "Sophie", "Claire", "Lila", "Lauryn", "Lystus" };
        string[] laender = new string[] { "USA", "England", "Japan", "France", "Russia", "China", "Korea", "Spain", "Canada", "Vietnam", "Italy" };
        string[] produkten = new string[] { "Samsung S7 Edge", "Intel Core i7", "Apple Airpods", "Logitech Gaming Headset", "Logitech Gaming Maus", "Marshall Major 2 Black", "Abendmahlzeit mit Champagner", "Steam Link", "Steam Controler", "Beats by Dr Dre Stereo Anlage", "Gatebox", "HP Notebook", "Acer Notebook", "Apple Macbook", "Kindle Paperwhite", "Samsung Galaxy Tab A", "Apple iPad", "Cooler Master Storm Octane", "Microsoft XBox One", "Playstation 4", "Nintendo Wii", "Nintendo New 3DS", "Nintendo Switch", "Razer Tastatur Blackwidow X Chroma", "Apple iPhone 5s", "Samsung Galaxy J3", "JBL Bluetooth Charge 2+", "Bose Bluetooth Mini 2", "Samsung Gear VR", "Nextcore Noon VR", "Fifa 17", "Sony PS4 Controler", "GTA 5 PS4", "Sony Playstation VR", "Bioshock", "Skyrim V Special Edition", "Battlefield 1", "Watch Dogs 2", "Grand Kingdom PS Vita", "Psychopass PS Vita", "Persona 5 PS Vita", "DOOM PS4", "Dishonored 2", "Darksouls 3", "Dragonball Xenoverse 2", "Witcher 3 Wild Hunt", "Eristoff Black" };

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

        public DataCreationClass()
        {
            KundeListe = new List<Kunde>();
            //init();
            int o = 0;
            while (o <= 100)
            {
                makeThings();
                o++;
            }
            //Console.WriteLine(KundeListe.Count);
            fixForderung();
            save();
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
        }
        public void makeThings()
        {
            int i = r.Next(4);
            switch (i)
            {
                case 0: makeStamm(); break;
                case 1: makePromi(); break;
                case 2: makeNormal(); break;
                case 3: makeNeu(); break;
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
        public void makeStamm()
        {
            Stammkunde n = new Stammkunde(giveName(), giveLand(), giveStrasse(), giveTel(), (Intervall)giveNewsletter());
            int i = 0;
            while (i <= r.Next(21))
            {
                n.addBestellung(makeBestellung());
                i++;
            }
            if(!KundeListe.Contains(n))
                KundeListe.Add(n);
        }
        public void makePromi()
        {
            Promikunde n = new Promikunde(giveName(), giveLand(), giveStrasse(), giveTel(), (Intervall)giveNewsletter(), r.Next(1, 51) / 100);
            int i = 0;

            while (i <= r.Next(21))
            {
                n.addBestellung(makeBestellung());
                i++;
            }
            if (!KundeListe.Contains(n))
                KundeListe.Add(n);
        }
        public void makeNormal()
        {
            NormalKunde n = new NormalKunde(giveName(), giveLand(), giveStrasse(), giveTel(), (Intervall)giveNewsletter());
            int i = 0;
            while (i <= r.Next(21))
            {
                n.addBestellung(makeBestellung());
                i++;
            }
            if (!KundeListe.Contains(n))
                KundeListe.Add(n);
        }
        public void makeNeu()
        {
            Neukunde n = new Neukunde(giveName(), giveLand(), giveStrasse(), giveTel(), (Intervall)giveNewsletter());
            int i = 0;

            while (i <= r.Next(21))
            {
                n.addBestellung(makeBestellung());
                i++;
            }
            if (!KundeListe.Contains(n))
                KundeListe.Add(n);
        }

        public String giveName()
        {
            string s = namen[r.Next(namen.Length)] + " " + namen[r.Next(namen.Length)];
            return s;
        }
        public String giveLand()
        {
            string s = laender[r.Next(laender.Length)];
            return s;
        }
        public String giveStrasse()
        {
            string q = "";
            int i = r.Next(4);
            switch (i)
            {
                case 0: q = "Street"; break;
                case 1: q = "Road"; break;
                case 2: q = "Place"; break;
                case 3: q = "Alley"; break;
            }
            string s = namen[r.Next(namen.Length)] + "'s " + q;
            return s;
        }
        public String giveTel()
        {
            return "+" + r.Next(11, 100) + " " + r.Next(1000, 10000) + " " + r.Next(100000, 1000000);
        }
        public int giveNewsletter()
        {
            return r.Next(4);
        }

        public Bestellung makeBestellung()
        {
            string s1 = produkten[r.Next(produkten.Length)];
            int i = r.Next(100000, 1000000);
            DateTime d = DateTime.Now.AddDays(-1 * r.Next(366));
            TimeSpan t = new TimeSpan(r.Next(1, 25), r.Next(1,61)+1, r.Next(1,61)+1);
            float k = r.Next(100, 10000) + (100 / r.Next(1, 100));
            Bestellung b = new Bestellung(s1, i, d, t, k);
            return b;
        }

        /**
         * 
         * Thats how I created a lot of data............. a lot
         * 
         * 
         * 
         * 
         * 
         * */


    }
}
