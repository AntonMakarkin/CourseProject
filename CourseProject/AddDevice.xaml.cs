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
    /// Логика взаимодействия для AddDevice.xaml
    /// </summary>
    public partial class AddDevice : Window
    {
        public AddDevice()
        {
            InitializeComponent();
            Add_Device();
        }

        private void ButtonCloseMain(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void Add_Device()
        {
            //выводим бренды
            string dbcon = @"Data Source = AddOrder.db; Version=3;";
            string query = "SELECT BrandName FROM Brands;";
            SQLiteConnection conn = new SQLiteConnection(dbcon);
            conn.Open();
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                BrandComboBox.Items.Add(reader["BrandName"]);
            }

            //выводим типы устройств
            string dbcon_2 = @"Data Source = AddOrder.db; Version=3;";
            string query_2 = "SELECT Name FROM Types;";
            SQLiteConnection conn_2 = new SQLiteConnection(dbcon_2);
            conn_2.Open();
            SQLiteCommand command_2 = new SQLiteCommand(query_2, conn_2);
            SQLiteDataReader reader_2 = command_2.ExecuteReader();

            while (reader_2.Read())
            {
                TypeDevice.Items.Add(reader_2["Name"]);
            }
        }

        private void AddNewDevice(object sender, RoutedEventArgs e)
        {
            if((BrandComboBox.Text == "") || (TypeDevice.Text == "") || (ModelDevice.Text ==""))
            {
                attention.Visibility = Visibility.Visible;
            }
            else
            {
                //ищем название бренда в соответствуюущей таблице
                attention.Visibility = Visibility.Hidden;
                string dbcon = @"Data Source = AddOrder.db; Version=3;";
                string query_search_brand = "SELECT BrandName FROM Brands WHERE BrandName = @BrandName;";
                string query_brand = "INSERT INTO Brands (BrandName) VALUES (@BrandName);";
                SQLiteConnection connection = new SQLiteConnection(dbcon);
                connection.Open();
                SQLiteCommand command_search = new SQLiteCommand(query_search_brand, connection);
                command_search.Parameters.Add("@BrandName", DbType.String).Value = BrandComboBox.Text;
                SQLiteDataAdapter da = new SQLiteDataAdapter(command_search);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0) //если есть, то не добавляем
                {
                    
                }
                else //в противном случае - добавляем
                {
                    SQLiteCommand command_brands = new SQLiteCommand(query_brand, connection);
                    command_brands.Connection = connection;
                    command_brands.Parameters.Add("@BrandName", DbType.String).Value = BrandComboBox.Text;
                    command_brands.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("Бренд добавлен");
                }


                //ищем название типа устройства в соответсвующей таблице
                string query_search_type = "SELECT Name FROM Types WHERE Name = @TypeName;";
                string query_type = "INSERT INTO Types (Name) VALUES (@Name);";
                SQLiteCommand command_search_types = new SQLiteCommand(query_search_type, connection);
                command_search_types.Parameters.Add("@TypeName", DbType.String).Value = TypeDevice.Text;
                SQLiteDataAdapter da_2 = new SQLiteDataAdapter(command_search_types);
                DataTable dt_2 = new DataTable();
                da_2.Fill(dt_2);

                if (dt.Rows.Count > 0) //если есть, то не добавляем
                {
                    
                }
                else //в противном случае - добавляем
                {
                    SQLiteCommand command_types = new SQLiteCommand(query_type, connection);
                    command_types.Connection = connection;
                    command_types.Parameters.Add("@BrandName", DbType.String).Value = BrandComboBox.Text;
                    command_types.ExecuteNonQuery();
                    System.Windows.Forms.MessageBox.Show("Тип добавлен");
                }


                //достаем id бренда, чтобы загрузить его в таблицу девайсов
                string query_search_brand_id = "SELECT id FROM Brands WHERE BrandName = @BrandName;";
                SQLiteCommand command_search_brand_id = new SQLiteCommand(query_search_brand_id, connection);
                command_search_brand_id.Parameters.Add("@BrandName", DbType.String).Value = BrandComboBox.Text;
                SQLiteDataReader reader_id = command_search_brand_id.ExecuteReader();
                reader_id.Read();
                int BrandID = reader_id.GetInt32(0);


                //достаем id типа устройства, чтобы загрузить его в таблицу девайсов
                string query_search_type_id = "SELECT id FROM Types WHERE Name = @TypeName;";
                SQLiteCommand command_search_type_id = new SQLiteCommand(query_search_type_id, connection);
                command_search_type_id.Parameters.Add("@TypeName", DbType.String).Value = TypeDevice.Text;
                SQLiteDataReader reader_id_type = command_search_type_id.ExecuteReader();
                reader_id_type.Read();
                int TypeID = reader_id_type.GetInt32(0);


                //достаем название модели устройства
                string Model = ModelDevice.Text;


                //добавляем устройство в таблицу устройств
                string query_device_search = "SELECT TypeId,BrandId,Model FROM Devices WHERE TypeId = @TypeId AND BrandID = @BrandID AND Model = @Model;";
                string query_device_Add = "INSERT INTO Devices (TypeId,BrandId,Model) VALUES (@TypeId,@BrandId,@Model);";
                SQLiteCommand command_search_device = new SQLiteCommand(query_device_search,connection);
                command_search_device.Parameters.Add("@TypeId", DbType.Int32).Value = TypeID;
                command_search_device.Parameters.Add("@BrandId", DbType.Int32).Value = BrandID;
                command_search_device.Parameters.Add("@Model", DbType.String).Value = Model;
                SQLiteDataAdapter da_device = new SQLiteDataAdapter(command_search_device);
                DataTable dt_device = new DataTable();
                da_device.Fill(dt_device);

                if(dt_device.Rows.Count > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Данное устройство уже есть в базе данных");
                }
                else
                {
                    SQLiteCommand command_device_add = new SQLiteCommand(query_device_Add, connection);
                    command_device_add.Parameters.Add("@TypeId", DbType.Int32).Value = TypeID;
                    command_device_add.Parameters.Add("@BrandId", DbType.Int32).Value = BrandID;
                    command_device_add.Parameters.Add("@Model", DbType.String).Value = Model;
                    command_device_add.ExecuteNonQuery();
                    connection.Close();
                    BrandComboBox.Text = "";
                    TypeDevice.Text = "";
                    ModelDevice.Clear();
                    System.Windows.Forms.MessageBox.Show("Устройство добавлено");
                }
                
            }
        }
    }
}
