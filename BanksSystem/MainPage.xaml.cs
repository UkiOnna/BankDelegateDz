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
            moneyUser.Text = user.Sum.ToString();
            account = new Account(user.Sum);
            account.RegisterHandler(new Account.AccountStateHandler(AddMoney),new Account.AccountStateHandler(LossMoney));
        }

        private void AddMoneyClick(object sender, RoutedEventArgs e)
        {
            double result;
            double money;
            if (double.TryParse(putBox.Text, out result))
            {
                account.Put(result, user);
                money = double.Parse(moneyUser.Text);
                money = money + result;
                moneyUser.Text = money.ToString();
            }
            else
            {
                MessageBox.Show("Неверные данные");
            }

        }

        private void LossMoneyClick(object sender, RoutedEventArgs e)
        {
            double result;
            double money;
            if (double.TryParse(lostBox.Text, out result))
            {
                account.Withdraw(result,user);
                money = double.Parse(moneyUser.Text);
                money = money - result;
                moneyUser.Text = money.ToString();
            }
            else
            {
                MessageBox.Show("Неверные данные");
            }
       
        }

        private static string AddMoney(double sum, Account personSum,User user)
        {
            personSum.Sum += sum;
            
            using (var context = new BankContext())
            {
                //сделать чтобы контекст сохранялся с измененой суммой
                for(int i = 0; i < context.Users.Count(); i++)
                {
                    if (context.Users.ToList()[i].Id == user.Id)
                    {
                        context.Users.ToList()[i].Sum = personSum.Sum;
                    }
                }
                context.SaveChanges();
            }
            return ($"Сумма {sum} добавлена на счет");
        }

        private static string LossMoney(double sum, Account personSum,User user)
        {
            if (sum <= personSum.Sum)
            {
                personSum.Sum -= sum;

                using(var context=new BankContext())
                {
                    //сделать чтобы контекст сохранялся с измененой суммой
                    for (int i = 0; i < context.Users.Count(); i++)
                    {
                        if (context.Users.ToList()[i].Id == user.Id)
                        {
                            context.Users.ToList()[i].Sum = personSum.Sum;
                        }
                    }
                    context.SaveChanges();
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
