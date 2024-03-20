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
using Yakovleva.Models;

namespace Yakovleva.Views.Waiter
{
    /// <summary>
    /// Interaction logic for AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        private readonly YakovlevaContext _context;
        public AddOrderWindow()
        {
            InitializeComponent();
            _context = new YakovlevaContext();

            var products = _context.Products.ToList();
            ProductsListBox.ItemsSource = products;
        }

        private void CreateNewOrder_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
