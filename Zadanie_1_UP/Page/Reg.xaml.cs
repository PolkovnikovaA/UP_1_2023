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
using System.Net;

namespace Zadanie_1_UP
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Frame frame1;

        public Reg(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }

        private void Reg_Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Avtoriz(frame1));
        }

        private void Reg_Reg(object sender, RoutedEventArgs e)
        {
            string log = login.Text;
            string pas = password.Text;
            string pas1 = password_Copy.Text;
            if (log != "")
            {
                if (pas != "")
                {
                    if (pas1 != "")
                    {
                        if (pas == pas1)
                        {
                            List<Zadanie_1_UP.Users> user = new List<Zadanie_1_UP.Users>() { new Users() };
                            List<Zadanie_1_UP.Histori> h = new List<Zadanie_1_UP.Histori>() { new Histori() };
                            int count = Entities.GetContext().Users.Count();
                            int count_h = Entities.GetContext().Histori.Count();
                            user[0].id = count + 1;
                            user[0].login = log;
                            user[0].password = pas;
                            Entities.GetContext().Users.Add(user[0]);
                            h[0].id = count_h + 1;
                            h[0].login = log;
                            h[0].ip = Dns.GetHostName();
                            h[0].dataZ = DateTime.Now;
                            h[0].blok = DateTime.Now.AddMinutes(-30);
                            Entities.GetContext().Histori.Add(h[0]);
                            Entities.GetContext().Users.Add(user[0]);
                            Entities.GetContext().SaveChanges();
                            frame1.Navigate(new Avtoriz(frame1));
                        }
                        else
                        {
                            MessageBox.Show("Пароли не совпадают");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Повторите пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }

        }
    }
}
