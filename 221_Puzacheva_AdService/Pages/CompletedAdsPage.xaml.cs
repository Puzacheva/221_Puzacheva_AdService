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
    /// Логика взаимодействия для CompletedAdsPage.xaml
    /// </summary>
    public partial class CompletedAdsPage : Page
    {
        private readonly User _currentUser;

        public CompletedAdsPage(User user)
        {
            InitializeComponent();

            _currentUser = user;

            ListAds.ItemsSource = Entities.GetContext().Ad
         .Where(ad => ad.User_ID == _currentUser.ID && ad.Status == 2)
         .ToList();

            DisplayTotalProfit();
        }

        private void DisplayTotalProfit()
        {
            var totalProfit = Entities.GetContext().Ad
                .Where(ad => ad.User_ID == _currentUser.ID && ad.Status == 2)
                .Sum(ad => ad.User.Profit); 

            ProfitTextBlock.Text = $"Прибыль: {totalProfit} ₽";
        }
    }
}
