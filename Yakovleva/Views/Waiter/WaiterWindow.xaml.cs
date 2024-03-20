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

namespace Yakovleva.Views.Waiter
{
    /// <summary>
    /// Interaction logic for WaiterWindow.xaml
    /// </summary>
    public partial class WaiterWindow : Window
    {
        private YakovlevaContext _context;

        public WaiterWindow()
        {
            InitializeComponent();
            _context = new YakovlevaContext();
            LoadOrdersAsync();

            CreateOrderBtn.Click += (sender, e) =>
            {
                AddOrderWindow addOrderWindow = new AddOrderWindow();
                addOrderWindow.Show();
                this.Close();
            };
        }

        private async void LoadOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .ToListAsync();
            WaiterGrid.ItemsSource = orders;
        }
        private void ButtonAdopted_Click(object sender, RoutedEventArgs e)
        {
            if (WaiterGrid.SelectedItem != null && WaiterGrid.SelectedItem is Order selectedOrder)
            {
                selectedOrder.Status = "Принят";
                _context.SaveChanges();
                LoadOrdersAsync();
            }
            else
            {
                MessageBox.Show("Выберите заказ для изменения статуса.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void ButtonPaid_Click(object sender, RoutedEventArgs e)
        {
            if (WaiterGrid.SelectedItem != null && WaiterGrid.SelectedItem is Order selectedOrder)
            {
                selectedOrder.Status = "Оплачен";
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
