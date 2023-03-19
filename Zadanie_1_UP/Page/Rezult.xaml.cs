using Aspose.BarCode.Generation;
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
    /// Логика взаимодействия для Rezult.xaml
    /// </summary>
    public partial class Rezult : Page
    {
        string user;
        public Frame frame1;
        object Item;

        List<Workers> workers = new List<Workers>();
        List<Result> results = new List<Result>();
        public Rezult(string user, Frame frame, object item)
        {
            InitializeComponent();
            user = user;
            frame1 = frame;
            Item = item;

            workers = Entities.GetContext().Workers.ToList();
            int count = Entities.GetContext().Workers.Count();
            for (int i = 0; i < count; i++)
            {
                if (workers[i].login == user && workers[i].dolg == "Лаборант")
                {
                    Details_Service_Lab();
                }
                if (workers[i].login == user && workers[i].dolg == "Администратор")
                {
                    Details_Service_Admin();
                }
                else
                {
                   // Details_Service_User();
                }
            }
        }

        public void Details_Service_Lab()
        {
            Sotr.Visibility = Visibility.Hidden;
            Text_lab2.Visibility = Visibility.Hidden;
            Zapiss.Visibility = Visibility.Hidden;

            results = Entities.GetContext().Result.ToList();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Issled == Item.GetType().GetProperty("Issled").GetValue(Item))
                {
                    Text_service12.Text = results[i].Issled;
                    if (results[i].result1 == "+")
                    {
                        Text_pric2.Text = "Положительный";
                    }
                    else
                    {
                        Text_pric2.Text = "Отрицательный";
                    }
                    //Text_lab2.Text = results[i].Sotr.ToString();
                    
                    break;
                }
            }
            print.Visibility = Visibility.Visible;

        }

        public void Details_Service_Admin()
        {
            Image_Delete.Visibility = Visibility.Visible;
            Image_Izm.Visibility = Visibility.Visible;
            Text_service1.Visibility = Visibility.Visible;
            Text_pric.Visibility = Visibility.Visible;
            Text_lab.Visibility = Visibility.Visible;
            Text_service12.Visibility = Visibility.Hidden;
            Text_pric2.Visibility = Visibility.Hidden;
            Text_lab2.Visibility = Visibility.Hidden;
            Zapiss.Visibility = Visibility.Hidden;
            Zapiss.Visibility = Visibility.Hidden;

            results = Entities.GetContext().Result.ToList();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Name == Item.GetType().GetProperty("Name").GetValue(Item))
                {
                    Text_service1.Text = results[i].Name;
                    Text_pric.Text = results[i].Issled;
                    Text_lab.Text = results[i].result1.ToString();
                    break;
                }
            }

        }

        private void Zapis_Delete(object sender, RoutedEventArgs e)
        {
            List<Result> ser = new List<Result> { };
            ser = Entities.GetContext().Result.ToList();
            try
            {
                for (int i = 0; i < ser.Count; i++)
                {
                    if (ser[i].Name == Item.GetType().GetProperty("Name").GetValue(Item))
                    {
                        Entities.GetContext().Result.Remove(ser[i]);
                        Entities.GetContext().SaveChanges();
                        break;
                    }
                }
                frame1.Navigate(new Glavnaya(user, frame1));
            }
            catch
            {
                MessageBox.Show("Вы не можете удалить данный анализ, пока не будет удален результат");
            }
        }

        private void Zapis_Izm(object sender, RoutedEventArgs e)
        {
            List<Result> ser = new List<Result>();
            ser = Entities.GetContext().Result.ToList();
            try
            {
                for (int i = 0; i < ser.Count; i++)
                {
                    if (ser[i].Name == Item.GetType().GetProperty("Name").GetValue(Item))
                    {
                        Text_service1.Text = results[i].Name;
                        Text_pric.Text = results[i].Issled.ToString();
                        Text_lab.Text = results[i].result1.ToString();
                        break;
                    }
                }
                Entities.GetContext().SaveChanges();
                frame1.Navigate(new Glavnaya(user, frame1));
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void Rezult_Print(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new Print(user, frame1, Item));
        }

        private void Zapis_Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Glavnaya(user, frame1));
        }
    }
}
