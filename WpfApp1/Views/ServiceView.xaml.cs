using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
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
using WpfApp1.Data;
using WpfApp1.Model;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для ServiceView.xaml
    /// </summary>
    public partial class ServiceView : Window
    {
        List<ServicesOTK> servicesOTK = new List<ServicesOTK>();
        public ServiceView()
        {
            InitializeComponent();

            using (OleDbConnection myConnection = new OleDbConnection(ApplicationContext.connectString))
            {
                myConnection.Open();
                using (OleDbCommand command = new OleDbCommand("SELECT * FROM servicesOTK", myConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new ServicesOTK();
                            order.Id = reader.GetInt32(0);
                            order.Name = reader.GetString(1);
                            order.Salary = reader.GetString(2);
                            order.ServiceCode = reader.GetString(3);
                            order.Term = reader.GetString(4);

                            servicesOTK.Add(order);
                        }
                    }
                }
                myConnection.Close();
            }

            dtg.ItemsSource = servicesOTK;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string path = @"D:\trash\123.txt";
            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true});
        }
    }
}
