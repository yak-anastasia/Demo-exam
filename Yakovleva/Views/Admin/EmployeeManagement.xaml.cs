using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EmployeeManagement.xaml
    /// </summary>
    public partial class EmployeeManagement : Window
    {
        private readonly YakovlevaContext _context;
        private ObservableCollection<User> _employees;
        
        public EmployeeManagement()
        {
            InitializeComponent();

            _context = new YakovlevaContext();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            _employees = new ObservableCollection<User>(_context.Users.Include(u => u.Role).Where(u => u.Status == "Active").ToList());
            EmployeesGrid.ItemsSource = _employees;
        }

        private void FireEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeesGrid.SelectedItem != null)
            {
                var selectedEmployee = EmployeesGrid.SelectedItem as User;

                using (var context = new YakovlevaContext())
                {
                    var employeeToUpdate = context.Users.FirstOrDefault(u => u.Id == selectedEmployee.Id);
                    if (employeeToUpdate != null)
                    {
                        employeeToUpdate.Status = "Inactive";
                        context.SaveChanges();
                        _employees.Remove(selectedEmployee);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для увольнения");
            }
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee addEmployee = new AddEmployee();
            addEmployee.ShowDialog();
            LoadEmployees();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }
    }
}
