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

namespace WDCApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для MoreWindow.xaml
    /// </summary>
    public partial class MoreWindow : Window
    {
        public MoreWindow()
        {
            InitializeComponent();
        }
        public void SetData(string serviceName, double? price, string description)
        {
            // Вывод данных в элементы управления окна
            serviceNameLabel.Text = serviceName;
           
            descriptionLabel.Text = description;
        }
    }
}
