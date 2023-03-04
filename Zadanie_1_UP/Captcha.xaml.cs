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
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        public Captcha()
        {
            InitializeComponent();
            Cap();
        }

        public void Cap()   //капча
        {
            String allowchar = " ";
            allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
            allowchar += "1,2,3,4,5,6,7,8,9,0";
            char[] a = { ',' };
            String[] ar = allowchar.Split(a);
            String pwd = "";
            string temp = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                temp = ar[(r.Next(0, ar.Length))];
                pwd += temp;
            }
            captha1.Text = pwd;
        }

        private void Captcha_Captcha(object sender, RoutedEventArgs e)
        {
            string cap1 = captha1.Text;
            string cap2 = captha2.Text;
            if (cap2 == cap1)
            {
                this.Close();
            }
            else
            {
                this.Close();
                Captcha capcha = new Captcha();
                capcha.Show();
            }

        }

        private void Captcha_Up(object sender, RoutedEventArgs e)
        {
            Cap();
        }
    }
}
