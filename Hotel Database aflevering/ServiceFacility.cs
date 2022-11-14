using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Azure.Core.GeoJson;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Hotel_Database_aflevering
{
    public class ServiceFacility
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hotel;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    
        public int GetAllFacality(SqlConnection connection)
        {
            string querystringAllFacility = "SELECT * from DemoFaciletes";
            Console.WriteLine(querystringAllFacility);
            SqlCommand command = new SqlCommand(querystringAllFacility, connection);
            SqlDataReader reader = command.ExecuteReader();
            int allFacality = 0;
            if (reader.Read())
            {
                allFacality = reader.GetInt32(0);
            }
            reader.Close();
            Console.WriteLine(allFacality);
            return allFacality;
        }
        private int CreateFacility(SqlConnection connection, Facility facility)
        {
            Console.WriteLine("Create Facility");
            string queryCreateFacility = $"INSERT INTO DemoFaciletes VALUES({facility.Faciletet_id},'{facility.Name}')";
            Console.WriteLine(queryCreateFacility);
            SqlCommand command = new SqlCommand(queryCreateFacility, connection);
            Console.WriteLine($"Create Facility : {facility.Faciletet_id}");
            var numberOfRowsAffected = command.ExecuteNonQuery();
            Console.WriteLine(numberOfRowsAffected);
            return numberOfRowsAffected;
        }



        private int RemoveFacility(SqlConnection connection, int Faciletet_id)
        {
            string deleteCommandString = $"Delete FROM DemoFaciletes Where Faciletet_id = {Faciletet_id}";
            Console.WriteLine(deleteCommandString);
            SqlCommand command = new SqlCommand(@deleteCommandString, connection);
            Console.WriteLine(Faciletet_id);
            int numberOfRowsAffected = command.ExecuteNonQuery();
            Console.WriteLine(numberOfRowsAffected);
            return numberOfRowsAffected;        
                        
        }
        private int UpdateFacility(SqlConnection connection, Facility facility)
        // Her får vi muligheden for at oprettet nye faciliteter i samarbejde med databasen
        {
            string updateCommandString = $"UPDATE DemoFaciletes SET Name = '{facility.Name}' Where Faciletet_id = {facility.Faciletet_id} ";
            Console.WriteLine(updateCommandString);
            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine(facility.Faciletet_id);
            int numberOfRowsAffected = command.ExecuteNonQuery();
            Console.WriteLine(numberOfRowsAffected);
            return numberOfRowsAffected;
        }
        private List<Facility> listAllfacility (SqlConnection connection)
        {
            string QueryListAllFacility = "SELECT * FROM DemoFaciletes";
            Console.WriteLine(QueryListAllFacility);
            SqlCommand query = new SqlCommand(QueryListAllFacility, connection);
            SqlDataReader reader = query.ExecuteReader();

            if (!reader.Read())
            {
                Console.WriteLine("Not Facility in database");
                reader.Close();
                return null;
            }
        

                List<Facility> facilities = new List<Facility>();
                while (reader.Read())
                {
                Facility facility = new Facility()
                {
                    Faciletet_id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                facilities.Add(facility);
                Console.WriteLine(facility);
                    
                }
            reader.Close();    
            return facilities;
        }
        private Facility GetFacility(SqlConnection connection, int Faciletet_id)
        {
            string QueryStringGetFacility = $"SELECT * from DemoFaciletes where Faciletet_id = {Faciletet_id} ";
            Console.WriteLine(QueryStringGetFacility);
            SqlCommand query = new SqlCommand(QueryStringGetFacility, connection);
            SqlDataReader reader = query.ExecuteReader();
            
            if (!reader.HasRows)
            {
                Console.WriteLine("no Hotels in Database ");
                reader.Close ();
                return null;
            }
            Facility facility = null;
            if(reader.Read())
            {
                facility = new Facility()
                {
                    Faciletet_id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                };
                Console.WriteLine(facility);
               
            }
            reader.Close();
            return facility;

        }

        public int GetMaxHotelNo(SqlConnection connection)
        {
            string queryStringAllHotel = "Select * from DemoHotel";
            Console.WriteLine(queryStringAllHotel);
            SqlCommand command = new SqlCommand(queryStringAllHotel, connection);
            SqlDataReader reader = command.ExecuteReader();
            int AllHotel = 0;
            if (reader.Read())
            {
                AllHotel = reader.GetInt32(0);
            }
            reader.Close();
            Console.WriteLine(AllHotel);
            return AllHotel;
        }
        private List<Hotel> AllHotels(SqlConnection connection)
        {
            string queryStringAllHotel = "Select * from DemoHotel";
            Console.WriteLine(queryStringAllHotel);
            SqlCommand command = new SqlCommand (queryStringAllHotel, connection);
            SqlDataReader reader =command.ExecuteReader();
            Console.WriteLine("List of all HotelFaciletes");
            if (!reader.Read())
            {
                Console.WriteLine("No Hotels in Database");
                reader.Close();
                return null;
            }
            List<Hotel> hotels = new List<Hotel>();
            while (reader.Read())
            {
                Hotel nextHotel = new Hotel()
                {
                    Hotel_No = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2),
                };
                hotels.Add(nextHotel);
                Console.WriteLine(nextHotel);
            }
            reader.Close();
            Console.WriteLine();
            return hotels;
        }
        private Hotel GetHotel(SqlConnection connection, int Hotel_No)
        {
            string queryStringHotel = $"Select * from DemoHotel where Hotel_No = {Hotel_No}";
            Console.WriteLine(queryStringHotel);
            SqlCommand query = new SqlCommand(queryStringHotel, connection);
            SqlDataReader reader = query.ExecuteReader();

            Console.WriteLine($"{Hotel_No}");

            if (!reader.HasRows)
            {
                Console.WriteLine("No Hotel in database");
                reader.Close();
                return null;
            }
            Hotel hotel = null;

            if (reader.Read())
            {
                hotel = new Hotel()
                {
                    Hotel_No = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2),
                };
                Console.WriteLine(hotel);
            }
            reader.Close();
            Console.WriteLine();
            return hotel;
        }
           private int GetMaxFacilityId(SqlConnection connection)
        {
            string queryStringAllFacility = $"Select Max(faciletet_id) From DemoFaciletes";
            Console.WriteLine(queryStringAllFacility);
            SqlCommand command = new SqlCommand(queryStringAllFacility, connection);
            SqlDataReader reader = command.ExecuteReader();
            int result = 0;
            if (reader.Read())
            {
                result = reader.GetInt32(0); 
            }
            reader.Close();
            Console.WriteLine($"Max Faciletet ID : {result}");
            return result;
        }           
        private int GetMaxHotelFacility(SqlConnection connection)
        {
            string queryStringAllHotelFacility = "SELECT * From HotelFacility";
            Console.WriteLine(queryStringAllHotelFacility);
            SqlCommand command = new SqlCommand(queryStringAllHotelFacility, connection);
            SqlDataReader reader = command.ExecuteReader();
            int HotelFacility = 0;
            while (reader.Read())
            {
                HotelFacility = reader.GetInt32(0);
            }
            reader.Close ();
            Console.WriteLine($"All Hotel with Facility{HotelFacility}");
            return HotelFacility;
        }
        private int CreateHotelFacility(SqlConnection connection, HotelFacility hotelFacility)
        {
            string inserCommandHotelFacility = $"INSERT INTO HotelFacility VALUES ({hotelFacility.id}, {hotelFacility.Hotel_No}, {hotelFacility.Faciletet_id})";
            Console.WriteLine(inserCommandHotelFacility);
            SqlCommand command = new SqlCommand (inserCommandHotelFacility, connection);
            Console.WriteLine($"Create HotelFacility : {hotelFacility.id}");
            int numbersOfRowAffected = command.ExecuteNonQuery();
            Console.WriteLine(numbersOfRowAffected);
            return numbersOfRowAffected;
        }
        private  int DeleteHotelFacility(SqlConnection connection, int id)
        {
            string deleteCommandString = $"Delete from HotelFacility Where id = {id}";
            Console.WriteLine(deleteCommandString);
            SqlCommand command = new SqlCommand(deleteCommandString, connection);
            Console.WriteLine($"Delete HotelFacility : {id}");
            int numbersOfRowAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Numbers of row Affected : { numbersOfRowAffected}");
            return numbersOfRowAffected;
        }
        private int UpdateHotelFacility (SqlConnection connection,HotelFacility hotelFacility)
        {
            string updateCommandString = $"UPDATE HotelFacility SET Hotel_No = {hotelFacility.Hotel_No}, Faciletet_id = {hotelFacility.Faciletet_id} Where id = {hotelFacility.id}";
            SqlCommand command = new SqlCommand(updateCommandString, connection);
            Console.WriteLine($"Update HotelFacility : {hotelFacility.id}");
            int numbersOfRowAffected = command.ExecuteNonQuery();
            Console.WriteLine($"Numbers of Row Affected : {numbersOfRowAffected}");
            return numbersOfRowAffected;
        }
        private List<HotelFacility> ListHotelFacility(SqlConnection connection)
        {
            string queryStringHotelFacility = $"Select * from HotelFacility";
            SqlCommand command = new SqlCommand(queryStringHotelFacility, connection);
            SqlDataReader reader = command.ExecuteReader();
            Console.WriteLine($"List All HotelFacilities");
            if (!reader.Read())
            {
                Console.WriteLine($"No HotelFacilities in Database");
                reader.Close();
                return null;
            }
           List <HotelFacility> hotelFacility = new List<HotelFacility>();
            while (reader.Read())
            {
                HotelFacility hotelFacility1 = new HotelFacility()
                {
                    id = reader.GetInt32(0),
                    Faciletet_id = reader.GetInt32(1),
                    Hotel_No = reader.GetInt32(2),
                };
                hotelFacility.Add(hotelFacility1);
                Console.WriteLine(hotelFacility1);
            }
            reader.Close();
            Console.WriteLine();
            return hotelFacility;
        }
        private HotelFacility GetHotelFacility(SqlConnection connection, int id)
        {
            string queryStringHotelFacility = $"SELECT * FROM HotelFacility Where id = {id}";
            Console.WriteLine(queryStringHotelFacility);
            SqlCommand query = new SqlCommand(queryStringHotelFacility, connection);
            SqlDataReader reader = query.ExecuteReader();

            Console.WriteLine(id);
            if (!reader.HasRows)
            {
                Console.WriteLine("No HotelFacility in Database!");
                reader.Close();
                return null;
            }
            HotelFacility hotelFacility = null;
            while (reader.Read())
            {
                hotelFacility = new HotelFacility()
                {
                    id = reader.GetInt32(0),
                    Hotel_No = reader.GetInt32(1),
                    Faciletet_id = reader.GetInt32(2)    
                };
                Console.WriteLine(hotelFacility);  
            }
            reader.Close();
            Console.WriteLine();
            return hotelFacility;
        }
        public void Run()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                AllHotels(connection);
                Hotel hotel = new Hotel()
                {
                    Hotel_No = GetMaxHotelNo(connection),
                    Name = " New Hotel : ",
                    Address = " New Adress : "
                };
                listAllfacility(connection);
                Facility newFacility = new Facility()
                {
                    Faciletet_id = GetMaxFacilityId(connection) + 1,
                    Name = "New Facility : "
                };
                CreateFacility(connection, newFacility);
                listAllfacility(connection);
                Facility facilityToUpdate = GetFacility(connection, newFacility.Faciletet_id);
                facilityToUpdate.Name = "(Update Name)";

                UpdateFacility(connection, newFacility);

                listAllfacility(connection);
                Facility facilityToDelete = GetFacility(connection, facilityToUpdate.Faciletet_id);

                RemoveFacility(connection, facilityToDelete.Faciletet_id);
                listAllfacility(connection);

                Console.WriteLine();

                ListHotelFacility(connection);
                HotelFacility newHotelFacility = new HotelFacility()
                {
                    id = GetMaxHotelFacility(connection) + 1,
                    Hotel_No = GetMaxHotelNo(connection),
                    Faciletet_id = GetMaxFacilityId(connection)
                };
                CreateHotelFacility(connection, newHotelFacility);
                ListHotelFacility(connection);

                HotelFacility hotelFacilityUpadte = GetHotelFacility(connection, newHotelFacility.id);
                hotelFacilityUpadte.Faciletet_id += facilityToUpdate.Faciletet_id;
                hotelFacilityUpadte.Hotel_No += GetMaxHotelNo(connection);

                UpdateHotelFacility(connection, newHotelFacility);
                ListHotelFacility(connection);

                HotelFacility hotelFacilityToRemove = GetHotelFacility(connection, newHotelFacility.id);

                DeleteHotelFacility(connection, hotelFacilityToRemove.id);
                ListHotelFacility(connection);

            }
        }
    }     
}
 


