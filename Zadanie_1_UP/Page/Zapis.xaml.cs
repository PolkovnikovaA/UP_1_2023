using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using System.Windows.Threading;

namespace Zadanie_1_UP
{
    /// <summary>
    /// Логика взаимодействия для Glavnaya.xaml
    /// </summary>
    public partial class Zapis : Page
    {
        public Frame frame1;
        string user;
        object Item;
        List<Workers> workers = new List<Workers>();
        List<Service> services = new List<Service>();
        List<Users> users = new List<Users>();

        public Zapis(string User, Frame frame, object item)
        {
            InitializeComponent();
            frame1 = frame;
            Item = item;
            user = User;
            

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
                    Details_Service_Admin();
                }
            }

        }

        public void Details_Service_Lab()
        {
            services = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                {
                    Text_service1.Text = services[i].service1;
                    Text_pric.Text = services[i].price.ToString();
                    break;
                }
            }
        }

        public void Details_Service_Admin()
        {
            Delete.Visibility = Visibility.Visible;
            Izm.Visibility = Visibility.Visible;

            services = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                {
                    Text_service1.Text = services[i].service1;
                    break;
                }
            }
            
        }

        private void Zapis_Delete(object sender, RoutedEventArgs e)
        {
            List<Service> ser = new List<Service> { };
            ser = Entities.GetContext().Service.ToList();
            try
            {
                for (int i = 0; i < ser.Count; i++)
                {
                    if (ser[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                    {
                        Entities.GetContext().Service.Remove(ser[i]);
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
            List<Service> ser = new List<Service>();
            ser = Entities.GetContext().Service.ToList();
            try
            {
                for (int i = 0; i < ser.Count; i++)
                {
                    if (ser[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                    {
                        ser[i].service1 = Text_service1.Text;
                        //ser[i].price = Convert.ToInt32(Text_pric.Text);
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
    }
}
