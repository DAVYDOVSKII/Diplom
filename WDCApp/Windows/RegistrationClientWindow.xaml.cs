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
using System.Windows.Shapes;
using WDCApp.Entities;

namespace WDCApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationClientWindow.xaml
    /// </summary>
    public partial class RegistrationClientWindow : Window
    {
        public RegistrationClientWindow()
        {
            InitializeComponent();
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получение данных о клиенте из текстовых полей
                string surname = SurnameTextBox.Text;
                string name = NameTextBox.Text;
                string patronymic = PatronymicTextBox.Text;
                DateTime dateOfBirth = DateTime.Parse(DateOfBirthTextBox.Text);
                string email = EmailTextBox.Text;
                string phoneNumber = PhoneNumberTextBox.Text;
                int passportSeries = int.Parse(PassportSeriesTextBox.Text);
                int passportNumber = int.Parse(PassportNumberTextBox.Text);

                // Создание объекта клиента
                var client = new Entities.Client
                {
                    Surname = surname,
                    Name = name,
                    Patronymic = patronymic,
                    DateOfBirth = dateOfBirth,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    PassportSeries = passportSeries,
                    PassportNumber = passportNumber
                };

                // Добавление клиента в базу данных
                App.Context.Client.Add(client);
                App.Context.SaveChanges();


                // Очистка полей после добавления клиента
                ClearFields();
            }
            catch (FormatException)
            {
                // Обработка исключения, если введены некорректные данные формата
 
                MessageBox.Show("Некорректные данные формата. Проверьте вводимые значения.", "Ошибка");
            }
            catch (Exception )
            {
                // Обработка других исключений, которые могут возникнуть при добавлении клиента в базу данных
                MessageBox.Show("Произошла ошибка при добавлении клиента. Пожалуйста, попробуйте еще раз.", "Ошибка");
            }
        }

        private void ClearFields()
        {
            SurnameTextBox.Text = string.Empty;
            NameTextBox.Text = string.Empty;
            PatronymicTextBox.Text = string.Empty;
            DateOfBirthTextBox.Text = string.Empty;
            EmailTextBox.Text = string.Empty;
            PhoneNumberTextBox.Text = string.Empty;
            PassportSeriesTextBox.Text = string.Empty;
            PassportNumberTextBox.Text = string.Empty;
        }
    }
}
