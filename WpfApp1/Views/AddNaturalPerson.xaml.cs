using System.Data.OleDb;
using System.Windows;
using WpfApp1.Data;
using WpfApp1.Model;

namespace WpfApp1.Views
{
    /// <summary>
    /// Логика взаимодействия для AddNaturalPerson.xaml
    /// </summary>
    public partial class AddNaturalPerson : Window
    {
        List<Client> clients = new List<Client>();
        public AddNaturalPerson(List<Client> client)
        {
            InitializeComponent();

            this.clients=client;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var person = new Client();

            person.Id = clients.Last().Id + 1;
            person.Mail = box1.Text;
            person.Name = box2.Text;
            person.Pass = box3.Text;
            person.DayOf = box4.Text;
            person.Passport = box5.Text;
            person.Number = box6.Text;

            using (OleDbConnection myConnection = new OleDbConnection(ApplicationContext.connectString))
            {
                myConnection.Open();

                using (OleDbCommand command = new OleDbCommand("INSERT INTO users ([mail], [pass], [day_of], [passport], [number], [name]) VALUES (@Mail, @Pass, @DayOf, @Passport, @Number, @Name)", myConnection))
                {
                    //command.Parameters.AddWithValue("@Id", person.Id);
                    command.Parameters.AddWithValue("@Mail", person.Mail);
                    command.Parameters.AddWithValue("@Pass", person.Pass);
                    command.Parameters.AddWithValue("@DayOf", person.DayOf);
                    command.Parameters.AddWithValue("@Passport", person.Passport);
                    command.Parameters.AddWithValue("@Number", person.Number);
                    command.Parameters.AddWithValue("@Name", person.Name);
                    command.ExecuteNonQuery();
                }

                myConnection.Close();
            }

            MessageBox.Show("Клиент добавлен");
            this.Close();
        }
    }
}
