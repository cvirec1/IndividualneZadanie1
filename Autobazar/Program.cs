using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobazar
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Transformers\Carshop.txt";
            List<Car> Cars = new List<Car>();
            CarShop.ReadFile(Cars, path);
            bool quit = false;
            do
            {
                Console.Clear();
                CarShop.MainMenu();
                
                Console.WriteLine("Zadaj voľbu: ");
                char menu = Console.ReadKey().KeyChar;
                Console.ReadKey();
                menu = Char.ToUpper(menu);
                Console.WriteLine();                
                switch (menu)
                {
                    case 'P':
                        {
                            Cars.Add(CarShop.AddCar(CarShop.GenerateID(Cars)));
                            break;
                        }
                    case 'Z':
                        {                            
                            CarShop.WriteData(Cars);
                            CarShop.RepairData(Cars, CarShop.FindCar(Cars));
                            break;
                        }
                    case 'O':
                        {
                            CarShop.RemoveCar(Cars);
                            CarShop.WriteData(Cars);
                            Console.ReadKey();
                            break;
                        }
                    case 'V':
                        {
                            CarShop.WriteData(Cars);
                            Console.ReadKey();
                            break;
                        }
                    case 'F':
                        {
                            CarShop.FilterData(Cars);                            
                            Console.ReadKey();
                            break;
                        }
                    case 'N':
                        {
                            CarShop.ReadFile(Cars, path);
                            break;
                        }
                    case 'U':
                        {                            
                            CarShop.WriteToFile(Cars, path);
                            break;
                        }
                    case 'Q':
                        {
                            CarShop.WriteToFile(Cars, path);
                            if (!CarShop.ExitProgram()) continue;
                            quit = true;
                            break;
                        }
                    default:
                        continue;                    
                }
            } while (!quit);
            Console.ReadKey();
        }
    }
}
