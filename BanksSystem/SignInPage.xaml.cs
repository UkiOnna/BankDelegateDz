using JsonFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        private Window window;
        private User user;
        public SignInPage(Window window)
        {
            InitializeComponent();
            this.window = window;

          
        }


        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            using (var context = new BankContext())
            {
                bool isOk = false;
               // UserService service = new UserService(context, new PasswordValidator());
                try
                {
                    for(int i = 0; i < context.Users.Count(); i++)
                    {
                        if (context.Users.ToList()[i].Login == loginBox.Text && context.Users.ToList()[i].Password == passwordBox.Password)
                        {
                            isOk = true;
                            user = context.Users.ToList()[i];
                        }
                    }
                   // User user = service.SignIn(loginBox.Text, passwordBox.Password);
                    if (isOk != false)
                    {
                        window.Content = new MainPage(user,window);
                    }
                    else
                    {
                        throw new Exception("Вы ввели неправильный логин и пароль");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void RegistrationButtonClick(object sender, RoutedEventArgs e)
        {
            window.Content = new SignUpPage(window);
           
        }

        private void loginBoxGotFocus(object sender, RoutedEventArgs e)
        {
            loginBox.Text = "";
        }

        private void passwordBoxGotFocus(object sender, RoutedEventArgs e)
        {
            passwordBox.Password = "";
        }
    }
}
