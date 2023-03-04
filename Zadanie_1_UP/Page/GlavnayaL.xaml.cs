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
    /// Логика взаимодействия для GlavnayaL.xaml
    /// </summary>
    public partial class GlavnayaL : Page
    {
        public Frame frame1;
        public GlavnayaL(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }
    }
}
