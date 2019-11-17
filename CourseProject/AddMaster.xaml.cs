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
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для AddMaster.xaml
    /// </summary>
    public partial class AddMaster : Window
    {
        public AddMaster()
        {
            InitializeComponent();
        }

        private void ButtonCloseMain(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddMasterClick(object sender, RoutedEventArgs e)
        {
            if(MasterTextBox.Text != "" && MasterPhoneTextBox.Text != "")
            {
                Attention.Visibility = Visibility.Hidden;
                string dbcon = "Data Source = AddOrder.db; Version=3;";
                string query_search = "SELECT * FROM Masters WHERE Name = @Name AND Phone = @Phone";
                string query = "INSERT INTO Masters (Name, Phone) VALUES (@Name, @Phone)";
                SQLiteConnection connection = new SQLiteConnection(dbcon);
                connection.Open();
                SQLiteCommand command_search = new SQLiteCommand(query_search, connection);
                command_search.Parameters.Add("@Name", DbType.String).Value = MasterTextBox.Text;
                command_search.Parameters.Add("@Phone", DbType.String).Value = MasterPhoneTextBox.Text;
                SQLiteDataAdapter da = new SQLiteDataAdapter(command_search);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    Attention2.Visibility = Visibility.Visible;
                }
                else
                {
                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    command.Connection = connection;
                    command.Parameters.Add("@Name", DbType.String).Value = MasterTextBox.Text;
                    command.Parameters.Add("@Phone", DbType.String).Value = MasterPhoneTextBox.Text;
                    command.ExecuteNonQuery();
                    connection.Close();
                    MasterTextBox.Clear();
                    MasterPhoneTextBox.Clear();
                    System.Windows.Forms.MessageBox.Show("Мастер добавлен");
                }
            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("Введите данные");
                Attention.Visibility = Visibility.Visible;
            }
            /*Window1 window = new Window1();
            string dbcon_1 = "Data Source = AddOrder.db; Version=3;";
            string query_search_1 = "SELECT Name,Phone FROM Masters";
            SQLiteConnection connection_1 = new SQLiteConnection(dbcon_1);
            connection_1.Open();
            SQLiteCommand command_search_1 = new SQLiteCommand(query_search_1, connection_1);
            SQLiteDataReader reader = command_search_1.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(reader);
            window.MasterDataGrid.ItemsSource = DT.DefaultView;*/
        }
    }
}
