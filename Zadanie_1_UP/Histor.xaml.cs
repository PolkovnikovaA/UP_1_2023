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
using System.Windows.Shapes;

namespace Zadanie_1_UP
{
    /// <summary>
    /// Логика взаимодействия для Histor.xaml
    /// </summary>
    public partial class Histor : Window
    {
        public Histor()
        {
            InitializeComponent();
            List<Histori> historis = new List<Histori>();
            historis = Entities.GetContext().Histori.ToList();
            Dgrid.ItemsSource = historis;
        }
    }
}
