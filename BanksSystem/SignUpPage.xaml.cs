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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BanksSystem
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        Window window;
        public SignUpPage(Window window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            using (var context = new BankContext())
            {
                try
                {
                    if (context.Users.Any(userr => userr.Login == loginBox.Text)) throw new Exception("Пользователь с таким логином уже сущетсвует");
                    if(loginBox.Text.ToList().Count<4) throw new Exception("Вы неправильно заполнили логин");
                    if (NameBox.Text.ToList().Count < 4) throw new Exception("Вы неправильно заполнили имя");
                    if (passwordBox.Text.ToList().Count < 4) throw new Exception("Вы неправильно заполнили пароль");

                    User user = new User { Login = loginBox.Text, Password = passwordBox.Text,Sum=0 };
                    Person person = new Person { Name = NameBox.Text };
                    context.Users.Add(user);
                    context.People.Add(person);
                    context.SaveChanges();
                    window.Content = new SignInPage(window);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }
    }
}
