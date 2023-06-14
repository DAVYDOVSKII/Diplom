using FontAwesome.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WDCApp.Entities;
using System.Data.Entity;
using System.ComponentModel;

namespace WDCApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public class ServiceWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Service> services;

        public ObservableCollection<Service> Services
        {
            get { return services; }
            set
            {
                services = value;
                OnPropertyChanged(nameof(Services));
            }
        }

        // Конструктор модели представления
        public ServiceWindowViewModel()
        {
            // Загрузка данных для ShoppingItems
            LoadData();
        }

        // Загрузка данных для ShoppingItems
        private void LoadData()
        {
            using (var context = new Model1())
            {
                Services = new ObservableCollection<Service>(context.Service);
            }
        }

        // Реализация интерфейса INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public partial class ServiceWindow : Window
    {

        ObservableCollection<Service> services;

        ObservableCollection<Service> cartItems;
      

        public double cartTotal = 0;

        public ServiceWindow()
        {
            InitializeComponent();
            // Привязка обработчика события к событию SelectionChanged вашего ListBox (ShoppingItems)
            ShoppingItems.SelectionChanged += ShoppingItems_SelectionChanged;

            // Привязка обработчика события к событию Click вашей кнопки (CheckOut)
            CheckOut.Click += CheckOut_Click;
            cartItems = new ObservableCollection<Service>();
            GetItems();
        }

        private void GetItems()
        {
            using (var context = new Entities.Model1())
            {
                services = new ObservableCollection<Service>(context.Service.ToList());
                ShoppingItems.ItemsSource = services;
                ShoppingCart.ItemsSource = cartItems;
            }
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            {
                string cost = GetTotal();
                string message = $"Общее количество: {cartItems.Count}" + Environment.NewLine +
                                 $"Общая стоимость: {cost}" + Environment.NewLine +
                                 Environment.NewLine +
                                 $"Оформить заказ?";

                string caption = "Закрыть формы";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.No)
                {
                    e.Handled = true;
                    return;
                }

                if (result == MessageBoxResult.Yes)
                {
                    using (var context = new Model1())
                    {
                        foreach (var cartItem in cartItems)
                        {
                            Contract newContract = new Contract
                            {

                                IdService = cartItem.Id,
                                NameService = cartItem.NameService,
                                NumberContract = GenerateContractNumber(),
                                IdClient = GetClientId(),
                                Date = DateTime.Now,
                                Status = false
                            };

                            context.Contract.Add(newContract);
                        }

                        context.SaveChanges();
                    }

                    MessageBox.Show("Заказ успешно сформирован!");

                    Discounted_Stackpanel.Visibility = Visibility.Collapsed;
                    cartItems.Clear();
                    CartTotal.Text = "0";
                    CartCount.Text = "0";
                }

                CartTotal.Text = GetTotal();
                CartCount.Text = cartItems.Count.ToString();
                ShoppingItems.SelectedItem = null;
            }
        }

        private void Coupon_Added_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Item_Remove_Btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Service selectedItem = (Service)((ImageAwesome)sender).DataContext;
            Service selectedService = services.FirstOrDefault(s => s.Id == selectedItem.Id);
            if (selectedService != null)
            {
                using (var context = new Entities.Model1())
                {
                    context.Entry(selectedService).State = EntityState.Detached;
                    cartItems.Remove(selectedItem);
                }
            }

            CartTotal.Text = GetTotal();
            CartCount.Text = cartItems.Count.ToString();
        }

        private void ShoppingItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems = ((ListBox)sender).SelectedItems;
            if (selectedItems.Count == 0)
                return;

            foreach (var selectedItem in selectedItems)
            {
                Service selectedService = (Service)selectedItem;
                if (selectedService == null)
                    continue;

                cartItems.Add(new Service
                {
                    Id = selectedService.Id,
                    NameService = selectedService.NameService,
                    Price = selectedService.Price
                });
            }

            CartTotal.Text = GetTotal();
            CartCount.Text = cartItems.Count.ToString();
            ShoppingItems.SelectedItem = null;
        }
        private string GetTotal()
        {
            cartTotal = 0;

            foreach (var item in cartItems)
            {
                using (var context = new Entities.Model1())
                {
                    Service service = context.Service.Find(item.Id);
                    if (service != null)
                    {
                        cartTotal += service.Price.GetValueOrDefault();
                    }
                }
            }

            return cartTotal.ToString("C2");
        }



        private int? GetClientId()
        {
            string clientLastName = Client_Field.Text;

            var client = App.Context.Client.FirstOrDefault(c => c.Surname == clientLastName);

            if (client == null)
            {
                MessageBox.Show("Клиент не найден!");

                // Перенаправляем пользователя на окно "RegistrationClientWindow"
                RegistrationClientWindow registrationWindow = new RegistrationClientWindow();
                registrationWindow.ShowDialog();

                // Получаем идентификатор клиента после добавления нового клиента
                client = App.Context.Client.FirstOrDefault(c => c.Surname == clientLastName);

                if (client == null)
                {
                    MessageBox.Show("Клиент не добавлен!");
                    return null;
                }
            }

            return client.Id; // Возвращаем идентификатор найденного клиента

        }


        private string GenerateContractNumber()
        {
            // Генерация уникального номера контракта
      
            DateTime now = DateTime.Now;
            return $"{now.Year}{now.Month}{now.Day}{now.Hour}{now.Minute}{now.Second}";
        }

        private void ShoppingCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;

            foreach (var selectedItem in listBox.SelectedItems)
            {
                if (selectedItem is Service selectedService)
                {
                    cartItems.Add(new Service
                    {
                        Id = selectedService.Id,
                        NameService = selectedService.NameService,
                        Price = selectedService.Price
                    });
                }
            }

            CartTotal.Text = GetTotal();
            CartCount.Text = cartItems.Count.ToString();
            ShoppingItems.SelectedItem = null;
        }

        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Service selectedItem = (Service)button.DataContext;

            // Создание нового окна
            MoreWindow moreWindow = new MoreWindow();

            // Передача данных в новое окно
            moreWindow.SetData(selectedItem.NameService, selectedItem.Price, selectedItem.Description);

            // Отображение нового окна
            moreWindow.Show();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Service selectedItem = (Service)button.DataContext;

            // Создание нового окна
            EditServiceWindow editServiceWindow = new EditServiceWindow();

            // Передача данных в новое окно
            editServiceWindow.SetData(selectedItem.NameService, selectedItem.Price, selectedItem.Description);

            // Отображение нового окна
            editServiceWindow.Show();
        }
    }
}
