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
    /// Логика взаимодействия для Add_Result.xaml
    /// </summary>
    public partial class Add_Result : Page
    {
        public Frame frame1;
        string user;
        public Add_Result(string User, Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;

            var allP = Entities.GetContext().Users.ToList();
            allP.Insert(0, new Users
            {
                name = "Пациент"
            });
            User2.ItemsSource = allP;
            User2.SelectedIndex = 0;

            var allL = Entities.GetContext().Workers.ToList();
            allL.Insert(0, new Workers
            {
                name = "Лаборант"
            });
            Sotr.ItemsSource = allL;
            Sotr.SelectedIndex = 0;

            var allS = Entities.GetContext().Service.ToList();
            allS.Insert(0, new Service
            {
                service1 = "Услуга"
            });
            Service.ItemsSource = allS;
            Service.SelectedIndex = 0;
        }

        private void Add_Res(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Result> results = new List<Result> { new Result() };
                results[0].id_lad = Sotr.SelectedIndex;
                results[0].id_service = Service.SelectedIndex;
                results[0].result1 = Result.Text;
                Entities.GetContext().Result.Add(results[0]);
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
    }
}
