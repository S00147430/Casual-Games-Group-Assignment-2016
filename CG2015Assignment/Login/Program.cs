using ServerSide.Models;
using ServerSide.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login
{
    public class Program
    {
        static void Main(string[] args)
        {
            UsersDb db = new UsersDb();
            UsersViewModel uvm = new UsersViewModel();

            //UsersDb db = new UsersDb();
            string name = null, password = null;

            Console.Write("User Name:");
            name = Console.ReadLine();

            SqlCommand cmd1 = new SqlCommand("SELECT Name FROM tmpusername WHERE _id=1", db.UsersDatabase;

            SqlDataReader usernameRdr = null;

            usernameRdr = cmd1.ExecuteReader();

            while (usernameRdr.Read())
            {
                string username11 = usernameRdr["username11"].ToString();
            }

            //var connect = "">;
            //var sql = "SELECT UserName From Users WHERE UserId = @0";
            //using (var conn = new SqlConnection(""))
            //using (var cmd = new SqlCommand(sql, conn))
            //{
            //    cmd.Parameters.AddWithValue("@0", UsersViewModel["Name"]);
            //    conn.Open();
            //    name = Convert.ToString(cmd.ExecuteScalar());
            //}

            Console.Write("\nPassword:");
            password = Console.ReadLine();

            if (name == null || password == null)
            {
                Console.Write("Please fill in all the deatils.");
            }

            //ServerSide.Models.UsersDb
        }
    }
}
