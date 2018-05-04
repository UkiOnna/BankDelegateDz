using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace BanksSystem
{
    public class Account
    {
        public double Sum { get; set; } // Переменная для хранения суммы

        public delegate string AccountStateHandler(double x,Account acc,User u);

        private AccountStateHandler delWithDraw;
        private AccountStateHandler delPut;
        public Account(double sum)
        {
            Sum = sum;
        }

        public void RegisterHandler(AccountStateHandler delP, AccountStateHandler delW)
        {
            delPut = delP;
            delWithDraw = delW;

        }

        public void Put(double sum,User user)
        {
            delPut.BeginInvoke(sum, this,user, new AsyncCallback(asyncResult => MessageBox.Show(delPut.EndInvoke(asyncResult))), null);
            
            
        }

        public void Withdraw(double sum,User user)
        {
            IAsyncResult result =  delWithDraw.BeginInvoke(sum, this,user, new AsyncCallback(asyncResult => MessageBox.Show(delWithDraw.EndInvoke(asyncResult))), null);
        }
    }
}
