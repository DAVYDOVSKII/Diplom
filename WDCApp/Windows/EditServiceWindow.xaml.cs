using System;
using System.Text;
using System.Windows;
using WDCApp.Entities;

namespace WDCApp.Windows
{
    public partial class EditServiceWindow : Window
    {
        private Entities.Service _currentService = null;


        public EditServiceWindow()
        {
            InitializeComponent();
        }

        internal void SetData(string nameService, double? price, string description)
        {
            _currentService = new Entities.Service
            {
                NameService = nameService,
                Price = price,
                Description = description,
               
            };

            txtNameService.Text = nameService;
            txtDescriptionService.Text = description;
            txtPriceService.Text = price.ToString();
          
        }

        private void btnSaveEditService_Click(object sender, RoutedEventArgs e)
        {
            string errors = CheckErrors();
            if (!string.IsNullOrWhiteSpace(errors))
            {
                MessageBox.Show(errors, "Ошибки", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            _currentService.NameService = txtNameService.Text;
            _currentService.Description = txtDescriptionService.Text;
            _currentService.Price = double.Parse(txtPriceService.Text);

          
            App.Context.SaveChanges();
            MessageBox.Show("Изменения сохранены успешно!");
            Close();
        }

        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtNameService.Text))
                errorBuilder.AppendLine("Название услуги обязательно для заполнения;");

            double price = 0;
            if (double.TryParse(txtPriceService.Text, out price) == false || price <= 0)
                errorBuilder.AppendLine("Стоимость услуги должна быть положительным числом;");

            if (errorBuilder.Length > 0)
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");

            return errorBuilder.ToString();
        }
    }
}
