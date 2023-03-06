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
        Navig sp = new Navig();
        List<Service> List_Service = new List<Service>();
        int kolvo_zapice = 3;

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

            List_Service = Entities.GetContext().Service.ToList();
            LViewTours.ItemsSource = Entities.GetContext().Service.ToList();

            sp.CountPageFlower = Entities.GetContext().Service.ToList().Count;
            DataContext = sp;

            try
            {
                sp.CountPageFlower = Convert.ToInt32(kolvo_zapice); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                sp.CountPageFlower = List_Service.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            sp.CountlistFlower = List_Service.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            LViewTours.ItemsSource = List_Service.Skip(0).Take(sp.CountPageFlower).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            sp.CurrentPage = 1; // текущая страница - это страница 1

        }

        /*private void kolvo_zapice_flower_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                sp.CountPageFlower = Convert.ToInt32(kolvo_zapice_flower.Text); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                sp.CountPageFlower = List_Service.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            sp.CountlistFlower = List_Service.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            LViewTours.ItemsSource = List_Service.Skip(0).Take(sp.CountPageFlower).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            sp.CurrentPage = 1; // текущая страница - это страница 1
        }*/

        private void Glavnaya_GoPage(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    sp.CurrentPage--;
                    break;
                case "next":
                    sp.CurrentPage++;
                    break;
                case "prev1":
                    sp.CurrentPage = 1;
                    break;
                case "next1":
                    {
                        List<Service> fl = Entities.GetContext().Service.ToList();
                        int a = fl.Count;
                        int b = Convert.ToInt32(kolvo_zapice);

                        if (a % b == 0)
                        {
                            sp.CurrentPage = a / b;
                        }
                        else
                        {
                            sp.CurrentPage = a / b + 1;
                        }

                    }
                    break;
                default:
                    sp.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            } 
            LViewTours.ItemsSource = List_Service.Skip(sp.CurrentPage * sp.CountPageFlower - sp.CountPageFlower).Take(sp.CountPageFlower).ToList();
        }

    }
}
