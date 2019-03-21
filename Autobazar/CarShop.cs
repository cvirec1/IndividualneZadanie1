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
            sb.AppendLine($"||||********AUTOBAZAR********||||");
            sb.AppendLine($"P => Pridaj nové auto.");
            sb.AppendLine($"Z => Zmeň údaje o aute.");
            sb.AppendLine($"O => Odstráň auto.");
            sb.AppendLine($"V => Vypíš zoznam áut v bazáry.");
            sb.AppendLine($"F => Vytvor filter.");
            sb.AppendLine($"N => Načítaj zoznam áut bazáru zo súboru.");
            sb.AppendLine($"U => Ulož zoznam áut bazáru do súboru.");
            sb.AppendLine($"Q => Ukonči program.");
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
            sb.AppendLine($"Ktoré údaje chcete zmeniť na aute?");
            sb.AppendLine($"R => Rok výroby");
            sb.AppendLine($"K => Počet najazdených KM");
            sb.AppendLine($"Z => Značka");
            sb.AppendLine($"T => Typ");
            sb.AppendLine($"P => Palivo");
            sb.AppendLine($"C => Cena");
            sb.AppendLine($"M => Mesto");
            sb.AppendLine($"D => Počet dverí");
            sb.AppendLine($"**************************");
            sb.AppendLine($"B => Vrátiť späť do menu");
            sb.AppendLine($"**************************");
            sb.Append($"Výber z možností (R, K, Z, T, P, C, M, D) : ");
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
                                    Console.WriteLine($"Zadaj rok výroby: ");
                                    c.RokVyroby = int.Parse(Console.ReadLine());
                                    success = true;
                                    break;
                                }
                            case 'K':
                                {
                                    Console.WriteLine($"Zadaj počet km: ");
                                    c.PocetKm = int.Parse(Console.ReadLine());
                                    success = true;
                                    break;
                                }
                            case 'Z':
                                {
                                    Console.WriteLine($"Zadaj značku: ");
                                    c.Znacka = Console.ReadLine();
                                    success = true;
                                    break;
                                }
                            case 'T':
                                {
                                    Console.WriteLine($"Zadaj typ: ");
                                    c.Typ = Console.ReadLine();
                                    success = true;
                                    break;
                                }
                            case 'P':
                                {                                    
                                    c.Palivo = ChooseFuel();
                                    success = true;
                                    break;
                                }
                            case 'C':
                                {
                                    Console.WriteLine($"Zadaj cenu auta: ");
                                    c.CenaAuta = decimal.Parse(Console.ReadLine());
                                    success = true;
                                    break;
                                }
                            case 'M':
                                {
                                    Console.WriteLine($"Zadaj mesto: ");
                                    c.Mesto = Console.ReadLine();
                                    success = true;
                                    break;
                                }
                            case 'D':
                                {
                                    Console.WriteLine($"Zadaj počet dverí: ");
                                    c.PocetDveri = int.Parse(Console.ReadLine());
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
                sb.AppendLine($"Vyber z možností (D,B,P):");
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
                Console.Write($"Je auto havarované?\nVýber z možností: Y/N\n");
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
                Console.WriteLine($"Data was loaded.\nPre pokračovanie stlačte klávesu.");
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
                //foreach (Car c in cars)
                //{
                //    if (c.ID >= 1)
                //    {
                //        id = ;
                //    }
                //}
            }           
            return id;
        }

        public static int FindCar(List<Car> cars)
        {
            bool success;
            int idAuta;
            
            do
            {
                Console.Write($"Zadaj id auta, v ktorom chceš zmeniť údaje : ");
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
            bool success = false;
            int rokOd;
            int rokDo;
            List<Car> filteredCar = new List<Car>();
            do
            {
                Console.Write($"Od roku výroby: ");
                success = int.TryParse(Console.ReadLine(), out rokOd);
            } while (!success);
            do
            {
                Console.Write($"Do roku výroby: ");
                success = int.TryParse(Console.ReadLine(), out rokDo);
            } while (!success);
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
            bool success = false;
            int kmOd;
            int kmDo;
            List<Car> filteredCar = new List<Car>();
            do
            {
                Console.Write($"KM od: ");
                success = int.TryParse(Console.ReadLine(), out kmOd);
            } while (!success);
            do
            {
                Console.Write($"Km do: ");
                success = int.TryParse(Console.ReadLine(), out kmDo);
            } while (!success);
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
            bool success = false;
            decimal cenaOd;
            decimal cenaDo;
            List<Car> filteredCar = new List<Car>();
            do
            {
                Console.Write($"Cena auta od: ");
                success = decimal.TryParse(Console.ReadLine(), out cenaOd);
            } while (!success);
            do
            {
                Console.Write($"Cena auta do: ");
                success = decimal.TryParse(Console.ReadLine(), out cenaDo);
            } while (!success);
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
            bool success = false;
            int dvereOd;
            int dvereDo;
            List<Car> filteredCar = new List<Car>();
            do
            {
                Console.Write($"Počet dverí od: ");
                success = int.TryParse(Console.ReadLine(), out dvereOd);
            } while (!success);
            do
            {
                Console.Write($"Počet dverí do: ");
                success = int.TryParse(Console.ReadLine(), out dvereDo);
            } while (!success);
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
            bool success = false;
            string znackaAuta = "";
            List<Car> filteredCar = new List<Car>();
            do
            {
                Console.Write($"Zadaj značku/značky áut a oddeľte ich čiakou:: ");
                do
                {
                    znackaAuta = Console.ReadLine();
                } while (znackaAuta == "");
                success = true;
            } while (!success);
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
            bool success = false;
            string typAuta = "";
            List<Car> filteredCar = new List<Car>();
            do
            {
                Console.Write($"Zadaj typ/typy áut a oddeľte ich čiakou: ");
                do
                {
                    typAuta = Console.ReadLine();
                } while (typAuta == "");
                success = true;
            } while (!success);
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

            bool success = false;
            string mesto = "";
            List<Car> filteredCar = new List<Car>();
            do
            {
                Console.Write($"Zadajte mesto/mestá a oddeľte ich čiarkou: ");
                do
                {
                    mesto = Console.ReadLine();
                } while (mesto== "");
                success = true;
            } while (!success);
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
