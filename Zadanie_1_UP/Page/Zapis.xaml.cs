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
            Sotr.Visibility = Visibility.Hidden;
            Text_lab2.Visibility = Visibility.Hidden;
            Zapiss.Visibility = Visibility.Hidden;
            Zapiss.Visibility = Visibility.Hidden;

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

        //Штрих-код
       /* public void Generate_Code()
        {
            var imageType = "Png";

            var imageFormat = (BarCodeImageFormat)Enum.Parse(typeof(BarCodeImageFormat), imageType.ToString());
            var encodeType = EncodeTypes.Code128;
            BarcodeGen barcode = new BarcodeGen();

            String allowchar = " ";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            barcode.Text = pwd;

            barcode.BarcodeType = encodeType;
            barcode.ImageType = imageFormat;

            try
            {
                string imagePath = GenerateBarcode(barcode);


                Uri fileUri = new Uri(Path.GetFullPath(imagePath));
               //imgDynamic.Source = new BitmapImage(fileUri);
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private string GenerateBarcode(BarcodeGen barcode)
        {
            // Image path
            string imagePath = "." + barcode.ImageType;

            // Initilize barcode generator
            BarcodeGenerator generator = new BarcodeGenerator(barcode.BarcodeType, barcode.Text);

            // Save the image
            generator.Save(imagePath, barcode.ImageType);

            return imagePath;
        }*/

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
                        
                        if (ser[i].image == null)
                        {
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

                            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, pwd);
                            var imageType = "Png";

                            string imagePath = "barcode" + pwd + ".Png";

                            string path = System.IO.Path.GetFullPath(imagePath);

                            generator.Save(imagePath, BarCodeImageFormat.Png);
                            ser[i].image = path;


                        }
                        else
                        {
                            if (img.FileName != "")
                            {
                                ser[i].image = img.FileName;
                            }
                        }
                        ser[i].Sotr = Text_lab.Text;
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
