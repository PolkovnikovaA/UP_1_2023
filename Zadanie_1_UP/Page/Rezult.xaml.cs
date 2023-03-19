using Aspose.BarCode.Generation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    Details_Resulr_Lab();
                }
                if (workers[i].login == user && workers[i].dolg == "Администратор")
                {
                    Details_Rezult_Admin();
                }
                else
                {
                    Details_Rezult_User();
                }
            }
        }

        public void Details_Resulr_Lab()
        {
            Zapiss.Visibility = Visibility.Hidden;

            Print.Visibility = Visibility.Visible;

            results = Entities.GetContext().Result.ToList();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Name == Item.GetType().GetProperty("Name").GetValue(Item))
                {
                    Sotr2.Text = results[i].Workers.name;
                    Service2.Text = results[i].Service.service1;
                    Price2.Text = Convert.ToString(results[i].Service.price);
                    if (results[i].result1 == "+")
                    {
                        Result2.Text = "Пложительный";
                    }
                    if (results[i].result1 == "-")
                    {
                        Result2.Text = "Отрицательный";
                    }
                    break;
                }
            }
        }

        public void Details_Rezult_Admin()
        {
            Sotr.Visibility = Visibility.Visible;
            Service.Visibility = Visibility.Visible;
            Price.Visibility = Visibility.Visible;
            Result.Visibility = Visibility.Visible;
            Delete.Visibility = Visibility.Visible;
            Izm.Visibility = Visibility.Visible;
            Print.Visibility = Visibility.Visible;

            Sotr2.Visibility = Visibility.Hidden;
            Service2.Visibility = Visibility.Hidden;
            Price2.Visibility = Visibility.Hidden;
            Result2.Visibility = Visibility.Hidden;
            Zapiss.Visibility = Visibility.Hidden;

            results = Entities.GetContext().Result.ToList();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].result1 == Item.GetType().GetProperty("result1").GetValue(Item))
                {
                    Sotr.Text = results[i].Workers.name;
                    Service.Text = results[i].Service.service1;
                    Price.Text = Convert.ToString(results[i].Service.price);
                    Result.Text = results[i].result1;
                    break;
                }
            }
        }

        public void Details_Rezult_User()
        {
            results = Entities.GetContext().Result.ToList();
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].Name == Item.GetType().GetProperty("Name").GetValue(Item))
                {
                    Sotr2.Text = results[i].Workers.name;
                    Service2.Text = results[i].Service.service1;
                    Price2.Text = Convert.ToString(results[i].Service.price);
                    if (results[i].result1 == "+")
                    {
                        Result2.Text = "Пложительный";
                    }
                    if (results[i].result1 == "-")
                    {
                        Result2.Text = "Отрицательный";
                    }
                    break;
                }
            }
        }

        //удаление результатов (администратор)
        private void Result_Delete(object sender, RoutedEventArgs e)
        {
            List<Result> results = new List<Result> { };
            results = Entities.GetContext().Result.ToList();
            try
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].result1 == Item.GetType().GetProperty("result1").GetValue(Item))
                    {
                        Entities.GetContext().Result.Remove(results[i]);
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

        //изменение результатов (администратор)
        private void Result_Izm(object sender, MouseButtonEventArgs e)
        {
            List<Result> results = new List<Result>();
            results = Entities.GetContext().Result.ToList();
            try
            {
                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].result1 == Item.GetType().GetProperty("result1").GetValue(Item))
                    {
                        results[i].Workers.name = Sotr.Text;
                        results[i].Service.service1 = Service.Text;
                        results[i].Service.price = Convert.ToDouble(Price.Text);
                        results[i].result1 = Result.Text;
                        if (results[i].BarCodeR == null)
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
                            results[i].BarCodeR = path;
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
