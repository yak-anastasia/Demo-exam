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
    /// Логика взаимодействия для ShiftsWindow.xaml
    /// </summary>
    public partial class ShiftsWindow : Window
    {
        private readonly YakovlevaContext _context;
        private NewShiftWindow _newShiftWindow;

        public ShiftsWindow()
        {
            InitializeComponent();
            _context = new YakovlevaContext();
            LoadShiftsAsync();

            backButton.Click += (sender, e) =>
            {
                new AdminWindow().Show();
                Close();
            };

            addButton.Click += NewShiftButton_Click;
        }

        private async Task LoadShiftsAsync()
        {
            try
            {
                var shifts = await _context.Shifts
                    .Include(s => s.ShiftUsers)
                        .ThenInclude(su => su.User)
                    .OrderByDescending(s => s.StartShift)
                    .ToListAsync();

                ShiftsGrid.ItemsSource = shifts;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void NewShiftButton_Click(object sender, RoutedEventArgs e)
        {
            var newShiftWindow = new NewShiftWindow();
            newShiftWindow.ShowDialog();
            LoadShiftsAsync();
        }
    }
}
