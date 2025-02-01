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

namespace _221_Puzacheva_AdService.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private readonly User _currentUser;

        public UserPage(User user)
        {
            InitializeComponent();

            if (NavigationService != null)
            {
                NavigationService.Navigated += NavigationService_Navigated;
            }
            _currentUser = user;

            UpdateAdList();
        }

        private void UpdateAdList()
        {
            ListAds.ItemsSource = Entities.GetContext().Ad
         .Where(ad => ad.User_ID == _currentUser.ID)
         .ToList();
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is UserPage)
            {
                UpdateAdList();
            }
        }

        private void ListAds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAd = ListAds.SelectedItem as Ad;
        }

        private void AddAd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Pages.AddAdPage(_currentUser.ID));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAd = ListAds.SelectedItem as Ad;

            if (selectedAd != null)
            {
                var adToEdit = Entities.GetContext().Ad.FirstOrDefault(ad => ad.ID == selectedAd.ID);

                if (adToEdit != null)
                {
                    NavigationService?.Navigate(new AddAdPage(_currentUser.ID, adToEdit));
                }
                else
                {
                    MessageBox.Show("Объявление не найдено в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteAd_Click(object sender, RoutedEventArgs e)
        {
            var adsForRemoving = ListAds.SelectedItems.Cast<Ad>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {adsForRemoving.Count()} элементов?",
                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    Entities.GetContext().Ad.RemoveRange(adsForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    UpdateAdList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
        }
    }
}
