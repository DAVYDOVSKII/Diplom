using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WDCApp.Entities;



namespace WDCApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private OrderManager orderManager;
        private List<Contract> contracts;

        public OrdersPage()
        {
            InitializeComponent();
            orderManager = new OrderManager();
            contracts = orderManager.GetContractsIncludingClient();
            DataContext = contracts;
        }
        private void ExportSearchResultsToExcel(List<Contract> searchResults)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Search Results");

                    // Установка заголовков столбцов
                    worksheet.Cells[1, 1].Value = "Номер договора";
                    worksheet.Cells[1, 2].Value = "Услуга";
                    worksheet.Cells[1, 3].Value = "Цена услуги";
                    worksheet.Cells[1, 4].Value = "ФИО клиента";
                    worksheet.Cells[1, 5].Value = "Дата";

                    // Заполнение данными
                    for (int i = 0; i < searchResults.Count; i++)
                    {
                        worksheet.Cells[i + 2, 1].Value = searchResults[i].NumberContract;
                        worksheet.Cells[i + 2, 2].Value = searchResults[i].Service.NameService;
                        worksheet.Cells[i + 2, 3].Value = searchResults[i].Service.Price;
                        string fullName = $"{searchResults[i].Client.Name} {searchResults[i].Client.Patronymic} {searchResults[i].Client.Surname}";
                        worksheet.Cells[i + 2, 4].Value = fullName;
                        worksheet.Cells[i + 2, 5].Value = searchResults[i].Date;

                        // Применение формата даты к столбцу
                        ExcelRange dateColumn = worksheet.Cells[i + 2, 5];
                        dateColumn.Style.Numberformat.Format = "MM/DD/YYYY/HH:mm";
                    }

                    worksheet.Column(1).Width = 15;
                    worksheet.Column(2).Width = 40;
                    worksheet.Column(3).Width = 30;
                    worksheet.Column(4).Width = 40;
                    worksheet.Column(5).Width = 20;

                    // Сохранение файла Excel
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        FileInfo file = new FileInfo(saveFileDialog.FileName);
                        package.SaveAs(file);
                        MessageBox.Show("Data exported to Excel successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while exporting data to Excel: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ExportToExcelButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.Trim();
            ComboBoxItem selectedStatusItem = (ComboBoxItem)statusComboBox.SelectedItem;
            string selectedStatus = selectedStatusItem.Content.ToString();

            List<Contract> filteredContracts = ApplyFilters(searchText, selectedStatus);

            ExportSearchResultsToExcel(filteredContracts);
        }

        private List<Contract> ApplyFilters(string searchText, string selectedStatus)
        {
            List<Contract> filteredContracts = contracts;

            if (!string.IsNullOrEmpty(searchText))
            {
                filteredContracts = filteredContracts.Where(contract =>
                    contract.NumberContract.Contains(searchText) ||
                    contract.Service.NameService.Contains(searchText) ||
                    contract.Client.Surname.Contains(searchText) ||
                    contract.Date.ToString().Contains(searchText) ||
                    contract.Status.ToString().Contains(searchText)
                ).ToList();
            }

            if (selectedStatus == "Завершенные")
            {
                filteredContracts = filteredContracts.Where(contract => contract.Status == true).ToList();
            }
            else if (selectedStatus == "Не завершенные")
            {
                filteredContracts = filteredContracts.Where(contract => contract.Status == false).ToList();
            }

            return filteredContracts;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.Trim();
            ComboBoxItem selectedStatusItem = (ComboBoxItem)statusComboBox.SelectedItem;
            string selectedStatus = selectedStatusItem.Content.ToString();

            List<Contract> filteredContracts = ApplyFilters(searchText, selectedStatus);

            DataContext = filteredContracts;
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Contract contract = button.DataContext as Contract;

            if (contract != null)
            {
                contract.Status = true;
                orderManager.CompleteOrder(contract);
                ordersListBox.GetBindingExpression(ListBox.ItemsSourceProperty)?.UpdateTarget();
                MessageBox.Show("Заказ завершен!");
                // Обновление страницы
                RefreshPage();
            }
        }
        private void RefreshPage()
        {
            // Очистка данных на странице
            ordersListBox.ItemsSource = null;

            // Повторная загрузка данных на страницу
            LoadData();
          
        }

        private void LoadData()
        {
            // Загрузка данных на страницу
            ordersListBox.ItemsSource = orderManager.GetContractsIncludingClient();
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)statusComboBox.SelectedItem;
            string selectedStatus = selectedItem.Content.ToString();

            List<Contract> filteredContracts = ApplyFilters(searchTextBox.Text.Trim(), selectedStatus);

            DataContext = filteredContracts;
        }
    }
}

  
