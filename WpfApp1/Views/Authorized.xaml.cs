using System.Data.OleDb;
using System.Windows;
using WpfApp1.Data;
using WpfApp1.Model;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для Authorized.xaml
    /// </summary>
    public partial class Authorized : Window
    {
        List<User> users = new List<User>();

        public Authorized()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (OleDbConnection myConnection = new OleDbConnection(ApplicationContext.connectString))
            {
                myConnection.Open();

                using (OleDbCommand command = new OleDbCommand("SELECT * FROM auth", myConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User();
                            user.Id = reader.GetInt32(0);
                            user.Login = reader.GetString(1);
                            user.Password = reader.GetString(2);
                            users.Add(user);
                        }
                    }
                }

                myConnection.Close();
            }

            var userr = users.Where(x => x.Login == box1.Text && x.Password == box2.Text);

            if(userr.Count() == 0)
            {
                MessageBox.Show("Пользователь не найден");
            }
            else
            {
                DecorationOrder decorationOrder = new DecorationOrder();
                decorationOrder.Show();
                this.Close();
            }
            //using (OleDbCommand command = new OleDbCommand("INSERT INTO YourTable (Column1, Column2) VALUES (@Value1, @Value2)", connection))
            //{
            //    command.Parameters.AddWithValue("@Value1", value1);
            //    command.Parameters.AddWithValue("@Value2", value2);

            //    command.ExecuteNonQuery();
            //}
        }
    }
}
