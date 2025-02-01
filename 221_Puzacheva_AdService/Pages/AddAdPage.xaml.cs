using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddAdPage.xaml
    /// </summary>
    public partial class AddAdPage : Page
    {
        private int currentUserID;
        private Ad currentAd;

        public AddAdPage(int userId, Ad adToEdit = null)
        {
            InitializeComponent();
            currentUserID = userId;
            currentAd = adToEdit;
            LoadComboboxes();

            if (currentAd != null)
            {
                TextBoxTitle.Text = currentAd.Title ?? "";
                TextBoxDescription.Text = currentAd.Description ?? "";
                TextBoxPrice.Text = currentAd.Price.ToString() ?? "";
                TextBoxPhoto.Text = currentAd.Photo ?? "";

                // Установка значений в ComboBox
                ComboBoxCity.SelectedValue = currentAd.City;
                ComboBoxCateg.SelectedValue = currentAd.Category;
                ComboBoxType.SelectedValue = currentAd.Type;
                ComboBoxStatus.SelectedValue = currentAd.Status;
                PubliсationDatePicker.SelectedDate = currentAd.Publication_date;
            }
        }

        private void LoadComboboxes()
        {
            ComboBoxCity.ItemsSource = Entities.GetContext().City.ToList();
            ComboBoxCity.DisplayMemberPath = "City1";
            ComboBoxCity.SelectedValuePath = "ID";

            ComboBoxCateg.ItemsSource = Entities.GetContext().Category.ToList();
            ComboBoxCateg.DisplayMemberPath = "Category1";
            ComboBoxCateg.SelectedValuePath = "ID";

            ComboBoxType.ItemsSource = Entities.GetContext().Type.ToList();
            ComboBoxType.DisplayMemberPath = "Type1";
            ComboBoxType.SelectedValuePath = "ID";

            ComboBoxStatus.ItemsSource = Entities.GetContext().Status.ToList();
            ComboBoxStatus.DisplayMemberPath = "Status1";
            ComboBoxStatus.SelectedValuePath = "ID";

        }

        private void TextBoxTitle_Changed(object sender, TextChangedEventArgs e)
        {
            txtHintTitle.Visibility = Visibility.Visible;
            if (TextBoxTitle.Text.Length > 0)
            {
                txtHintTitle.Visibility = Visibility.Hidden;
            }
        }

        private void TextBoxDescription_Changed(object sender, TextChangedEventArgs e)
        {
            txtHintDescription.Visibility = Visibility.Visible;
            if (TextBoxDescription.Text.Length > 0)
            {
                txtHintDescription.Visibility = Visibility.Hidden;
            }
        }

        private void TextBoxPrice_Changed(object sender, TextChangedEventArgs e)
        {
            txtHintPrice.Visibility = Visibility.Visible;
            if (TextBoxPrice.Text.Length > 0)
            {
                txtHintPrice.Visibility = Visibility.Hidden;
            }
        }

        private void TextBoxPhoto_Changed(object sender, TextChangedEventArgs e)
        {
            txtHintPhoto.Visibility = Visibility.Visible;
            if (TextBoxPhoto.Text.Length > 0)
            {
                txtHintPhoto.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (PubliсationDatePicker.SelectedDate == null || ComboBoxCity.SelectedItem == null
                || ComboBoxCateg.SelectedItem == null || ComboBoxType.SelectedItem == null
                || ComboBoxStatus.SelectedItem == null || string.IsNullOrEmpty(TextBoxTitle.Text)
                || string.IsNullOrEmpty(TextBoxDescription.Text) || string.IsNullOrEmpty(TextBoxPrice.Text))
            {
                MessageBox.Show("Все поля, кроме фото, являются обязательными!");
                return;
            }

            /*if (!Regex.IsMatch(TextBoxPrice.Text, @"^\d+(\.\d{1,2})?$"))
            {
                MessageBox.Show("Цена должна быть в формате: 1111 или 1111.00", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }*/

            try
            {
                if (currentAd != null)
                {
                    currentAd.Publication_date = PubliсationDatePicker.SelectedDate.Value;
                    currentAd.City = (int)ComboBoxCity.SelectedValue;
                    currentAd.Category = (int)ComboBoxCateg.SelectedValue;
                    currentAd.Type = (int)ComboBoxType.SelectedValue;
                    currentAd.Status = (int)ComboBoxStatus.SelectedValue;
                    currentAd.Title = TextBoxTitle.Text;
                    currentAd.Description = TextBoxDescription.Text;
                    currentAd.Price = decimal.Parse(TextBoxPrice.Text);
                    currentAd.Photo = string.IsNullOrWhiteSpace(TextBoxPhoto.Text) ? null : TextBoxPhoto.Text;
                }
                else
                {
                    var newAd = new Ad
                    {
                        Publication_date = PubliсationDatePicker.SelectedDate.Value,
                        City = (int)ComboBoxCity.SelectedValue,
                        Category = (int)ComboBoxCateg.SelectedValue,
                        Type = (int)ComboBoxType.SelectedValue,
                        Status = (int)ComboBoxStatus.SelectedValue,
                        Title = TextBoxTitle.Text,
                        Description = TextBoxDescription.Text,
                        Price = decimal.Parse(TextBoxPrice.Text),
                        Photo = string.IsNullOrWhiteSpace(TextBoxPhoto.Text) ? null : TextBoxPhoto.Text,
                        User_ID = currentUserID
                    };


                    Entities.GetContext().Ad.Add(newAd);
                }
                Entities.GetContext().SaveChanges();

                MessageBox.Show("Объявление успешно сохранено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new UserPage(Entities.GetContext().User.Find(currentUserID)));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserPage(Entities.GetContext().User.Find(currentUserID)));
        }

        private void ComboBoxStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxStatus.SelectedItem == "Завершено")
            {
                AmountInputWindow inputWindow = new AmountInputWindow();
                /*if (inputWindow.ShowDialog() == true)
                {
                    int profitAmount = intputWindow.ProfitAmount;

                    currentUser.Profit += profitAmount;
                    Entities.GetContext().SaveChanges();
                    
                    MessageBox.Show("Прибыль успешно добавлена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }*/
            }
        }
    }
}
