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
using WDCApp.Entities;
using WDCApp.Pages;
using WDCApp.Windows;

namespace WDCApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(string greeting, string fullName)
        {
            InitializeComponent();

            txtGreeting.Text = $"{greeting}, {fullName}!";

        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {
           ServiceWindow serviceWindow = new ServiceWindow();   
            serviceWindow.Show();
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            ServiceFrame.Navigate(new Pages.StaffPage());
            Manager.ServiceFrame = ServiceFrame;
        }

        private void btnRegistrationClient_Click(object sender, RoutedEventArgs e)
        {
            RegistrationClientWindow registrationClientWindow = new RegistrationClientWindow();
            registrationClientWindow.Show();
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            ServiceFrame.Navigate(new Pages.ClientPage());
            Manager.ServiceFrame = ServiceFrame;
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            ServiceFrame.Navigate(new Pages.OrdersPage());
            Manager.ServiceFrame = ServiceFrame;    
        }
    }
}
