using System.Data.OleDb;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Data;
using WpfApp1.Model;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для DecorationOrder.xaml
    /// </summary>
    public partial class DecorationOrder : Window
    {
        List<string> faces = new List<string>() { "ЮЛ", "ФЛ" };
        List<Client> clients = new List<Client>();
        List<Order> orders = new List<Order>();
        List<LegalPerson> legalPerson = new List<LegalPerson>();
        List<ServicesOTK> servicesOTK = new List<ServicesOTK>();
        public string MyHintText { get; set; }

        public DecorationOrder()
        {
            InitializeComponent();

            using (OleDbConnection myConnection = new OleDbConnection(ApplicationContext.connectString))
            {
                myConnection.Open();

                using (OleDbCommand command = new OleDbCommand("SELECT * FROM users", myConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var client = new Client();
                            client.Id = reader.GetInt32(0);
                            client.Mail = reader.GetString(1);
                            client.Pass = reader.GetString(2);
                            client.DayOf = reader.GetString(3);
                            client.Passport = reader.GetString(4);
                            client.Number = reader.GetString(5);
                            client.Name = reader.GetString(6);

                            clients.Add(client);
                        }
                    }
                }

                using (OleDbCommand command = new OleDbCommand("SELECT * FROM legal_person", myConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var Person = new LegalPerson();
                            Person.Id = reader.GetInt32(0);
                            Person.CompanyName = reader.GetString(1);
                            Person.Inn = reader.GetString(2);
                            Person.Director = reader.GetString(3);
                            Person.ContactPerson = reader.GetString(4);
                            Person.Number = reader.GetString(5);
                            Person.Mail = reader.GetString(6);

                            legalPerson.Add(Person);
                        }
                    }
                }

                using (OleDbCommand command = new OleDbCommand("SELECT * FROM orders", myConnection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order();
                            order.Id = reader.GetInt32(0);
                            order.Date = reader.GetString(1);
                            order.ServicesId = reader.GetInt32(2);
                            order.Status = reader.GetString(3);
                            order.Salary = reader.GetString(4);
                            order.Number = reader.GetString(5);
                            order.UserId = reader.GetInt32(6);

                            orders.Add(order);
                        }
                    }
                }

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
            MyHintText = Convert.ToString(orders.Last().Id + 1);
            box1.ToolTip = new ToolTip { Content = MyHintText };
            cmb.ItemsSource = faces;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cmb.SelectedItem is not null && box1.Text is not null && box3.Text is not null)
            {
                if (cmb.SelectedItem == "ФЛ")
                {
                    var client = clients.Where(x => x.Name == box3.Text);
                    if (client.Count() == 0)
                    {
                        MessageBox.Show("Клиент не найден");
                        AddNaturalPerson addNaturalPerson = new AddNaturalPerson(clients);
                        addNaturalPerson.ShowDialog();
                    }
                    else
                    {
                        var order = new Order();

                        var service = servicesOTK.Find(x => x.Name == box5.Text);

                        if (service is not null)
                        {
                            order.Number = box1.Text;
                            order.UserId = clients.Find(x => x.Name == box3.Text).Id;
                            order.Status = "Новая";
                        }
                        else
                        {
                            MessageBox.Show("Такой услуги нет");
                        }

                    }
                }
                else
                {
                    var legalPersons = legalPerson.Where(x => x.Director == box3.Text).ToList();
                    if (legalPersons.Count() == 0)
                    {
                        MessageBox.Show("Клиент не найден");
                        AddLegalPerson addLegalPerson = new AddLegalPerson();
                        addLegalPerson.ShowDialog();
                    }
                    else
                    {
                        var order = new Order();

                        var service = servicesOTK.Find(x => x.Name == box5.Text);

                        if (service is not null)
                        {
                            order.Number = box1.Text;
                            order.UserId = legalPerson.Find(x => x.Director == box3.Text).Id;
                            order.Status = "Новая";
                        }
                        else
                        {
                            MessageBox.Show("Такой услуги нет");
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }

        private void box1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                box1.Text = MyHintText;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ServiceView serviceView = new ServiceView();
            serviceView.ShowDialog();
        }
    }
}
