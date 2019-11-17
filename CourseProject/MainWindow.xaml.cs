using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void cmdMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        readonly string dbcon = @"Data Source = UserData.db; Version=3;";

        private void LoginClick(object sender, EventArgs e)
        {
            if((Login.Text.Trim() == "") || (Password.Password.Trim() == ""))
            {
                attention.Visibility = Visibility.Visible;
            }
            else
            {
                string query = "SELECT * FROM `Authorisation` WHERE `Login` = @uL AND `Password` = @uP";
                SQLiteConnection conn = new SQLiteConnection(dbcon);
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@ul",Login.Text);
                cmd.Parameters.AddWithValue("@uP", Password.Password);
                SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if(dt.Rows.Count > 0)
                {
                    //System.Windows.Forms.MessageBox.Show("Вы вошли")
                    Hide();
                    Window1 window = new Window1();
                    window.Show();

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Вы не вошли");
                }
            }
        }

        private void ButtonClose(object sender, EventArgs e)
        {
            Close();
        }

    }
}
