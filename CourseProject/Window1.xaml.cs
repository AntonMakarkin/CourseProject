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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public void DataRefresh()
        {
            MainDataGrid.Items.Refresh();
        }

        private void ButtonCloseMain(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void CmdMinimizeMain_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void AddOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AddOrder add = new AddOrder();
            add.ShowDialog();

        }

        private void AddClient_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            Master.Visibility = Visibility.Collapsed;
            Devices.Visibility = Visibility.Collapsed;
            Clients.Visibility = Visibility.Visible;
            Work.Visibility = Visibility.Collapsed;
            string dbcon = "Data Source = AddOrder.db; Version = 3";
            string query = "SELECT Name,Phone FROM Clients";
            SQLiteConnection connection = new SQLiteConnection(dbcon);
            connection.Open();
            SQLiteCommand select_command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = select_command.ExecuteReader();
            DataTable dt = new DataTable(); //Создаем новый DataTable
            dt.Load(reader); //Загружаем DataReader в DataTable
            ClientsDataGrid.ItemsSource = dt.DefaultView;
        }

        private void Window_Loaded(object sender, EventArgs e)
        {
            string dbcon = "Data Source = AddOrder.db; Version = 3";
            string query = "SELECT Name,Phone FROM Clients";
            SQLiteConnection connection = new SQLiteConnection(dbcon);
            connection.Open();
            SQLiteCommand select_command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = select_command.ExecuteReader();
            DataTable dt = new DataTable(); //Создаем новый DataTable
            dt.Load(reader); //Загружаем DataReader в DataTable
            MainDataGrid.ItemsSource = dt.DefaultView;
        }

        private void Masters_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            Clients.Visibility = Visibility.Collapsed;
            Devices.Visibility = Visibility.Collapsed;
            Master.Visibility = Visibility.Visible;
            Work.Visibility = Visibility.Collapsed;
            string dbcon = "Data Source = AddOrder.db; Version = 3";
            string query = "SELECT Name,Phone,IDNumber FROM Masters";
            SQLiteConnection connection = new SQLiteConnection(dbcon);
            connection.Open();
            SQLiteCommand select_command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = select_command.ExecuteReader();
            DataTable dt = new DataTable(); //Создаем новый DataTable
            dt.Load(reader); //Загружаем DataReader в DataTable
            MasterDataGrid.ItemsSource = dt.DefaultView;
        }

        private void AddClientClick(object sender, RoutedEventArgs e)
        {
            AddClient add = new AddClient();
            add.ShowDialog();
        }

        private void AddMasterClick(object sender, RoutedEventArgs e)
        {
            AddMaster add = new AddMaster();
            add.ShowDialog();
        }

        private void AddDeviceClick(object sender, RoutedEventArgs e)
        {
            AddDevice add = new AddDevice();
            add.ShowDialog();
        }

        private void AddWorkClick(object sender, RoutedEventArgs e)
        {
            AddWork add = new AddWork();
            add.ShowDialog();
        }

        private void Works_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Work.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
            Clients.Visibility = Visibility.Collapsed;
            Master.Visibility = Visibility.Collapsed;
            Devices.Visibility = Visibility.Collapsed;
            string dbcon = "Data Source = AddOrder.db; Version = 3";
            string query = "SELECT Works.Name, Works.Price, Works.DeviceID, Devices.id, Devices.Model FROM Works, Devices WHERE DeviceID = Devices.id;";
            SQLiteConnection connection = new SQLiteConnection(dbcon);
            connection.Open();
            SQLiteCommand select_command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = select_command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            WorksDataGrid.ItemsSource = dt.DefaultView;
        }

        private void Devices_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            Clients.Visibility = Visibility.Collapsed;
            Master.Visibility = Visibility.Collapsed;
            Devices.Visibility = Visibility.Visible;
            Work.Visibility = Visibility.Collapsed;
            string dbcon = "Data Source = AddOrder.db; Version = 3";
            //string query = "SELECT Тип,Бренд,Модель FROM Devices";
            string query = "SELECT Types.Name, Brands.BrandName, Devices.Model From Brands INNER JOIN(Types INNER JOIN Devices ON Types.id = Devices.TypeId) ON Brands.id = Devices.BrandId";
            SQLiteConnection connection = new SQLiteConnection(dbcon);
            connection.Open();
            SQLiteCommand select_command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = select_command.ExecuteReader();
            DataTable dt = new DataTable(); //Создаем новый DataTable
            dt.Load(reader); //Загружаем DataReader в DataTable
            DevicesDataGrid.ItemsSource = dt.DefaultView;
        }

        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            //MasterDataGrid.Items.Refresh();
            string dbcon_1 = "Data Source = AddOrder.db; Version=3;";
            string query_search_1 = "SELECT Name,Phone,IDNumber FROM Masters";
            SQLiteConnection connection_1 = new SQLiteConnection(dbcon_1);
            connection_1.Open();
            SQLiteCommand command_search_1 = new SQLiteCommand(query_search_1, connection_1);
            SQLiteDataReader reader = command_search_1.ExecuteReader();
            DataTable DT = new DataTable();
            DT.Load(reader);
            MasterDataGrid.ItemsSource = DT.DefaultView;
        }
    }
}
