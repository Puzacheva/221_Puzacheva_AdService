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
    public partial class AllAdsPage : Page
    {
        public AllAdsPage()
        {
            InitializeComponent();

            LoadComboboxes();

            var currentAds = Entities.GetContext().Ad
                .Select(ad => new
                {
                    ad.Photo,
                    ad.Title,
                    ad.Description,
                    ad.Price,
                    City = ad.City1.City1
                }).ToList();

            ListAds.ItemsSource = currentAds;
        }

        private void LoadComboboxes()
        {
            ComboBoxCity.ItemsSource = Entities.GetContext().City.ToList();
            ComboBoxCity.DisplayMemberPath = "City1";

            ComboBoxCateg.ItemsSource = Entities.GetContext().Category.ToList();
            ComboBoxCateg.DisplayMemberPath = "Category1";

            ComboBoxType.ItemsSource = Entities.GetContext().Type.ToList();
            ComboBoxType.DisplayMemberPath = "Type1";

            ComboBoxStatus.ItemsSource = Entities.GetContext().Status.ToList();
            ComboBoxStatus.DisplayMemberPath = "Status1";

        }

        private void TextBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAds();
        }

        private void ComboBoxCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAds();
        }

        private void ComboBoxCateg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAds();
        }

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAds();
        }

        private void ComboBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAds();
        }

        private void UpdateAds()
        {
            var currentAds = Entities.GetContext().Ad.ToList();

            currentAds = currentAds.Where(x => x.Title.ToLower().Contains(TextBoxSearch.Text.ToLower())).ToList();

            if (ComboBoxCity.SelectedItem != null)
            {
                var selectedCity = ComboBoxCity.SelectedItem as City;
                if (selectedCity != null)
                {
                    currentAds = currentAds.Where(x => x.City == selectedCity.ID).ToList();
                }
            }

            if (ComboBoxCateg.SelectedItem != null)
            {
                var selectedCategory = ComboBoxCateg.SelectedItem as Category;
                if (selectedCategory != null)
                {
                    currentAds = currentAds.Where(x => x.Category == selectedCategory.ID).ToList();
                }
            }

            if (ComboBoxType.SelectedItem != null)
            {
                var selectedType = ComboBoxType.SelectedItem as Type;
                if (selectedType != null)
                {
                    currentAds = currentAds.Where(x => x.Type == selectedType.ID).ToList();
                }
            }

            if (ComboBoxStatus.SelectedItem != null)
            {
                var selectedStatus = ComboBoxStatus.SelectedItem as Status;
                if (selectedStatus != null)
                {
                    currentAds = currentAds.Where(x => x.Status == selectedStatus.ID).ToList();
                }
            }

            ListAds.ItemsSource = currentAds.Select(ad => new
            {
                ad.Photo,
                ad.Title,
                ad.Description,
                ad.Price,
                City = ad.City1.City1
            }).ToList();
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TextBoxSearch.Clear();
            ComboBoxCity.SelectedItem = null;
            ComboBoxCateg.SelectedItem = null;
            ComboBoxType.SelectedItem = null;
            ComboBoxStatus.SelectedItem = null;
        }
    }
}
