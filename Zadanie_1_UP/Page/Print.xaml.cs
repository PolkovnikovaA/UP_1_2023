﻿using System;
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
    /// Логика взаимодействия для Print.xaml
    /// </summary>
    public partial class Print : Page
    {
        public Frame frame1;
        object Item;
        string user;

        List<Result> result = new List<Result>();

        public Print(string User, Frame frame, object item)
        {
            InitializeComponent();
            user = User;
            frame1 = frame;
            Item = item;

            Vivod();
        }

        //вывод на печать
        public void Vivod()
        {
            result = Entities.GetContext().Result.ToList();
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].result1 == Item.GetType().GetProperty("result1").GetValue(Item))
                {
                    Name.Text = result[i].Users1.name;
                    Data.Text = Convert.ToString(result[i].Hapi);
                    Graz.Text = result[i].Gra;
                    Iss.Text = result[i].Service.service1;
                    if (result[i].result1 == "+")
                    {
                        Issledovanie.Text = "Положительный";
                    }
                    else
                    {
                        Issledovanie.Text = "Отрицательный";
                    }    
                    break;
                }
            }
        }

        private void Zapis_Nazad(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Rezult(user, frame1, Item));
        }
    }
}
