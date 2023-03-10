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
        public Frame frame1;
        string user;
        public Add(string User, Frame frame1)
        {
            InitializeComponent();
            frame1 = frame1;
            user = User;
        }

        private void Add_Service2(object sender, RoutedEventArgs e)
        {
            List<Service> services = new List<Service> { };
            int count = Entities.GetContext().Service.Count();
            try
            {
                services[0].id = count + 3;
                services[0].service1 = Text_service1.Text;
                services[0].price = null;
                services[0].image = null;
                
                Entities.GetContext().Service.Add(services[0]);
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
