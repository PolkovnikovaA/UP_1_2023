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

namespace Zadanie_1_UP
{
    /// <summary>
    /// Логика взаимодействия для Avtoriz.xaml
    /// </summary>
    public partial class Avtoriz : Page
    {
        public Frame frame1;
        public int vx = 0;

        public Avtoriz(Frame frame)
        {
            frame1 = frame;
            InitializeComponent();
        }

        List<Zadanie_1_UP.Users> users = new List<Zadanie_1_UP.Users>();

        private void Avtoriz_Vxod(object sender, MouseButtonEventArgs e)
        {
            string klients = login.Text;
            string pas = password.Password;
            int count = Entities.GetContext().Users.Count();
            users = Entities.GetContext().Users.ToList();
            for (int i = 0; i < count; i++)
            {
                if (users[i].login == klients)
                {
                    if (users[i].password == pas)
                    {
                        frame1.Navigate(new Glavnaya(frame1));
                        vx = 1;
                        break;
                    }
                }
            }
            if (vx == 0)
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void Avtoriz_Zareg(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Reg(frame1));
        }

    }
}
