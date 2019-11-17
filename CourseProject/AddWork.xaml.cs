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
    /// Логика взаимодействия для AddWork.xaml
    /// </summary>
    public partial class AddWork : Window
    {
        public AddWork()
        {
            InitializeComponent();
            Add_Device();
        }

        void Add_Device()
        {
            //выводим модели устройств в combobox
            string dbcon = @"Data Source = AddOrder.db; Version=3;";
            string query = "SELECT Model FROM Devices;";
            SQLiteConnection connection_models = new SQLiteConnection(dbcon);
            connection_models.Open();
            SQLiteCommand query_models = new SQLiteCommand(query, connection_models);
            SQLiteDataReader reader_models = query_models.ExecuteReader();

            while(reader_models.Read())
            {
                ModelDevice.Items.Add(reader_models["Model"]);
            }
            reader_models.Close();
        }

        private void ButtonCloseMain(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Add_Work(object sender, RoutedEventArgs e)
        {
            string dbcon = @"Data Source = AddOrder.db; Version=3;";
            string query_select_DeviceID = "SELECT id FROM Devices WHERE Model = @ModelName";
            string query_search_work = "SELECT Name, Price, DeviceID FROM Works WHERE Name = @NameSelect AND Price = @PriceSelect AND DeviceID = @DeviceIDSelect;";
            string query_insert_work = "INSERT INTO Works (Name, Price, DeviceID) VALUES (@Name, @Price, @DeviceID);";
            SQLiteConnection connection_works = new SQLiteConnection(dbcon);
            connection_works.Open();

            SQLiteCommand extract_DeviceID = new SQLiteCommand(query_select_DeviceID, connection_works);
            string Model = ModelDevice.SelectedValue.ToString();
            extract_DeviceID.Parameters.Add("@ModelName", DbType.String).Value = Model;
            SQLiteDataReader deviceIDreader = extract_DeviceID.ExecuteReader();
            deviceIDreader.Read();
            int DeviceID = deviceIDreader.GetInt32(0);

            SQLiteCommand query_search_work_command = new SQLiteCommand(query_search_work, connection_works);
            query_search_work_command.Parameters.Add("@NameSelect", DbType.String).Value = WorkTextBox.Text;
            string price = Price.Text;
            int number_price = int.Parse(price);
            query_search_work_command.Parameters.Add("@PriceSelect", DbType.String).Value = number_price;
            query_search_work_command.Parameters.Add("@DeviceIDSelect", DbType.Int32).Value = DeviceID;
            SQLiteDataAdapter workDA = new SQLiteDataAdapter(query_search_work_command);
            DataTable workDT = new DataTable();
            workDA.Fill(workDT);

            if (workDT.Rows.Count > 0)
            {
                System.Windows.Forms.MessageBox.Show("Данное услуга уже существует");
            }
            else
            {
                SQLiteCommand query_insert_work_command = new SQLiteCommand(query_insert_work, connection_works);
                query_insert_work_command.Parameters.Add("@Name", DbType.String).Value = WorkTextBox.Text;
                query_insert_work_command.Parameters.Add("@Price", DbType.String).Value = number_price; 
                query_insert_work_command.Parameters.Add("@DeviceID", DbType.Int32).Value = DeviceID;
                query_insert_work_command.ExecuteNonQuery();
                System.Windows.Forms.MessageBox.Show("Услуга добавлена");
            }
        }

    }
}
