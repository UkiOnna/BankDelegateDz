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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    
    public partial class MainPage : Page
    {
        public Account account;
        public User user;
        public MainPage(User user)
        {
            InitializeComponent();
            this.user = user;
            account = new Account(user.Sum);
            account.RegisterHandler(new Account.AccountStateHandler(AddMoney),new Account.AccountStateHandler(LossMoney));
        }

        private void AddMoneyClick(object sender, RoutedEventArgs e)
        {
            double result;
            if (double.TryParse(putBox.Text, out result))
            {
                account.Put(result);
            }
            else
            {
                MessageBox.Show("Неверные данные");
            }
        }

        private void LossMoneyClick(object sender, RoutedEventArgs e)
        {
            double result;
            if (double.TryParse(putBox.Text, out result))
            {
                account.Withdraw(result);
            }
            else
            {
                MessageBox.Show("Неверные данные");
            }
        }

        private static string AddMoney(double sum, Account personSum)
        {
            personSum.Sum += sum;
            using (var context = new BankContext())
            {
                //сделать чтобы контекст сохранялся с измененой суммой
            }
            return ($"Сумма {sum} добавлена на счет");
        }

        private static string LossMoney(double sum, Account personSum)
        {
            if (sum <= personSum.Sum)
            {
                personSum.Sum -= sum;

                using(var context=new BankContext())
                {
                    //сделать чтобы контекст сохранялся с измененой суммой
                }
                return ($"Сумма {sum} была снята со счета");
            }
            else
            {
                return ($"У вас недостаточно денег для совершении операции");

            }
        }
    }
}
