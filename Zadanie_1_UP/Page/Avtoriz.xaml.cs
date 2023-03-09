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
    /// Логика взаимодействия для Avtoriz.xaml
    /// </summary>
    public partial class Avtoriz : Page
    {
        public Frame frame1;
        public int vx = 0;
        int captcha = 0;

        DateTime date;

        public Avtoriz(Frame frame)
        {
            frame1 = frame;
            InitializeComponent();
            date = DateTime.Now;
        }

        public void vxod()
        {
            string user = login.Text;
            string pas = password.Password;
            int count = Entities.GetContext().Users.Count();
            int count_hh = Entities.GetContext().Histori.Count();
            int count_w = Entities.GetContext().Workers.Count();
            worker = Entities.GetContext().Workers.ToList();
            Users = Entities.GetContext().Users.ToList();
            historys = Entities.GetContext().Histori.ToList();
            for (int i = 0; i < count_w; i++)
            {
                if (worker[i].login == user)
                {
                    if (worker[i].password == pas)
                    {
                        for (int j = count_hh - 1; j >= 0; j--)
                        {
                            if (historys[j].login == user)
                            {
                                DateTime t = DateTime.Now;
                                if (historys[j].blok != null)
                                {
                                    t = (DateTime)historys[j].blok;
                                    t = t.AddMinutes(30);
                                }
                                if (DateTime.Now < historys[j].blok || t <= DateTime.Now)
                                {
                                    vx = 1;
                                    int count_h = Entities.GetContext().Histori.Count();
                                    history_login[0].id = count_h + 1;
                                    history_login[0].login = user;
                                    history_login[0].dataZ = DateTime.Now;
                                    history_login[0].ip = Dns.GetHostName();
                                    if (historys[j].blok < DateTime.Now)
                                    {
                                        history_login[0].blok = date.AddHours(2.5);
                                    }
                                    else
                                    {
                                        history_login[0].blok = historys[j].blok;
                                    }
                                    Entities.GetContext().Histori.Add(history_login[0]);
                                    Entities.GetContext().SaveChanges();
                                    frame1.Navigate(new Glavnaya(worker[i].login, frame1));
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("Перерыв пол часа");
                                    vx = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < count; i++)
            {
                if (Users[i].login == user)
                {
                    if (Users[i].password == pas)
                    {
                        for (int j = count_hh - 1; j >= 0; j--)
                        {
                            if (historys[j].login == user)
                            {
                                DateTime t = DateTime.Now;
                                if (historys[j].blok != null)
                                {
                                    t = (DateTime)historys[j].blok;
                                    t = t.AddMinutes(30);
                                }
                                if (DateTime.Now < historys[j].blok || t <= DateTime.Now)
                                {
                                    vx = 1;
                                    int count_h = Entities.GetContext().Histori.Count();
                                    history_login[0].id = count_h + 1;
                                    history_login[0].login = user;
                                    history_login[0].dataZ = DateTime.Now;
                                    history_login[0].ip = Dns.GetHostName();
                                    if (historys[j].blok < DateTime.Now)
                                    {
                                        history_login[0].blok = date.AddHours(2.5);
                                    }
                                    else
                                    {
                                        history_login[0].blok = historys[j].blok;
                                    }
                                    Entities.GetContext().Histori.Add(history_login[0]);
                                    Entities.GetContext().SaveChanges();
                                    frame1.Navigate(new Glavnaya(Users[i].login, frame1));
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("Перерыв пол часа");
                                    vx = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (vx == 0)
            {
                MessageBox.Show("Неверный логин или пароль");
                error++;
            }
        }

        List<Zadanie_1_UP.Users> Users = new List<Zadanie_1_UP.Users>();
        List<Zadanie_1_UP.Workers> worker = new List<Zadanie_1_UP.Workers>();
        List<Zadanie_1_UP.Histori> history_login = new List<Zadanie_1_UP.Histori> { new Histori() };
        List<Zadanie_1_UP.Histori> historys = new List<Zadanie_1_UP.Histori>();
        int error = 0;

        private void Avtoriz_Vxod(object sender, MouseButtonEventArgs e)
        {
            Captcha captcha = new Captcha();
            if (error > 3)
            {
                captcha.Show();
            }
            else
            {
                vx = 0;
                vxod();
            }
        }

        private void Avtoriz_Zareg(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Reg(frame1));
        }

        private void Avtoriz_Glaz_z(object sender, MouseButtonEventArgs e)
        {
            password.Visibility = Visibility.Hidden;
            password1.Visibility = Visibility.Visible;
            glaz_z.Visibility = Visibility.Hidden;
            glaz_o.Visibility = Visibility.Visible;
            password1.Text = password.Password;
        }

        private void Avtoriz_Glaz_o(object sender, MouseButtonEventArgs e)
        {
            password.Visibility = Visibility.Visible;
            password1.Visibility = Visibility.Hidden;
            glaz_z.Visibility = Visibility.Visible;
            glaz_o.Visibility = Visibility.Hidden;
            password.Password = password1.Text;
        }
    }
}
