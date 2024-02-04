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

namespace Yakovleva.Views.Admin
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        private readonly YakovlevaContext _context;

        public Orders()
        {
            InitializeComponent();
            _context = new YakovlevaContext();
            LoadOrdersAsync();
            BackBtn.Click += (sender, e) =>
            {
                AdminWindow mainWindow = new AdminWindow();
                mainWindow.Show();
                this.Close();
            };
        }

        public List<Order> Orders { get; set; } = new List<Order>();
        
        private async void LoadOrdersAsync()
        {
            Orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .ToListAsync();
            OrdersGrid.ItemsSource = Orders;
        }
    }
}
