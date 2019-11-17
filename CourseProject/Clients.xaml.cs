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
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        public Clients()
        {
            InitializeComponent();
        }

        private void ButtonCloseMain(object sender, EventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string dbcon = "Data Source = AddOrder.db; Version = 3";
            string query = "SELECT * FROM Clients";
            SQLiteConnection connection = new SQLiteConnection(dbcon);
            connection.Open();
            SQLiteCommand select_command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = select_command.ExecuteReader();
            DataTable dt = new DataTable(); //Создаем новый DataTable
            dt.Load(reader); //Загружаем DataReader в DataTable
            MainDataGrid.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddClient add = new AddClient();
            add.ShowDialog();
        }

        private void Button_Click_Change(object sender, RoutedEventArgs e)
        {
            //MainDataGrid.IsReadOnly = false;
            //string update_table = "UPDATE Clients SET";
            //string Name, Number;

            //RedactureClient add = new RedactureClient();
            //add.Show();
            //int index = MainDataGrid.CurrentItem.Count;
            //int index = MainDataGrid.Items.IndexOf(MainDataGrid.CurrentItem);
            //Name = MainDataGrid.



        }
    }
}
