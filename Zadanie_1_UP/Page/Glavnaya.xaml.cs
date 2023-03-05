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

namespace Zadanie_1_UP
{
    /// <summary>
    /// Логика взаимодействия для Glavnaya.xaml
    /// </summary>
    public partial class Glavnaya : Page
    {
        public Frame frame1;

        public Glavnaya(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }

        private void Glavnaya_Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Avtoriz(frame1));
        }

        private void Glavnaya_Analiz(object sender, MouseButtonEventArgs e)
        {
            /*var all = Entities.GetContext().Service.ToList();
            ComboType.ItemsSource = all;
            LViewTours.ItemsSource = all;*/

            var all = Entities.GetContext().Service.ToList();
           
            ComboType.ItemsSource = all;
            LViewTours.ItemsSource = all;

            LViewTours.Visibility = Visibility.Visible;
        }

    }
}
