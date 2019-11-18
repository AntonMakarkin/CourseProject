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
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using System.Configuration;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
            Add_Order();
        }

       

        void Add_Order()
        {
            //Добавляем имена клиентов в combobox
            string dbcon = @"Data Source = AddOrder.db; Version=3;";
            string query = "SELECT Name,Phone FROM Clients ORDER BY Name ASC;";
            SQLiteConnection conn = new SQLiteConnection(dbcon);
            conn.Open();
            SQLiteCommand command = new SQLiteCommand(query, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                ClientComboBox.Items.Add(reader["Name"]);
            }
            reader.Close();


            //Добавляем названия брендов в combobox
            string brand_query = "SELECT * FROM Brands;";
            SQLiteConnection connection = new SQLiteConnection(dbcon);
            connection.Open();
            SQLiteCommand select_brand = new SQLiteCommand(brand_query, connection);
            SQLiteDataAdapter brandDA = new SQLiteDataAdapter(select_brand);
            DataTable BrandDT = new DataTable();
            brandDA.Fill(BrandDT);
            Brand.SelectedValuePath = "id";
            Brand.DisplayMemberPath = "Name";
            Brand.ItemsSource = BrandDT.DefaultView;
            TypeOfDevice.IsEnabled = false;
            Model.IsEnabled = false;

            //Добавляем мастеров в combobox
            string master_query = "SELECT Name FROM Masters;";
            SQLiteCommand master_command = new SQLiteCommand(master_query, conn);
            SQLiteDataReader master_reader = master_command.ExecuteReader();

            while (master_reader.Read())
            {
                Master.Items.Add(master_reader["Name"]);
            }
            master_reader.Close();
            conn.Close();
        }


        private void ButtonCloseMain(object sender, EventArgs e)
        {
            Close();
        }

        public void Brand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Добавляем названия типов в combobox
            /*TypeOfDevice.IsEnabled = false; //разблокируем combobox типов
            Model.IsEnabled = false; //combobox моделей остается заблокирован
            TypeOfDevice.Text = "";
            TypeOfDevice.Items.Clear();
            string dbcon = @"Data Source = AddOrder.db; Version=3;"; //путь к бд
            string type_query_1 = "SELECT DISTINCT Devices.TypeId, Devices.BrandId, Types.id, Types.Name FROM Devices, Types WHERE Devices.TypeId = Types.id AND Devices.BrandID = @BrandID";
            string search_id_brands = "SELECT id FROM Brands WHERE BrandName = @BrandName;";
            string BrandName = Brand.SelectedValue.ToString();
            //string type_query = "SELECT Name FROM Types;"; //запрос для извлечения типов устройства
            SQLiteConnection conn = new SQLiteConnection(dbcon); //подсоединяемся к бд
            conn.Open(); //открываем соединение

            SQLiteCommand search_id_brands_command = new SQLiteCommand(search_id_brands, conn);
            search_id_brands_command.Parameters.Add("@BrandName", DbType.String).Value = BrandName;
            SQLiteDataReader brands_id_reader = search_id_brands_command.ExecuteReader();
            brands_id_reader.Read();
            int BrandID = brands_id_reader.GetInt32(0);

            SQLiteCommand type_command = new SQLiteCommand(type_query_1, conn);
            type_command.Parameters.Add("@BrandID", DbType.Int32).Value = BrandID;
            SQLiteDataAdapter type_DA = new SQLiteDataAdapter(type_command);
            DataTable typeDT = new DataTable();
            type_DA.Fill(typeDT);
            if (typeDT.Rows.Count > 0)
            {
                
                SQLiteDataReader type_reader = type_command.ExecuteReader();
                //Model.Items.Clear();
                while (type_reader.Read()) //пока считываем таблицу
                {
                    TypeOfDevice.Items.Add(type_reader["Name"]); //добаляем найденные строчки
                    TypeOfDevice.IsEnabled = true;
                    //Model.IsEnabled = false;
                }
                type_reader.Close(); //заканчиваем прочтение
                TypeOfDevice.IsEnabled = true;
            }
            else
            {

            }
            //conn.Close();*/
            if(Brand.SelectedValue.ToString() != null)
            {
                string dbcon = @"Data Source = AddOrder.db; Version=3;";
                SQLiteConnection conn = new SQLiteConnection(dbcon);
                //string type_query_1 = "SELECT DISTINCT Devices.TypeId, Devices.BrandId, Types.id, Types.Name FROM Devices, Types WHERE Devices.TypeId = Types.id AND Devices.BrandID = @BrandID";
                string type_query_1 = "SELECT DISTINCT BrandID, Name FROM Types WHERE BrandID = @ID;";
                SQLiteCommand type_query_command = new SQLiteCommand(type_query_1, conn);
                conn.Open();

                type_query_command.Parameters.AddWithValue("@ID", Brand.SelectedValue.ToString());
                SQLiteDataAdapter TypeDA = new SQLiteDataAdapter(type_query_command);
                DataTable TypeDT = new DataTable();
                TypeDA.Fill(TypeDT);
                TypeOfDevice.SelectedValuePath = "BrandID";
                TypeOfDevice.DisplayMemberPath = "Name";
                TypeOfDevice.ItemsSource = TypeDT.DefaultView;
                TypeOfDevice.IsEnabled = true;
                Model.IsEnabled = false;
            }

        }

        private void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            /*Model.IsEnabled = false;
            //Добаляем названия моделей в combobox на основе бренда и тип
            string dbcon = @"Data Source = AddOrder.db; Version=3;";
            SQLiteConnection conn = new SQLiteConnection(dbcon);
            conn.Open();
            string brand_id_search = "SELECT id FROM Brands WHERE BrandName = @BrandName;";
            SQLiteCommand brand_id_search_command = new SQLiteCommand(brand_id_search, conn);
            brand_id_search_command.Parameters.Add("@BrandName", DbType.String).Value = Brand.Text;
            SQLiteDataReader brand_id_reader = brand_id_search_command.ExecuteReader();
            brand_id_reader.Read();
            int BrandID = brand_id_reader.GetInt32(0);

            string type_id_search = "SELECT id FROM Types WHERE Name = @Name;";
            SQLiteCommand type_id_search_command = new SQLiteCommand(type_id_search, conn);
            //string Type = TypeOfDevice.SelectedValue.ToString();
            string Type = TypeOfDevice.Text;
            type_id_search_command.Parameters.Add("@Name", DbType.String).Value = Type;
            SQLiteDataReader type_id_reader = type_id_search_command.ExecuteReader();
            type_id_reader.Read();
            int TypeID = type_id_reader.GetInt32(0);

            string model_search = "SELECT Model FROM Devices WHERE TypeId = @TypeID AND BrandId = @BrandID";
            SQLiteCommand model_search_command = new SQLiteCommand(model_search, conn);
            model_search_command.Parameters.Add("@TypeID", DbType.Int32).Value = TypeID;
            model_search_command.Parameters.Add("@BrandID", DbType.Int32).Value = BrandID;
            SQLiteDataAdapter model_DA = new SQLiteDataAdapter(model_search_command);
            DataTable model_DT = new DataTable();
            model_DA.Fill(model_DT);
            if (model_DT.Rows.Count > 0)
            {
                Model.Items.Clear();
                SQLiteDataReader model_reader = model_search_command.ExecuteReader();
                while (model_reader.Read())
                {
                    Model.Items.Add(model_reader["Model"]);
                }
                Model.IsEnabled = true;
            }
            else
            {
                conn.Close();
                System.Windows.Forms.MessageBox.Show("Нет моделей");
                Model.Text = "";
                Model.IsEnabled = false;
            }
            //conn.Close();*/
            if (TypeOfDevice.SelectedValue.ToString() != null)
            {
                string dbcon = @"Data Source = AddOrder.db; Version=3;";
                SQLiteConnection conn = new SQLiteConnection(dbcon);
                //string model_search = "SELECT * FROM Devices WHERE TypeId = @TypeID AND BrandId = @BrandID";
                string model_search = "SELECT * FROM Models WHERE TypeID = @id;";
                SQLiteCommand model_search_command = new SQLiteCommand(model_search, conn);
                conn.Open();

                model_search_command.Parameters.AddWithValue("@id", TypeOfDevice.SelectedValue.ToString());
                SQLiteDataAdapter ModelDA = new SQLiteDataAdapter(model_search_command);
                DataTable ModelDT = new DataTable();
                ModelDA.Fill(ModelDT);
                Model.SelectedValuePath = "TypeID";
                Model.DisplayMemberPath = "ModelName";
                Model.ItemsSource = ModelDT.DefaultView;
                //Brand.IsEnabled = false;
                Model.IsEnabled = true;
                Work.IsEnabled = false;
            }
        }

        private void Model_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*Work.IsEnabled = false;
            //добавляем доступные услуги в соотвествии с моделями
            Work.IsEnabled = false;
            string dbcon = @"Data Source = AddOrder.db; Version=3;";
            SQLiteConnection conn = new SQLiteConnection(dbcon);
            conn.Open();
            string device_id_search = "SELECT id FROM Devices WHERE Model = @ModelName;";
            SQLiteCommand device_id_search_command = new SQLiteCommand(device_id_search, conn);
            string ModelDevice = Model.SelectedValue.ToString();
            device_id_search_command.Parameters.Add("@ModelName", DbType.String).Value = ModelDevice;
            SQLiteDataReader device_id_reader = device_id_search_command.ExecuteReader();
            device_id_reader.Read();
            int DeviceID = device_id_reader.GetInt32(0);

            string work_search = "SELECT Name, DeviceID FROM Works WHERE DeviceID = @DeviceID;";
            SQLiteCommand work_search_command = new SQLiteCommand(work_search, conn);
            work_search_command.Parameters.Add("@DeviceID", DbType.Int32).Value = DeviceID;
            SQLiteDataAdapter workDA = new SQLiteDataAdapter(work_search_command);
            DataTable workDT = new DataTable();
            workDA.Fill(workDT);
            if (workDT.Rows.Count > 0)
            {
                SQLiteDataReader work_reader = work_search_command.ExecuteReader();
                Work.Items.Clear();
                while (work_reader.Read())
                {
                    Work.Items.Add(work_reader["Name"]);
                }
                Work.IsEnabled = true;
            }
            else
            {
                conn.Close();
                System.Windows.Forms.MessageBox.Show("Нет услуг");
                Work.Text = "";
                Work.IsEnabled = false;
            }*/

        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            if(ClientComboBox.Text == "" || Brand.Text == "" || TypeOfDevice.Text == "" || Model.Text == "" || Master.Text == "" || Work.Text == "")
            {
                attention.Visibility = Visibility.Visible;
            }
            else
            {

            }
        }

        private void ClearOrder_Click(object sender, RoutedEventArgs e)
        {
            /*Brand.Text = "";
            TypeOfDevice.Text = "";
            Model.Text = "";
            Work.Text = "";
            Brand.Items.Clear();
            TypeOfDevice.Items.Clear();
            Model.Items.Clear();
            Work.Items.Clear();*/
            Brand.IsEditable = true;
            Brand.Text = "";
            TypeOfDevice.IsEditable = true;
            TypeOfDevice.Text = "";
            TypeOfDevice.IsEditable = false;
            Model.IsEditable = true;
            Model.Text = "";
            Model.IsEditable = false;
        }

    }
}
