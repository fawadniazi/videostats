using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace VideoStats
{
    public class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "videosDB";
            uid = "root";
            password = "cafeblue";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert()
        {
            string query = "INSERT INTO tableinfo (name, age) VALUES('John Smith', '33')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }


        public void Insert(string query, string col1, string col2, string col3, string col4, string col5, string col6)
        {


            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@col1", col1);
                cmd.Parameters.AddWithValue("@col2", col2);
                cmd.Parameters.AddWithValue("@col3", col3);
                cmd.Parameters.AddWithValue("@col4", col4);
                cmd.Parameters.AddWithValue("@col5", col5);
                cmd.Parameters.AddWithValue("@col6", col6);
                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }



        public void Insert(string query, string col1, string col2, string col3, string col4, string col5, string col6, string col7, string col8, string col9)
        {


            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@col1", col1);
                cmd.Parameters.AddWithValue("@col2", col2);
                cmd.Parameters.AddWithValue("@col3", col3);
                cmd.Parameters.AddWithValue("@col4", col4);
                cmd.Parameters.AddWithValue("@col5", col5);
                cmd.Parameters.AddWithValue("@col6", col6);
                cmd.Parameters.AddWithValue("@col7", col7);
                cmd.Parameters.AddWithValue("@col8", col8);
                cmd.Parameters.AddWithValue("@col9", col9);
               // cmd.Parameters.AddWithValue("@col10", col10);
                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }


        public void Insert(string query, string col1, string col2, string col3, string col4, string col5, string col6, string col7, string col8, string col9,
            string col10, string col11, string col12, string col13, string col14, string col15)
        {


            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@col1", col1);
                cmd.Parameters.AddWithValue("@col2", col2);
                cmd.Parameters.AddWithValue("@col3", col3);
                cmd.Parameters.AddWithValue("@col4", col4);
                cmd.Parameters.AddWithValue("@col5", col5);
                cmd.Parameters.AddWithValue("@col6", col6);
                cmd.Parameters.AddWithValue("@col7", col7);
                cmd.Parameters.AddWithValue("@col8", col8);
                cmd.Parameters.AddWithValue("@col9", col9);
                cmd.Parameters.AddWithValue("@col10", col10);
                cmd.Parameters.AddWithValue("@col11", col11);
                cmd.Parameters.AddWithValue("@col12", col12);
                cmd.Parameters.AddWithValue("@col13", col13);
                cmd.Parameters.AddWithValue("@col14", col14);
                cmd.Parameters.AddWithValue("@col15", col15);
                // cmd.Parameters.AddWithValue("@col10", col10);
                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }


        public void InsertFixtures(string query, string col1, string col2, string col3, string col4, string col5)
        {
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@col1", col1);
                cmd.Parameters.AddWithValue("@col2", col2);
                cmd.Parameters.AddWithValue("@col3", col3);
                cmd.Parameters.AddWithValue("@col4", col4);
                cmd.Parameters.AddWithValue("@col5", col5);
              
                // cmd.Parameters.AddWithValue("@col10", col10);
                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }



        public void InsertBundesligaFixtures(string query, string col1, string col2, string col3, string col4,string col5)
        {
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@col1", col1);
                cmd.Parameters.AddWithValue("@col2", col2);
                cmd.Parameters.AddWithValue("@col3", col3);
                cmd.Parameters.AddWithValue("@col4", col4);
                cmd.Parameters.AddWithValue("@col5", col5);
      

                // cmd.Parameters.AddWithValue("@col10", col10);
                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }



        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM videos";

            //Create a list to store the result
            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader[0] + "");
                    list[1].Add(dataReader[1] + "");
                    list[2].Add(dataReader[2] + "");
                    list[3].Add(dataReader[3] + "");
                    list[4].Add(dataReader[4] + "");
                    list[5].Add(dataReader[5] + "");
                    list[6].Add(dataReader[6] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }




        //Select statement
        public List<string>[] SelectData(string query)
        {
//            string query = "SELECT * FROM videos";

            //Create a list to store the result
            List<string>[] list = new List<string>[7];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();
            list[3] = new List<string>();
            list[4] = new List<string>();
            list[5] = new List<string>();
            list[6] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader[0] + "");
                    list[1].Add(dataReader[1] + "");
                    list[2].Add(dataReader[2] + "");
                    list[3].Add(dataReader[3] + "");
                    list[4].Add(dataReader[4] + "");
                    list[5].Add(dataReader[5] + "");
                    list[6].Add(dataReader[6] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        public DataTable Select(string query)
        {
            DataTable dt = new DataTable();
           // DataSet ds = new DataSet();
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                //MySqlDataReader dataReader = cmd.ExecuteReader();
                MySqlDataAdapter DA = new MySqlDataAdapter(cmd);
                DA.Fill(dt);
        
             
                
                

                

              
              //  dt.Load(dataReader);

                
                //close Data Reader
                //dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return dt;
            }
            else
            {
                return dt;
            }


           

       
        }

        //Count statement
        public int Count()
        {

            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
        }

        //Restore
        public void Restore()
        {
        }
    }
}