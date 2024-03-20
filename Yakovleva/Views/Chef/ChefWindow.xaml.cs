using Microsoft.EntityFrameworkCore;
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

namespace Yakovleva.Views.Chef
{
    /// <summary>
    /// Interaction logic for ChefWindow.xaml
    /// </summary>
    public partial class ChefWindow : Window
    {
        private YakovlevaContext _context;

        public ChefWindow()
        {
            InitializeComponent();
            _context = new YakovlevaContext();
            LoadOrdersAsync();
        }

        private async void LoadOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Where(o => o.Status == "Принят" || o.Status == "Готовится")
                .ToListAsync();
            OrdersGrid.ItemsSource = orders;
        }

        private void ButtonPrepare_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersGrid.SelectedItem != null && OrdersGrid.SelectedItem is Order selectedOrder)
            {
                selectedOrder.Status = "Готовится";
                _context.SaveChanges();
                LoadOrdersAsync();
            }
            else
            {
                MessageBox.Show("Выберите заказ для изменения статуса.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ButtonReady_Click(object sender, RoutedEventArgs e)
        {

            if (OrdersGrid.SelectedItem != null && OrdersGrid.SelectedItem is Order selectedOrder)
            {
                selectedOrder.Status = "Готов";
                _context.SaveChanges();
                LoadOrdersAsync();
            }
            else
            {
                MessageBox.Show("Выберите заказ для изменения статуса.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
