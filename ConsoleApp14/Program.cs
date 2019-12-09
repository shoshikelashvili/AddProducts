using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseHelper;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;

namespace ConsoleApp14
{
    class Program
    {
        static void Main(string[] args)
        {
            string dataFile = @"C:\Users\ratis\Desktop\products.txt";
            string data = null;
            using (var fileReader = new StreamReader(new FileStream(dataFile, FileMode.Open)))
            {
                string line;
                while (!fileReader.EndOfStream)
                {
                    //data = File.ReadAllText(dataFile);
                    line = fileReader.ReadLine();
                    string[] words = line.Split("\t".ToCharArray());

                    Database db = new Database();
                    db.ExecuteNonQuery("InsertData_sp", CommandType.StoredProcedure,
                        new SqlParameter("Category", words[0]),
                        new SqlParameter("Id", words[1]),
                        new SqlParameter("Name", words[2]),
                        new SqlParameter("Price", words[3])
                    );
                }
            }
            Console.ReadKey();
        }
    }
}
