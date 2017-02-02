namespace Kundeverwaltung
{
    interface IKunde
    {
        Intervall HaeufigkeitNewsletter { get; set; }
        string KundeCode { get; set; }
        double KundenForderung { get; set; }
        string Land { get; set; }
        string Name { get; set; }
        string Strasse { get; set; }
        string Tel { get; set; }

        string createKundeCode(string s);
        bool Equals(object obj);
        double kostenBerechnung();
        string ToString();
    }
}