﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
    /// Логика взаимодействия для DecorationOrder.xaml
    /// </summary>
    public partial class DecorationOrder : Window
    {
        List<string> faces = new List<string>() {"ЮЛ", "ФЛ"};
        List<Client> clients = new List<Client>();
        List<Order> orders = new List<Order>();
        List<ServicesOTK> servicesOTK = new List<ServicesOTK>();
        string page = "";

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
            page = Convert.ToString(orders.Last().Id + 1);
            tool.Content = page;
            cmb.ItemsSource = faces;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(cmb.SelectedItem is not null && box1.Text is not null && box3.Text is not null)
            {
                if (cmb.SelectedItem == "ФЛ")
                {
                    var client = clients.Where(x => x.Name == box3.Text);
                    if (client.Count() == 0)
                    {
                        MessageBox.Show("Клиент не найден");
                        AddNaturalPerson addNaturalPerson = new AddNaturalPerson();
                        addNaturalPerson.ShowDialog();
                    }
                    else
                    {
                        var order = new Order();

                        var service = servicesOTK.Find(x => x.Name == box5.Text);

                        if(service is not null)
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

                }
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }
    }
}
