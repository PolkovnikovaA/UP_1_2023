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
    public partial class Zapis : Page
    {
        public Frame frame1;
        string User;
        object Item;
        List<Service> services = new List<Service>();

        public Zapis(Frame frame, object item)
        {
            InitializeComponent();
            frame1 = frame;
            Item = item;
            services = Entities.GetContext().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                {
                    Text_service1.Text = services[i].service1;
                    Text_pric.Text = services[i].price.ToString();
                    break;
                }
            }

        }

    }
}
