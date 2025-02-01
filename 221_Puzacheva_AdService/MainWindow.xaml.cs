using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace _221_Puzacheva_AdService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _currentUser;

        protected override void OnClosing(CancelEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = $"AdsService - {page.Title}";

            if (page is Pages.AllAdsPage)
                BackButton.Visibility = Visibility.Hidden;
            else
                BackButton.Visibility = Visibility.Visible;

            if (page is Pages.AllAdsPage)
                AuthButton.Visibility = Visibility.Visible;
            else
                AuthButton.Visibility = Visibility.Hidden;

            if (page is Pages.UserPage)
                CompleteAdsButton.Visibility = Visibility.Visible;
            else
                CompleteAdsButton.Visibility = Visibility.Hidden;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.AuthPage());
        }

        private void CompleteAdsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.CompletedAdsPage(_currentUser)); 
        }
    }
}
