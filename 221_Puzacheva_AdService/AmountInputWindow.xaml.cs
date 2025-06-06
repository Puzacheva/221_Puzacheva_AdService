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
using System.Windows.Shapes;

namespace _221_Puzacheva_AdService
{
    /// <summary>
    /// Логика взаимодействия для AmountInputWindow.xaml
    /// </summary>
    public partial class AmountInputWindow : Window
    {
        public int ProfitAmount { get; private set; }

        public AmountInputWindow()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ProfitTextBox.Text, out int amount) && amount >= 0)
            {
                ProfitAmount = amount;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Введите неотрицательное целое число.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
