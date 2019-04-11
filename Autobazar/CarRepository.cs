using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Autobazar
{
    class CarRepository
    {
        readonly string connString = @"Server = VALJASEK\SQL2017; Database = CarBazar; Trusted_Connection = True;";      

        public List<Car> GetAll()
        {
            string sqlGetAll = @"select * from Car";
            List<Car> cars = new List<Car>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    try
                    {
                        using(SqlCommand cmd=new SqlCommand(sqlGetAll, connection))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Car car = new Car();
                                    car.ID = reader.GetInt32(0);
                                    car.Znacka = reader.GetString(1);
                                    car.Typ = reader.GetString(2);
                                    car.RokVyroby = reader.GetInt32(3);
                                    car.PocetKm = reader.GetInt32(4);
                                    car.Palivo = (TypPaliva)reader.GetInt32(5);
                                    car.CenaAuta = reader.GetDecimal(6);
                                    car.Mesto = reader.GetString(7);
                                    car.PocetDveri = reader.GetInt32(8);
                                    car.JeHavarovane = reader.GetBoolean(9);
                                    cars.Add(car);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return cars;
        }

        public Car GetById(int id)
        {
            string sqlGetById = @"select * from Car where id = @id";
            Car car = new Car();
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlGetById, connection))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {                                    
                                    car.ID = reader.GetInt32(0);
                                    car.Znacka = reader.GetString(1);
                                    car.Typ = reader.GetString(2);
                                    car.RokVyroby = reader.GetInt32(3);
                                    car.PocetKm = reader.GetInt32(4);
                                    car.Palivo = (TypPaliva)reader.GetInt32(5);
                                    car.CenaAuta = reader.GetDecimal(6);
                                    car.Mesto = reader.GetString(7);
                                    car.PocetDveri = reader.GetInt32(8);
                                    car.JeHavarovane = reader.GetBoolean(9);                                    
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return car;
        }

        public int AddNewCar(Car car)
        {
            int insertId = 0;
            string sqlInsertCar = @"insert into car output inserted.id values(@znacka,@typ,@rokVyroby,@pocetKm,@palivo,@cena,@mesto,@pocetDveri,@havarovane)";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlInsertCar, connection))
                        {
                            cmd.Parameters.Add("@znacka", SqlDbType.NVarChar).Value = car.Znacka;
                            cmd.Parameters.Add("@typ", SqlDbType.NVarChar).Value = car.Typ;
                            cmd.Parameters.Add("@rokVyroby", SqlDbType.Int).Value = car.RokVyroby;
                            cmd.Parameters.Add("@pocetKm", SqlDbType.Int).Value = car.PocetKm;
                            cmd.Parameters.Add("@palivo", SqlDbType.Int).Value = car.Palivo;
                            cmd.Parameters.Add("@cena", SqlDbType.Decimal).Value = car.CenaAuta;
                            cmd.Parameters.Add("@mesto", SqlDbType.NVarChar).Value = car.Mesto;
                            cmd.Parameters.Add("@pocetDveri", SqlDbType.Int).Value = car.PocetDveri;
                            cmd.Parameters.Add("@havarovane", SqlDbType.Bit).Value = car.JeHavarovane;
                            insertId = (int)cmd.ExecuteScalar();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return insertId;
        }


        public bool UpdateCar(Car car)
        {            
            string sqlUpdateCar = @"update car set znacka = @znacka,typ = @typ,rokvyroby = @rokVyroby,pocetKm = @pocetKm,palivo = @palivo,cena = @cena,mesto = @mesto,pocetDveri = @pocetDveri,havarovane =@havarovane where id = @id";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlUpdateCar, connection))
                        {
                            cmd.Parameters.Add("@znacka", SqlDbType.NVarChar).Value = car.Znacka;
                            cmd.Parameters.Add("@typ", SqlDbType.NVarChar).Value = car.Typ;
                            cmd.Parameters.Add("@rokVyroby", SqlDbType.Int).Value = car.RokVyroby;
                            cmd.Parameters.Add("@pocetKm", SqlDbType.Int).Value = car.PocetKm;
                            cmd.Parameters.Add("@palivo", SqlDbType.Int).Value = car.Palivo;
                            cmd.Parameters.Add("@cena", SqlDbType.Decimal).Value = car.CenaAuta;
                            cmd.Parameters.Add("@mesto", SqlDbType.NVarChar).Value = car.Mesto;
                            cmd.Parameters.Add("@pocetDveri", SqlDbType.Int).Value = car.PocetDveri;
                            cmd.Parameters.Add("@havarovane", SqlDbType.Bit).Value = car.JeHavarovane;
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = car.ID;
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                return true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }


        public bool DeleteCar(int id)
        {            
            string sqlDeleteCar = @"delete from car where id = @id";
            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlDeleteCar, connection))
                        {
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                            if (cmd.ExecuteNonQuery() > 0)
                            {
                                return true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
    }
}
