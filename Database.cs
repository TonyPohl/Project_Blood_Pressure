using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPressure
{
    /// <summary>
    /// Class represents database
    /// </summary>
    internal class Database
    {
        readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Tony a Vlaďka\Dropbox\Projekty\ProjectPressure\PressureDB.mdf;Initial Catalog=PressureDB;Integrated Security=True";
        /// <summary>
        /// Header for listing all records
        /// </summary>
        public void ListingHeader()
        {
            Console.WriteLine("  Id   |  Date\t  |  Time   |   Sys  |  Dia  | Pulse | \n======================================================");
        }                
        /// <summary>
        /// Count of all records
        /// </summary>
        public void CountOfRecords()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }                
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT COUNT(*) FROM Pressure"
                };
                // number of affected rows
                int numbersOfRecords = (int)cmd.ExecuteScalar();
                conn.Close();                
                Console.WriteLine($"The count of records in the database is {numbersOfRecords}.");
            }            
        }
        /// <summary>
        /// Add new record
        /// </summary>
        public void AddRecord() 
        {
            SqlConnection conn = new SqlConnection(connectionString);

            // DATA INSERTING

            //Date and time
            Console.WriteLine("Insert date and time in form DD.MM.YY hh:mm:");
            DateTime dateTime;
            while(!DateTime.TryParse(Console.ReadLine(), out dateTime))
                Console.WriteLine("Invalid value, please insert a value in form DD.MM.YY hh:mm!");

            // Systolic pressure
            Console.WriteLine("Insert a systolic pressure value:");
            int systolic;
            while (!int.TryParse(Console.ReadLine(), out systolic))
                Console.WriteLine("Invalid value, please insert a numeric value!");

            // Diastolic pressure
            Console.WriteLine("Insert a diastolic pressure value:");
            int diastolic;
            while(!int.TryParse(Console.ReadLine(),out diastolic))
                Console.WriteLine("Invalid value, please insert a numeric value!");

            // Heart rate
            Console.WriteLine("Insert a heart rate value:");
            int pulse;
            while (!int.TryParse(Console.ReadLine(), out pulse))
                Console.WriteLine("Invalid value, please insert a numeric value!");

            // SQL QUERY
            string query = "INSERT INTO Pressure (DateTime, Systolic, Diastolic, Pulse) " +
                "VALUES (@dateTime, @systolic, @diastolic, @pulse)";

            using(SqlCommand sqlQuery = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                sqlQuery.Parameters.AddWithValue("@dateTime", dateTime);
                sqlQuery.Parameters.AddWithValue("@Systolic", systolic);
                sqlQuery.Parameters.AddWithValue("@Diastolic", diastolic);
                sqlQuery.Parameters.AddWithValue("@Pulse", pulse);
                // number of affected rows
                int rows = sqlQuery.ExecuteNonQuery();
                Console.WriteLine($"The count of successfully saved records is {rows}.");
            }
        }        
        /// <summary>
        /// Remove a record via ID
        /// </summary>
        public void RemoveRecord()
        {
            using(SqlConnection conn  = new SqlConnection(connectionString))
            {
                // DATA NSERTING
                Console.WriteLine("Insert the ID you want to remove:");
                int id;
                while(!int.TryParse(Console.ReadLine(), out id))
                    Console.WriteLine("Invalid value, please insert a numeric value!");
               
                // SQL QUERY
                string query = "DELETE FROM Pressure WHERE Id=@id";
                using(SqlCommand sqlQuery = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    sqlQuery.Parameters.AddWithValue("@id", id);
                    // number of affected rows
                    int rows = sqlQuery.ExecuteNonQuery();
                    Console.WriteLine($"The count of successfully removed records is {rows}.");
                }
                Console.ReadKey();
            }
        }
        /// <summary>
        /// List of all records
        /// </summary>
        public void ListingAllRecords()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand sqlQuery = new SqlCommand("SELECT * FROM Pressure", conn);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            SqlDataReader reader = sqlQuery.ExecuteReader();
            while(reader.Read())
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",
                    reader[0],
                    reader.GetDateTime(1),
                    reader["Systolic"],
                    reader["Diastolic"],
                    reader["Pulse"]);
            }            
        }
        /// <summary>
        /// Average of all systolic pressure records
        /// </summary>
        public void CalculateSystolicAvg()
        {
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                SqlCommand sqlQuery = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT AVG(Systolic) FROM Pressure"
                };
                double avgSys = (int)sqlQuery.ExecuteScalar();
                conn.Close();
                Console.WriteLine($"The average of all systolic pressure records is {Math.Round(avgSys)} mmHg.");
            }
        }
        /// <summary>
        /// Average of all diastolic pressure records
        /// </summary>
        public void CalculateDiastolicAvg()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                SqlCommand sqlQuery = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT AVG(Diastolic) FROM Pressure"
                };
                double avgDia = (int)sqlQuery.ExecuteScalar();
                conn.Close();
                Console.WriteLine($"The average of all diastolic pressure records is {Math.Round(avgDia)} mmHg.");
            }
        }
        /// <summary>
        /// Average of all heart rate records
        /// </summary>
        public void CalculatePulseAvg()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                SqlCommand sqlQuery = new SqlCommand
                {
                    Connection = conn,
                    CommandText = "SELECT AVG(Pulse) FROM Pressure"
                };
                double pulse = (int)sqlQuery.ExecuteScalar();
                conn.Close();
                Console.WriteLine($"The average of all heart rate records is {Math.Round(pulse)} bpm.");
            }
        }
    }
}
