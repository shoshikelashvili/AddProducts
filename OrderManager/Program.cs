using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.IO;
using System.Data.SqlClient;
using System.Data;


namespace OrderManager
{
    class Program
    {
        static void Main(string[] args)
        {
            bool fileReady; string data = null;
            Console.WriteLine("Please Specify the path of the file you'd like to read");

            do
            {
                try
                {
                    string filepath = Console.ReadLine();
                    data = File.ReadAllText(filepath);
                    fileReady = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error occured: " + e.Message + "\nPlease try again");
                    fileReady = false;
                }
            }
            while (fileReady != true);

            using (StringReader reader = new StringReader(data))
            {
                string singleLine;
                while((singleLine = reader.ReadLine()) != null)
                {
                    string[] words  = singleLine.Split("\t".ToCharArray());

                    Database dbhelper = new Database(@"Data Source=DESKTOP-VU3SH3M\SQLEXPRESS; database = G02; integrated security = true;");
                    dbhelper.ExecuteNonQuery($"InsertData_sp", CommandType.StoredProcedure, new SqlParameter("Category", words[0]), new SqlParameter("Id", words[1]), new SqlParameter("Name", words[2]), new SqlParameter("Price", words[3]));
                }
            }
            Console.WriteLine("Data added succesfully!");
            Console.ReadKey();
            //TODO add check for data format
        }
    }
}
