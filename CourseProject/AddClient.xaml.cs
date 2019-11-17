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
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {

        public AddClient()
        {
            InitializeComponent();
            //this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void ButtonCloseMain(object sender, EventArgs e)
        {
            Close();
            /*Clients add = new Clients();
            add.ShowDialog();*/
           
        }

        private void Button_Click(object sender, EventArgs e)
        {
            //по клику на кнопку Добавить - добавляем клиента в соответствующую таблицу БД
            string dbcon = "Data Source = AddOrder.db; Version=3;";
            string query_search = "SELECT * FROM Clients WHERE Name = @Name AND Number = @Phone";
            string query = "INSERT INTO Clients (Name, Number) VALUES (@Name, @Phone)";
            SQLiteConnection connection = new SQLiteConnection(dbcon);
            connection.Open();
            SQLiteCommand command_search = new SQLiteCommand(query_search, connection);
            command_search.Parameters.Add("@Name", DbType.String).Value = FullName.Text;
            command_search.Parameters.Add("@Phone", DbType.String).Value = PhoneNumber.Text;
            SQLiteDataAdapter da = new SQLiteDataAdapter(command_search);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                System.Windows.Forms.MessageBox.Show("Клиент с данным именем и телефоном уже существует");
            }
            else
            {
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Connection = connection;
                command.Parameters.Add("@Name", DbType.String).Value = FullName.Text;
                command.Parameters.Add("@Phone", DbType.String).Value = PhoneNumber.Text;
                command.ExecuteNonQuery();
                connection.Close();
                System.Windows.Forms.MessageBox.Show("Клиент добавлен");
            }
            
        }
    }
}
