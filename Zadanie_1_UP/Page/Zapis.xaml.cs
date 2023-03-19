using System;
using System.Collections.Generic;
using System.IO;
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
using Aspose.BarCode.Generation;
using Path = System.IO.Path;


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
        Microsoft.Win32.OpenFileDialog img = new Microsoft.Win32.OpenFileDialog();
        List<Workers> workers = new List<Workers>();
        List<Service> services = new List<Service>();

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
                    Details_Service_User();
                }
            }
        }

        public void Details_Service_Lab()
        {
            Zapiss.Visibility = Visibility.Hidden;

            services = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                {
                    Sotr2.Text = services[i].Workers.name;
                    Service2.Text = services[i].service1.ToString();
                    Price2.Text = Convert.ToString(services[i].price);
                    break;
                }
            }
        }

        private void Zapis_Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Glavnaya(user, frame1));
        }

        public void Details_Service_Admin()
        {
            Zapiss.Visibility = Visibility.Hidden;
            Sotr2.Visibility = Visibility.Hidden;
            Service2.Visibility = Visibility.Hidden;
            Price2.Visibility = Visibility.Hidden;

            Sotr1.Visibility = Visibility.Visible;
            Service.Visibility = Visibility.Visible;
            Price.Visibility = Visibility.Visible;
            Delete.Visibility = Visibility.Visible;
            Izm.Visibility = Visibility.Visible;

            services = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                {
                    Sotr1.Text = services[i].Workers.name;
                    Service.Text = services[i].service1;
                    Price.Text = Convert.ToString(services[i].price);
                    break;
                }
            }
        }

        public void Details_Service_User()
        {
            services = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                {
                    Sotr2.Text = services[i].Workers.name;
                    Service2.Text = services[i].service1.ToString();
                    Price2.Text = Convert.ToString(services[i].price);
                    break;
                }
            }
        }

        private void Zapis_Delete(object sender, RoutedEventArgs e)
        {
            List<Service> services = new List<Service> { };
            services = Entities.GetContext().Service.ToList();
            try
            {
                for (int i = 0; i < services.Count; i++)
                {
                    if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                    {
                        Entities.GetContext().Service.Remove(services[i]);
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
            List<Service> services = new List<Service>();
            services = Entities.GetContext().Service.ToList();
            try
            {
                for (int i = 0; i < services.Count; i++)
                {
                    if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                    {
                        services[i].Workers.name = Sotr1.Text;
                        services[i].service1 = Service.Text;
                        services[i].price = Convert.ToDouble(Price.Text);
                        if (services[i].image == null)
                        {
                            //рандом
                            String allowchar = " ";
                            allowchar += "1,2,3,4,5,6,7,8,9,0";
                            char[] a = { ',' };
                            String[] ar = allowchar.Split(a);
                            String pwd = "";
                            string temp = "";
                            Random r = new Random();
                            for (int j = 0; j < 10; j++)
                            {
                                temp = ar[(r.Next(0, ar.Length))];
                                pwd += temp;
                            }

                            //генератор штрихкода
                            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, pwd);
                            var imageType = "Png";
                            string imagePath = "barcode" + pwd + ".Png";
                            string path = System.IO.Path.GetFullPath(imagePath);
                            generator.Save(imagePath, BarCodeImageFormat.Png);
                            services[i].image = path;
                        }
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
