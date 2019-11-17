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

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для RedactureClient.xaml
    /// </summary>
    public partial class RedactureClient : Window
    {
        public RedactureClient()
        {
            InitializeComponent();
        }

        private void ButtonCloseMain(object sender, EventArgs e)
        {
            Close();
        }
    }
}
