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
            Sotr.Visibility = Visibility.Hidden;
            Text_lab2.Visibility = Visibility.Hidden;

            services = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                {
                    Text_service12.Text = services[i].service1;
                    Text_pric2.Text = services[i].price.ToString();
                    Text_lab2.Text = services[i].Sotr.ToString();
                    if (services[i].Sotr == "Мулянов Андрей Александрович")
                    {
                        var uriImageSource = new Uri(@"/Zadanie_1_UP;component/Image/Sotr1.png", UriKind.RelativeOrAbsolute);
                        Image.Source = new BitmapImage(uriImageSource);
                    }
                    else if (services[i].Sotr == "Алешкина Варвара Владимировна")
                    {
                        var uriImageSource = new Uri(@"/Zadanie_1_UP;component/Image/Sotr2.png", UriKind.RelativeOrAbsolute);
                        Image.Source = new BitmapImage(uriImageSource);
                    }
                    else
                    {
                        var uriImageSource = new Uri(@"/Zadanie_1_UP;component/Image/zagl.jpg", UriKind.RelativeOrAbsolute);
                        Image.Source = new BitmapImage(uriImageSource);
                    }
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
            Image_Delete.Visibility = Visibility.Visible;
            Image_Izm.Visibility = Visibility.Visible;
            Text_service1.Visibility = Visibility.Visible;
            Text_pric.Visibility = Visibility.Visible;
            Text_lab.Visibility = Visibility.Visible;
            Text_service12.Visibility = Visibility.Hidden;
            Text_pric2.Visibility = Visibility.Hidden;
            Text_lab2.Visibility = Visibility.Hidden;
            Zapiss.Visibility = Visibility.Hidden;

            services = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                {
                    Text_service1.Text = services[i].service1;
                    Text_pric.Text = services[i].price.ToString();
                    Text_lab.Text = services[i].Sotr.ToString();
                    if (services[i].Sotr == "Мулянов Андрей Александрович")
                    {
                        var uriImageSource = new Uri(@"/Zadanie_1_UP;component/Image/Sotr1.png", UriKind.RelativeOrAbsolute);
                        Image.Source = new BitmapImage(uriImageSource);
                    }
                    else if (services[i].Sotr == "Алешкина Варвара Владимировна")
                    {
                        var uriImageSource = new Uri(@"/Zadanie_1_UP;component/Image/Sotr2.png", UriKind.RelativeOrAbsolute);
                        Image.Source = new BitmapImage(uriImageSource);
                    }
                    else
                    {
                        var uriImageSource = new Uri(@"/Zadanie_1_UP;component/Image/zagl.jpg", UriKind.RelativeOrAbsolute);
                        Image.Source = new BitmapImage(uriImageSource);
                    }
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
                    Text_service12.Text = services[i].service1;
                    Text_pric2.Text = services[i].price.ToString();
                    Text_lab2.Text = services[i].Sotr.ToString();
                    if (services[i].Sotr == "Мулянов Андрей Александрович")
                    {
                        var uriImageSource = new Uri(@"/Zadanie_1_UP;component/Image/Sotr1.png", UriKind.RelativeOrAbsolute);
                        Image.Source = new BitmapImage(uriImageSource);
                    }
                    else if (services[i].Sotr == "Алешкина Варвара Владимировна")
                    {
                        var uriImageSource = new Uri(@"/Zadanie_1_UP;component/Image/Sotr2.png", UriKind.RelativeOrAbsolute);
                        Image.Source = new BitmapImage(uriImageSource);
                    }
                    else
                    {
                        var uriImageSource = new Uri(@"/Zadanie_1_UP;component/Image/zagl.jpg", UriKind.RelativeOrAbsolute);
                        Image.Source = new BitmapImage(uriImageSource);
                    }
                    break;
                }
            }
        }

        private void Image_Zapis(object sender, MouseButtonEventArgs e)
        {
            img.FileName = "Picture";
            img.DefaultExt = ".jpg";
            img.Filter = "Picture (.jpg)|*.jpg";

            Nullable<bool> result = img.ShowDialog();
            if (result != false)
            {
                var uriImageSource = new Uri(img.FileName, UriKind.RelativeOrAbsolute);
                Image.Source = new BitmapImage(uriImageSource);
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
                        ser[i].price = Convert.ToInt32(Text_pric.Text);
                        ser[i].Sotr = Text_lab.Text;
                        if (ser[i].image == "")
                        {
                            ser[i].image = null;
                        }
                        else
                        {
                            if (img.FileName != "")
                            {
                                ser[i].image = img.FileName;
                            }
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
