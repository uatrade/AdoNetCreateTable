using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Configuration;

namespace DZ_1_4_Selling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection myConnect;
        //string connectionString;
        SqlCommand cmd;
        SqlDataReader rdr = null;
        SqlDataReader rdrTable = null;
        string mytable;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (ListOfTables.Items.Count == 0)
            {
                try
                {
                    myConnect = new SqlConnection();

                    myConnect.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                    myConnect.Open();
                    cmd = new SqlCommand("Select TABLE_NAME From sells.INFORMATION_SCHEMA.TABLES", myConnect);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ListOfTables.Items.Add(rdr[0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    myConnect.Close();
                }
            }
        }

        private void ListOfTables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BaseInfo.Items.Clear();
            mytable = ListOfTables.SelectedItem.ToString();
            
        rdr = null;
            try
            {
                myConnect = new SqlConnection();

                myConnect.ConnectionString = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
                myConnect.Open();

                if (mytable == "Customers" || mytable == "Sellers")
                {
                    cmd = new SqlCommand("Select * From [sells].[dbo].[" + mytable + "]", myConnect);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        BaseInfo.Items.Add(rdr[0] + " " + rdr[1] + " " + rdr[2]);
                    }
                }
                if(mytable == "Selling")
                {
                    cmd = new SqlCommand("Select Customers.Name_Customer, Customers.LastName_Customer, Sellers.Name_Seller, Sellers.LastName_Seller, Selling.summa, Selling.dateofsell From [sells].[dbo].Customers, [sells].[dbo].Sellers, [sells].[dbo].Selling Where Selling.Id_Cusstomer = Customers.Id and Selling.Id_Seller = Sellers.Id", myConnect);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        BaseInfo.Items.Add("Покупатель"+" "+rdr[0] + "  " + rdr[1] + " Продавец: " + rdr[2] + " " + rdr[3] + " Сумма: " + rdr[4]+" Дата: "+rdr[5]);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                myConnect.Close();
            }
        }
    }
}

