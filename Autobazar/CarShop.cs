using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Autobazar
{
    static class CarShop
    {

        public static void MainMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"||||***********AUTOBAZAR*************||||");
            sb.AppendLine($"-----------------------------------------");
            sb.AppendLine($"P => Pridaj nové auto.");
            sb.AppendLine($"Z => Zmeň údaje o aute.");
            sb.AppendLine($"O => Odstráň auto.");
            sb.AppendLine($"V => Vypíš zoznam áut bazáru.");
            sb.AppendLine($"F => Filtruj dáta bazáru.");
            sb.AppendLine($"N => Načítaj zoznam áut bazáru zo súboru.");
            sb.AppendLine($"U => Ulož zoznam áut bazáru do súboru.");
            sb.AppendLine($"-----------------------------------------");
            sb.AppendLine($"*****************************************");
            sb.AppendLine($"!!!!!!!!!!Q => Ukonči program!!!!!!!!!!!!");
            sb.AppendLine($"*****************************************");
            Console.WriteLine(sb.ToString());
        }

        public static Car AddCar(int idAuta)
        {           
            string znackaAuta = OverStringVstup($"Značka: ");
            string typAuta = OverStringVstup($"Typ: ");
            int rok = OverIntVstup($"Rok výroby: ");
            int pocetKm = OverIntVstup($"Počet najazdených KM: ");            
            TypPaliva palivoTyp = ChooseFuel();
            decimal cena = OverDecimalVstup($"\nCena auta: ");            
            string mesto = OverStringVstup($"Zadaj mesto: ");            
            int pocetDveri = OverIntVstup($"Počet dverí: ");                     
            bool havarovane = IsDamage();
            Car newCar = new Car(idAuta, rok, pocetKm, znackaAuta, typAuta, palivoTyp, cena, mesto, pocetDveri, havarovane);            
            return newCar;
        }

        public static string OverStringVstup(string popis)
        {
            string text;
            do
            {
                Console.Write(popis);
                text = Console.ReadLine();
            } while (text == "");
            text = text.ToLower();
            return text;
        }

        public static int OverIntVstup(string popis)
        {
            int number;
            bool success = false;
            do
            {
                Console.Write(popis);
                success = int.TryParse(Console.ReadLine(), out number);
            } while (!success);
            return number;
        }

        public static decimal OverDecimalVstup(string popis)
        {
            decimal number;
            bool success = false;
            do
            {
                Console.Write(popis);
                success = decimal.TryParse(Console.ReadLine(), out number);
            } while (!success);
            return number;
        }

        public static string RepairDataMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"-----Ktoré údaje chcete zmeniť na aute?-----");
            sb.AppendLine($"--------------------------------------------");
            sb.AppendLine($"R => Rok výroby");
            sb.AppendLine($"K => Počet najazdených KM");
            sb.AppendLine($"Z => Značka");
            sb.AppendLine($"T => Typ");
            sb.AppendLine($"P => Palivo");
            sb.AppendLine($"C => Cena");
            sb.AppendLine($"M => Mesto");
            sb.AppendLine($"D => Počet dverí");
            sb.AppendLine($"H => Havarované");
            sb.AppendLine($"--------------------------------------------");
            sb.AppendLine($"********************************************");
            sb.AppendLine($"B => Vrátiť späť do hlavného menu");
            sb.AppendLine($"********************************************");
            sb.AppendLine($"--------------------------------------------");
            sb.AppendLine($"Výber z možností (R, K, Z, T, P, C, M, D) : ");
            sb.AppendLine($"--------------------------------------------");
            return sb.ToString();
        }

        public static void RepairData(List<Car> cars,int idAuta)
        {
            bool success =false;
            do
            {
                foreach (Car c in cars)
                {
                    if (c.ID == idAuta)
                    {                         
                        Console.WriteLine(RepairDataMenu());
                        char vyber = Console.ReadKey().KeyChar;
                        vyber = Char.ToUpper(vyber);
                        Console.WriteLine();
                        switch (vyber)
                        {
                            case 'R':
                                {                                    
                                    c.RokVyroby = OverIntVstup($"Zadaj rok výroby: ");                                    
                                    break;
                                }
                            case 'K':
                                {                                    
                                    c.PocetKm = OverIntVstup($"Zadaj počet km: ");                                    
                                    break;
                                }
                            case 'Z':
                                {
                                    c.Znacka = OverStringVstup($"Zadaj značku: ");                                    
                                    break;
                                }
                            case 'T':
                                {
                                    c.Typ = OverStringVstup($"Zadaj typ: ");                                    
                                    break;
                                }
                            case 'P':
                                {                                    
                                    c.Palivo = ChooseFuel();                                    
                                    break;
                                }
                            case 'C':
                                {
                                    c.CenaAuta = OverDecimalVstup($"Zadaj cenu auta: ");                                    
                                    break;
                                }
                            case 'M':
                                {
                                    c.Mesto = OverStringVstup($"Zadaj mesto: ");                                    
                                    break;
                                }
                            case 'D':
                                {
                                    c.PocetDveri = OverIntVstup($"Zadaj počet dverí: ");                                    
                                    break;
                                }
                            case 'H':
                                {
                                    c.JeHavarovane = IsDamage();
                                    break;
                                }
                            case 'B':
                                {                                    
                                    success = true;
                                    break;
                                }

                            default:
                                continue;
                        }
                    }
                }
            } while (!success);

        }

        public static TypPaliva ChooseFuel()
        {
            bool success = false;
            TypPaliva palivoTyp = 0;
            do
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Typ paliva:");
                sb.AppendLine($"D => Diesel");
                sb.AppendLine($"B => Benzin");
                sb.AppendLine($"P => Plyn");
                sb.AppendLine($"-------------------------");
                sb.AppendLine($"Vyber z možností (D,B,P):");
                sb.AppendLine($"-------------------------");
                Console.Write(sb.ToString());
                char vyber = Console.ReadKey().KeyChar;
                vyber = Char.ToUpper(vyber);
                switch (vyber)
                {
                    case 'D':
                        {
                            palivoTyp = TypPaliva.diesel;
                            success = true;
                            break;
                        }
                    case 'B':
                        {
                            palivoTyp = TypPaliva.benzin;
                            success = true;
                            break;
                        }
                    case 'P':
                        {
                            palivoTyp = TypPaliva.plyn;
                            success = true;
                            break;
                        }
                    default:
                        continue;
                }

            } while (!success);
            Console.ReadKey();
            return palivoTyp;
        }

        public static bool IsDamage()
        {
            bool havarovane=false;
            bool success =false;
            do
            {
                Console.WriteLine($"Je auto havarované?");
                Console.WriteLine($"---------------------");
                Console.WriteLine($"Výber z možností: Y/N");
                Console.WriteLine($"---------------------");
                char vyber = Console.ReadKey().KeyChar;
                vyber = Char.ToUpper(vyber);
                switch (vyber)
                {
                    case 'Y':
                        {
                            havarovane = true;
                            success = true;
                            break;
                        }
                    case 'N':
                        {
                            havarovane = false;
                            success = true;
                            break;
                        }
                    default:
                        continue;
                }
            } while (!success);
            Console.ReadKey();
            return havarovane;

        }

        public static void RemoveCar(List<Car> cars)
        {
            bool success;
            int idAuta;
            do
            {
                Console.Write($"Zadaj id auta, ktoré chceš zmazať: ");
                success = int.TryParse(Console.ReadLine(), out idAuta);
            } while (!success);
            foreach (Car c in cars)
            {
                if(c.ID == idAuta)
                {
                    cars.Remove(c);
                    Console.WriteLine($"Auto bolo zmazané");
                    break;
                }
                else
                {
                    Console.WriteLine($"Auto so zadaným ID nie je v bazáre");
                }
            }

        }
        /// <summary>
        /// Metóda na zápis zoznamu áut do textového súboru, ktorý je definovaný v ceste path.
        /// </summary>
        /// <param name="cars"></param>
        /// <param name="path"></param>
        public static void WriteToFile(List<Car> cars, string path)
        {
            if (CarShop.SaveControl())
            {
                DeleteOldFile(path);
                foreach (Car c in cars)
                {
                    File.AppendAllText(path, c.ToString());
                }
                Console.WriteLine($"Data was writed.");
            }                
            
        }
        public static bool SaveControl()
        {
            bool answer = false;
            bool success = false;
            do
            {

                Console.WriteLine($"Chcete uložiť program?\nZadajte Y/N: => ");
                char vyber = Console.ReadKey().KeyChar;
                Console.ReadKey();
                vyber = Char.ToUpper(vyber);
                //Console.ReadKey();
                switch (vyber)
                {
                    case 'Y':
                        {
                            answer = true;
                            success = true;
                            break;
                        }
                    case 'N':
                        {
                            answer = false;
                            success = true;
                            break;
                        }

                    default:
                        continue;
                }

            } while (!success);
            return answer;
        }
        /// <summary>
        /// Metóda na načítanie dát z textového súboru.
        /// </summary>
        /// <param name="path"></param>
        public static void ReadFile(List<Car> cars,string path)
        {
            if (File.Exists(path))
            {
                var lines = File.ReadAllLines(path);
                foreach (string text in lines)
                {
                    var values = text.Split('\t');
                    Car newCar = new Car(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), values[3], values[4], (TypPaliva)Enum.Parse(typeof(TypPaliva), values[5]), decimal.Parse(values[6]), values[7], int.Parse(values[8]), bool.Parse(values[9]));
                    cars.Add(newCar);
                }
                Console.WriteLine($"---------------------------------------------------");
                Console.WriteLine($"----------------Data was loaded--------------------");
                Console.WriteLine($"---------------------------------------------------");
                Console.WriteLine($"--------Pre pokračovanie stlačte klávesu.----------");
                Console.WriteLine($"---------------------------------------------------");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Súbor pre načítanie nie je vytvorený\nPre pokračovanie stlačte klávesu.");
                Console.ReadKey();
            }            
        }
        /// <summary>
        /// Metóda na vypís zoznamu áut do konzoly.
        /// </summary>
        /// <param name="cars"></param>
        public static void WriteData(List<Car> cars)
        {
            foreach(Car c in cars)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public static int GenerateID(List<Car> cars)
        {
            int id = 1;
            int pocet = cars.Count();
            if (pocet > 0)
            {
                id = cars[pocet-1].ID + 1;               
            }           
            return id;
        }

        public static int FindCar(List<Car> cars)
        {
            bool success;
            int idAuta;
            
            do
            {
                Console.WriteLine($"------------------------------------------");
                Console.WriteLine($"Zadaj id auta, v ktorom chceš zmeniť údaje : ");
                Console.WriteLine($"------------------------------------------");
                success = int.TryParse(Console.ReadLine(), out idAuta);
                foreach (Car c in cars)
                {
                    if (c.ID == idAuta)
                    {
                        success = true;
                        break;
                    }
                    else
                    {
                        
                        success = false;
                    }

                }
                if (!success)
                {
                    Console.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Console.WriteLine($"Auto so zadaným id neexistuje! Zadaj správne id.");
                    Console.WriteLine($"!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                }               

            } while (!success);
           
            //do
            //{
            //    success = int.TryParse(Console.ReadLine(), out idAuta);
            //} while (!success);
            return idAuta;
        }
        
        public static bool ExitProgram()
        {
            bool answer = false;
            bool success = false;            
            do
            {

                Console.WriteLine($"Chcete ukončiť program?\nZadajte Y/N: => ");
                char vyber = Console.ReadKey().KeyChar;
                Console.ReadKey();
                vyber = Char.ToUpper(vyber);
                //Console.ReadKey();
                switch (vyber)
                {
                    case 'Y':
                        {
                            answer = true;
                            success = true;
                            break;
                        }
                    case 'N':
                        {
                            answer = false;
                            success = true;
                            break;
                        }
                    
                    default:
                        continue;
                }

            } while (!success);
            return answer;
        }

        public static string FilterDataMenu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Ktoré údaje chcete filtrovať ");
            sb.AppendLine($"R => Rok výroby od - do");
            sb.AppendLine($"K => Počet najazdených KM od - do");
            sb.AppendLine($"Z => Značka");
            sb.AppendLine($"T => Typ");
            sb.AppendLine($"P => Palivo");
            sb.AppendLine($"C => Cena");
            sb.AppendLine($"M => Mesto");
            sb.AppendLine($"D => Počet dverí");
            sb.AppendLine($"H => Havarované");
            sb.AppendLine($"F => Filtruj");
            sb.AppendLine($"**************************");
            sb.AppendLine($"B => Vrátiť späť do menu");
            sb.AppendLine($"**************************");
            sb.Append($"Výber z možností pre vytvorenie filtra (R, K, Z, T, P, C, M, D, H)\n Vyhľadanie podľa filtra stlač => F \n Pre návrat stlačte => B:  ");
            return sb.ToString();
        }

        public static void FilterData(List<Car> cars)
        {                        
                      
            List<Car> carFilter = new List<Car>();
            carFilter = cars;
            bool success = false;
            do
            {                
                Console.WriteLine(FilterDataMenu());
                char vyber = Console.ReadKey().KeyChar;
                vyber = Char.ToUpper(vyber);
                Console.WriteLine();
                switch (vyber)
                {
                    case 'R':
                        {                            
                            carFilter = FilterDataYear(carFilter);
                            success = false;
                            continue;
                        }
                    case 'K':
                        {                            
                            carFilter = FilterDataKm(carFilter);
                            success = false;
                            continue;                            
                        }
                    case 'Z':
                        {                            
                            carFilter = FilterDataBrand(carFilter);
                            success = false;
                            continue;
                        }
                    case 'T':
                        {                            
                            carFilter = FilterDataType(carFilter);
                            success = false;
                            continue;
                        }
                    case 'P':
                        {                            
                            carFilter = FilterDataFuel(carFilter);
                            success = false;
                            continue;
                        }
                    case 'C':
                        {                            
                            carFilter = FilterDataPrice(carFilter);
                            success = false;
                            continue;
                        }
                    case 'M':
                        {                            
                            carFilter = FilterDataCity(carFilter);
                            success = false;
                            continue;
                        }
                    case 'D':
                        {                            
                            carFilter = FilterDataDoor(carFilter);
                            success = false;
                            continue;
                        }
                    case 'H':
                        {                            
                            carFilter = FilterDataDamage(carFilter);
                            success = false;
                            continue;
                        }

                    case 'F':
                        {
                            WriteData(carFilter);
                            success = true;
                            break;
                        }
                    case 'B':
                        {                            
                            success = true;
                            break;
                        }

                    default:
                        continue;
                }
               

            } while (!success);
         
        }

        public static void DeleteOldFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
        }
        
        public static List<Car> FilterDataYear(List<Car> items)
        {            
            List<Car> filteredCar = new List<Car>();
            int rokOd = OverIntVstup($"Od roku výroby: ");
            int rokDo = OverIntVstup($"Do roku výroby: ");            
            foreach (Car c in items)
            {
                if (rokOd <= c.RokVyroby && c.RokVyroby <= rokDo)
                {
                    Console.WriteLine(c.ToString());
                    filteredCar.Add(c);
                }                               
            }
            return filteredCar;
        }

        public static List<Car> FilterDataKm(List<Car> items)
        {            
            List<Car> filteredCar = new List<Car>();
            int kmOd = OverIntVstup($"KM od: ");
            int kmDo = OverIntVstup($"KM od: ");            
            foreach (Car c in items)
            {
                if (kmOd <= c.PocetKm && c.PocetKm <= kmDo)
                {
                    Console.WriteLine(c.ToString());
                    filteredCar.Add(c);
                }
            }
            return filteredCar;
        }

        public static List<Car> FilterDataPrice(List<Car> items)
        {            
            List<Car> filteredCar = new List<Car>();
            decimal cenaOd = OverDecimalVstup($"Cena auta od: ");
            decimal cenaDo = OverDecimalVstup($"Cena auta do: ");            
            foreach (Car c in items)
            {
                if (cenaOd <= c.CenaAuta && c.CenaAuta <= cenaDo)
                {
                    Console.WriteLine(c.ToString());
                    filteredCar.Add(c);
                }
            }
            return filteredCar;
        }

        public static List<Car> FilterDataDoor(List<Car> items)
        {            
            List<Car> filteredCar = new List<Car>();
            int dvereOd = OverIntVstup($"Počet dverí od: ");
            int dvereDo = OverIntVstup($"Počet dverí do: ");            
            foreach (Car c in items)
            {
                if (dvereOd <= c.PocetDveri && c.PocetDveri <= dvereDo)
                {
                    Console.WriteLine(c.ToString());
                    filteredCar.Add(c);
                }
            }
            return filteredCar;
        }

        public static List<Car> FilterDataBrand(List<Car> items)
        {                       
            List<Car> filteredCar = new List<Car>();            
            string znackaAuta = OverStringVstup($"Zadaj značku/značky áut a oddeľte ich čiakou: ");
            znackaAuta = znackaAuta.Replace(" ", "");
            var values = znackaAuta.Split(',');
            foreach (Car c in items)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (c.Znacka==values[i])
                    {
                        Console.WriteLine(c.ToString());
                        filteredCar.Add(c);
                        i++;
                    }
                }                
            }
            return filteredCar;
        }

        public static List<Car> FilterDataType(List<Car> items)
        {            
            List<Car> filteredCar = new List<Car>();            
            string typAuta = OverStringVstup($"Zadaj typ/typy áut a oddeľte ich čiakou: ");
            typAuta = typAuta.Replace(" ", "");
            var values = typAuta.Split(',');
            foreach (Car c in items)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (c.Typ == values[i])
                    {
                        Console.WriteLine(c.ToString());
                        filteredCar.Add(c);
                        i++;
                    }
                }
            }
            return filteredCar;
        }

        public static List<Car> FilterDataCity(List<Car> items)
        {                       
            List<Car> filteredCar = new List<Car>();            
            string mesto = OverStringVstup($"Zadajte mesto/mestá a oddeľte ich čiarkou: ");
            mesto = mesto.Replace(" ", "");
            var values = mesto.Split(',');
            foreach (Car c in items)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (c.Mesto == values[i])
                    {
                        Console.WriteLine(c.ToString());
                        filteredCar.Add(c);
                        i++;
                    }
                }
            }
            return filteredCar;
        }

        public static List<Car> FilterDataDamage(List<Car> items)
        {
            bool priznak = IsDamage();

            List<Car> filteredCar = new List<Car>();
            foreach (Car c in items)
            {

                if (c.JeHavarovane == priznak)
                {
                    Console.WriteLine(c.ToString());
                    filteredCar.Add(c);
                }

            }
            return filteredCar;            
        }

        public static List<Car> FilterDataFuel(List<Car> items)
        {
            bool success = false;
            List<TypPaliva> palivo = new List<TypPaliva>();
            palivo.Add(ChooseFuel());
            do
            {
                Console.WriteLine($"Chcete pridať ďalší druh paliva do filtra?\nZadajte Y/N: => ");
                char vyber = Console.ReadKey().KeyChar;
                Console.ReadKey();
                vyber = Char.ToUpper(vyber);                
                switch (vyber)
                {
                    case 'Y':
                        {
                            palivo.Add(ChooseFuel());                            
                            break;
                        }
                    case 'N':
                        {                            
                            success = true;
                            break;
                        }

                    default:
                        continue;
                }

            } while (!success);



            List<Car> filteredCar = new List<Car>();
            foreach (Car c in items)
            {
                for (int i = 0; i < palivo.Count(); i++)
                {
                    if (c.Palivo == palivo[i])
                    {
                        Console.WriteLine(c.ToString());
                        filteredCar.Add(c);
                    }
                }              

            }
            return filteredCar;
        }        
    }
}
