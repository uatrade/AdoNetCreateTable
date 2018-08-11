using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConnectDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //string connects= @"server=DESKTOP-E8FFIHV;user id=sa;" + "password= 123456;initial catalog=Adonet";
        SqlConnection myConnection;
        SqlConnectionStringBuilder conn;
        string creategruppa2;
        SqlDataReader rdr = null;
        SqlCommand cmd;

        public MainWindow()
        {
            InitializeComponent();    
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
             myConnection = new SqlConnection();
             conn = new SqlConnectionStringBuilder();

            ConnetcionDB();
            myConnection.ConnectionString = conn.ConnectionString;

            creategruppa2 = @"Create Table gruppa3(ID INT not null Primary key, Name nvarchar(20) not null)";
 
                myConnection.Open();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            cmd = new SqlCommand("Select * From gruppa", myConnection);
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                infodb.Text+= rdr[0]+" "+rdr["Name"].ToString()+"\n";
            }

            myConnection.Close();
        }

        void ConnetcionDB()
        {
                conn.UserID = "sa";
                conn.Password = "123456";
                conn.InitialCatalog = "Adonet";
                conn.DataSource = "DESKTOP-E8FFIHV";
                //conn.ConnectTimeout = 100;
            
        }
    }
}
