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
        private readonly YakovlevaContext _context;

        private Int32 user;

        public WaiterWindow(Int32 currentUser = 1)
        {
            InitializeComponent();
            _context = new YakovlevaContext();
            this.user = currentUser;
            LoadOrdersAsync();

            CreateOrderBtn.Click += (sender, e) =>
            {
                AddOrderWindow addOrderWindow = new AddOrderWindow(this.user);
                addOrderWindow.ShowDialog();
                LoadOrdersAsync();
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

        private void ButtonPaid_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = WaiterGrid.SelectedItem as Order;
            if (selectedOrder != null && selectedOrder.Status == "Готов")
            {
                selectedOrder.Status = "Оплачен";
                _context.SaveChanges();
                MessageBox.Show("Статус заказа успешно сменен на \"Оплачен\"", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadOrdersAsync();
            }
            else
            {
                MessageBox.Show("Вы можете менять статус только для заказов со статусами \"Готов\".", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
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
