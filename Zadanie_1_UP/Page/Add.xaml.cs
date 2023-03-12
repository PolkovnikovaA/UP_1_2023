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
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        Microsoft.Win32.OpenFileDialog img = new Microsoft.Win32.OpenFileDialog();
        public Frame frame1;
        string user;
        public Add(string User, Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
        }

        private void Add_Service2(object sender, RoutedEventArgs e)
        {
            List<Service> services = new List<Service> { new Service() };
            int count = Entities.GetContext().Service.Count();
            try
            {
                //services[0].id = count + 5;
                services[0].service1 = Text_service1.Text;
                services[0].price = Convert.ToDouble(Text_pric.Text);
                services[0].Sotr = Text_lab.Text;
                
                Entities.GetContext().Service.Add(services[0]);
                Entities.GetContext().SaveChanges();
                frame1.Navigate(new Glavnaya(user, frame1));
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void Add_Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Glavnaya(user, frame1));
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
    }
}
