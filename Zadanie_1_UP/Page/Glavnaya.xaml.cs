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
    public partial class Glavnaya : Page
    {
        private DispatcherTimer _timer;

        public static readonly DependencyProperty TickCounterProperty = DependencyProperty.Register(
            "TickCounter", typeof(int), typeof(Glavnaya), new PropertyMetadata(default(int)));

        public Frame frame1;
        string user;
        Navig sp = new Navig();
        List<Service> List_Service = new List<Service>();
        List<Users> users = new List<Users>();
        List<Workers> workers = new List<Workers>();
        List<Histori> historys = new List<Histori>();
        int kolvo_zapice = 3;

        public Glavnaya(string User, Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
            user = User;
            int count_hh = Entities.GetContext().Histori.Count();
            historys = Entities.GetContext().Histori.ToList();
            int time = 0;
            for (int j = count_hh - 1; j >= 0; j--)
            {
                if (historys[j].login == user)
                {
                    DateTime b = (DateTime)historys[j].blok;
                    DateTime d = (DateTime)historys[j].dataZ;
                    int h = b.Hour - d.Hour;
                    int m = b.Minute - d.Minute;
                    time = 60 * h + m;
                    break;
                }
            }
            DataContext = sp;
            DateTime dateTime = DateTime.Now;
            TickCounter = time;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1d);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();

            workers = Entities.GetContext().Workers.ToList();
            int count = Entities.GetContext().Workers.Count();
            for (int i = 0; i < count; i++)
            {
                if (workers[i].login == user && workers[i].dolg == "Лаборант")
                {
                    Add.Visibility = Visibility.Visible;
                    //Image.Visibility= Visibility.Hidden;
                }
                if (workers[i].login == user && workers[i].dolg == "Администратор")
                {
                    //Image.Visibility = Visibility.Hidden;
                }
            }
        }

        public int TickCounter
        {
            get { return (int)GetValue(TickCounterProperty); }
            set { SetValue(TickCounterProperty, value); }
        }

        public int soxr = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {

            if (--TickCounter <= 0)
            {
                var timer = (DispatcherTimer)sender;
                timer.Stop();
                if (soxr == 0)
                {
                    if (MessageBox.Show("Чтобы закончить работу и закрыть кабинет на кварцевание нажмите да, если хотите продолжить работу на 5 минут нажмите нет", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        TickCounter = 5;
                        _timer = new DispatcherTimer();
                        _timer.Interval = TimeSpan.FromMinutes(1d);
                        _timer.Tick += new EventHandler(Timer_Tick);
                        _timer.Start();
                        soxr = 1;
                    }
                    else
                    {
                        MessageBox.Show("Закрытие программы");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    MessageBox.Show("Закрытие программы");
                    Environment.Exit(0);
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Glavnaya_Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Avtoriz(frame1));
        }

        private async void GlavnayaService_Click(object sender, MouseButtonEventArgs e)
        {
            object item = LViewTours.SelectedItem;
            frame1.Navigate(new Zapis(user, frame1, item));
        }

        private void Update()
        {
            var currentService = Entities.GetContext().Service.ToList();
            
            currentService = currentService.Where(p => p.service1.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewTours.ItemsSource = currentService.ToList();

            var currentResult = Entities.GetContext().Result.ToList();

                for (int i = 0; i < currentResult.Count; i++)
                {
                    if (currentResult[i].login != user)
                    {
                        currentResult.RemoveAt(i);
                        i--;
                    }
                }
            currentResult = currentResult.Where(p => p.result1.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            LViewResult.ItemsSource = currentResult.ToList();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void Glavnaya_Analiz(object sender, MouseButtonEventArgs e)
        {
            var all = Entities.GetContext().Service.ToList();

            ComboType.ItemsSource = all;
            LViewTours.ItemsSource = all;

            LViewTours.Visibility = Visibility.Visible;
            LViewResult.Visibility = Visibility.Hidden;
            Str.Visibility = Visibility.Visible;
            TBoxSearch.Text = "";
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

        private void Glavnaya_Rezultat(object sender, MouseButtonEventArgs e)
        {
            List<Result> result = new List<Result>();
            List<Users> use = new List<Users>();
            TBoxSearch.Text = "";
            result = Entities.GetContext().Result.ToList();
            use = Entities.GetContext().Users.ToList();
            int counts1 = Entities.GetContext().Result.Count();
            for (int i = 0; i < counts1; i++)
            {
                if (result[i].login != user)
                {
                    result.RemoveAt(i);
                    i--;
                    counts1--;
                }
            }
            LViewResult.ItemsSource = result;
            LViewTours.Visibility = Visibility.Hidden;
            LViewResult.Visibility = Visibility.Visible;
            
        }

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

        private void Add5(object sender, RoutedEventArgs e)
        {
            frame1.Navigate(new Add(user, frame1));
        }
    }
}
