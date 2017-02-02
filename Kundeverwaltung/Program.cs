using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Kundeverwaltung
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataCreationClass d = new DataCreationClass();
            //Abfrage3();
            //erg.Save("../../kundenbestellungen.xml");
            var xml = XElement.Load("../../kundenbestellungen.xml");
            Verwaltung vw = new Verwaltung(xml);
            //vw.ausgeben();
            Console.ReadLine();
        }
        public static void Abfrage1()
        {
            var xml = XElement.Load("../../kundenbestellungen.xml");

            var erg =
                from x in xml.Descendants("Stammkunde")
                    //orderby x.Attribute("Name")
                select new { Name = x.Attribute("Name").Value };

            foreach (var item in erg.OrderBy(y => y.Name))
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void Abfrage1_1()
        {
            var xml = XElement.Load("../../kundenbestellungen.xml");

            var erg =
                from x in xml.Descendants("Normalkunde")
                    //orderby x.Attribute("Name")
                select new { Name = x.Attribute("Name").Value };

            foreach (var item in erg.OrderBy(y => y.Name))
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void Abfrage1_2()
        {
            var xml = XElement.Load("../../kundenbestellungen.xml");

            var erg =
                from x in xml.Descendants("Neukunde")
                    //orderby x.Attribute("Name")
                select new { Name = x.Attribute("Name").Value };

            foreach (var item in erg.OrderBy(y => y.Name))
            {
                Console.WriteLine(item.Name);
            }
        }
        public static void Abfrage1_3()
        {
            var xml = XElement.Load("../../kundenbestellungen.xml");

            var erg =
                from x in xml.Descendants("Promikunde")
                    //orderby x.Attribute("Name")
                select new { Name = x.Attribute("Name").Value };

            foreach (var item in erg.OrderBy(y => y.Name))
            {
                Console.WriteLine(item.Name);
            }
        }

        public static void Abfrage2()
        {
            var xml = XElement.Load("../../kundenbestellungen.xml");

            var erg =
                from x in xml.Descendants("Stammkunde")
                    //orderby x.Attribute("Name")
                select new
                {
                    Name = x.Attribute("Name").Value,
                    anz = x.Descendants("Bestellung").Count()
                };

            foreach (var item in erg.OrderBy(y => y.anz))
            {
                Console.WriteLine("Name:" + item.Name + " Anzahl:" + item.anz);
            }
        }


        public static void Abfrage4()
        {
            var xml = XElement.Load("../../kundenbestellungen.xml");

            var erg =
            new XElement("Kundenliste",
            from x in xml.Descendants("Stammkunde")
            select new XElement("Stammkunde",
                    new XElement("Kunde-Code", x.Attribute("Kunde-Code")),
                    new XElement("Name", x.Attribute("Name")),
                    new XElement("Land", x.Attribute("Land"))));
            Console.WriteLine(erg);
        }

        public static void Abfrage5()
        {
            var xml = XElement.Load("../../kundenbestellungen.xml");

            var erg =
                    from x in xml.Descendants("Stammkunde")
                    group x by x.Attribute("Name").Value into res
                    orderby res.Key
                    select new
                    {
                        Name = res.Key,
                        bestellung = res
                    };
            foreach (var item in erg)
            {
                Console.WriteLine(item.Name);
                foreach (var i in item.bestellung)
                {
                    Console.WriteLine("\n\t" + i.Element("Bestellungen").Element("Bestellung").Attribute("Produkt-Name").Value);
                }
            }
        }
        public static void Abfrage3()
        {
            var xml = XElement.Load("../../kundenbestellungen.xml");
            foreach (var item in xml.Elements())
            {
                bool b = false;
                foreach (var item2 in item.Descendants("Bestellung"))
                {
                    if (item2.Attribute("Produkt-Name").Value == "Nextcore Noon VR")
                        b = true;
                }
                if (b)
                    Console.WriteLine(item);
            }
        }

    }
}
