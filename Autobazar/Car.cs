using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobazar
{
    class Car
    {
        public int ID { get; set; }
        public int RokVyroby { get; set; }
        public int PocetKm { get; set; }
        public string Znacka { get; set; }
        public string Typ { get; set; }
        public TypPaliva Palivo { get; set; }
        public decimal CenaAuta { get; set; }
        public string Mesto { get; set; }
        public int PocetDveri { get; set; }
        public bool JeHavarovane { get; set; }

        public Car()
        {

        }

        public Car(int iD, int rokVyroby, int pocetKm, string znacka, string typ, TypPaliva palivo, decimal cenaAuta, string mesto, int pocetDveri, bool jeHavarovane)
        {
            ID = iD;
            RokVyroby = rokVyroby;
            PocetKm = pocetKm;
            Znacka = znacka;
            Typ = typ;
            Palivo = palivo;
            CenaAuta = cenaAuta;
            Mesto = mesto;
            PocetDveri = pocetDveri;
            JeHavarovane = jeHavarovane;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ID);
            sb.Append("\t");
            sb.Append(RokVyroby);
            sb.Append("\t");
            sb.Append(PocetKm);
            sb.Append("\t");
            sb.Append(Znacka);
            sb.Append("\t");
            sb.Append(Typ);
            sb.Append("\t");
            sb.Append(Enum.GetName(typeof(TypPaliva),Palivo));
            sb.Append("\t");
            sb.Append(CenaAuta);
            sb.Append("\t");
            sb.Append(Mesto);
            sb.Append("\t");
            sb.Append(PocetDveri);
            sb.Append("\t");
            sb.Append(JeHavarovane);
            sb.Append("\n");

            return sb.ToString();
        }
                                            
        
    }
    public enum TypPaliva
    {
        diesel,
        benzin,
        plyn,
    }
}
