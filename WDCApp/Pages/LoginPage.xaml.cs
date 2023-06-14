using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace WDCApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = App.Context.User.FirstOrDefault(p => p.Login == TBoxLogin.Text && p.Password == PBoxPassword.Password);
            if (currentUser != null)
            {
                App.CurrentUser = currentUser;

                string fullName = GetFullName(currentUser);       
                string greeting = GetGreeting();

                Manager.MainFrame.Navigate(new Pages.MainPage(greeting, fullName));              
            }
            else
            {
                MessageBox.Show("Пользователь не найден, повторите попытку!");
            }
        }
        private string GetFullName(User currentUser)
        {
            string fullName = $"{currentUser.Name} {currentUser.Patronymic}";
            return fullName;
        }

        private string GetGreeting()
        {
            DateTime currentTime = DateTime.Now;
            int currentHour = currentTime.Hour;

            if (currentHour >= 6 && currentHour < 12)
            {
                return "Доброе утро";
            }
            else if (currentHour >= 12 && currentHour < 18)
            {
                return "Добрый день";
            }
            else
            {
                return "Добрый вечер";
            }
        }
    }
}
