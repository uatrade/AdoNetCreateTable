using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

//ДЗ№1.2 Напишите код с использованием технологии ADO.NET, который создает в базе данных таблицу gruppa.

namespace CreateTableAdoNet
{
    class Program
    {

        static void Main(string[] args)
        {
            string creategruppa = @"Create Table gruppa4(ID INT not null Primary key, Name nvarchar(20) not null)";

            //DESKTOP-E8FFIHV
            SqlConnection conn = new SqlConnection();
            //conn.ConnectionString = @"Data Source=DESKTOP-E8FFIHV; Initial Catalog = Adonet; Integrated Security = SSPI; ";  //Для windows authentif
            conn.ConnectionString = @"server=DESKTOP-E8FFIHV;user id=sa;" + "password= 123456;initial catalog=Adonet"; //Для Sql login Password
            conn.Open();

            SqlCommand newtable = new SqlCommand(creategruppa, conn);
            newtable.ExecuteNonQuery();

            conn.Close();
        }
    }
}
