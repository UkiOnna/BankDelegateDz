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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
     
        
        public MainWindow()
        {
            using (var context = new BankContext())
            {
                Person pers = new Person { Id = 1, Name = "Alex" };
                context.People.Add(pers);
            }
            InitializeComponent();
            Content = new SignInPage(this);


        }

       
    }
}
