﻿using System;
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
    /// Логика взаимодействия для NewShiftWindow.xaml
    /// </summary>
    public partial class NewShiftWindow : Window
    {
        private readonly YakovlevaContext _context;
        public NewShiftWindow()
        {
            InitializeComponent();

            _context = new YakovlevaContext();

            var employess = _context.Users.ToList();
            EmployeesListBox.ItemsSource = employess;
            HourCmb(StartHourCmb);
            HourCmb(EndHourCmb);
            MinuteCmb(StartMinuteCmb);
            MinuteCmb(EndMinuteCmb);
        }

        private void HourCmb(ComboBox combobox)
        {
            for (int i = 0; i <= 24; i++)
            {
                combobox.Items.Add(new ComboBoxItem { Content = i.ToString() });
            }
        }

        private void MinuteCmb(ComboBox combobox)
        {
            for (int i = 0; i <= 60; i++)
            {
                combobox.Items.Add(new ComboBoxItem { Content = i.ToString() });
            }
        }

        private async void CreateShiftButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedEmployees = EmployeesListBox.SelectedItems.Cast<User>().ToList();

                var startShiftDate = StartDatePicker.SelectedDate ?? DateTime.Today;
                var startShiftTime = TimeSpan.FromHours(int.Parse(((ComboBoxItem)StartHourCmb.SelectedItem).Content.ToString())) +
                    TimeSpan.FromMinutes(int.Parse(((ComboBoxItem)StartMinuteCmb.SelectedItem).Content.ToString()));
                var startShiftDateTime = startShiftDate.Add(startShiftTime);

                var endShiftDate = EndDatePicker.SelectedDate ?? DateTime.Today;
                var endShiftTime = TimeSpan.FromHours(int.Parse(((ComboBoxItem)EndHourCmb.SelectedItem).Content.ToString())) +
                    TimeSpan.FromMinutes(int.Parse(((ComboBoxItem)EndMinuteCmb.SelectedItem).Content.ToString()));
                var endShiftDateTime = endShiftDate.Add(endShiftTime);

                var newShift = new Shift
                {
                    StartShift = startShiftDateTime,
                    EndShift = endShiftDateTime,
                    StatusShift = "Новая смена"
                };

                foreach (var employee in selectedEmployees)
                {
                    newShift.ShiftUsers.Add(new ShiftUser { User = employee });
                }

                _context.Shifts.Add(newShift);
                await _context.SaveChangesAsync();

                MessageBox.Show("Смена успешно добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении смены: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
