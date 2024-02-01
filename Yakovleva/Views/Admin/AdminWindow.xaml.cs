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

namespace Yakovleva.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void ManageEmployeesBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeManagement employeeManagement = new EmployeeManagement();
            employeeManagement.Show();
            this.Close();
        }

        private void ManageOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            Orders ordersManagement = new Orders();
            ordersManagement.Show();
            this.Close();
        }

        private void ManageShiftsBtn_Click(object sender, RoutedEventArgs e)
        {
            Shifts shiftsManagement = new Shifts();
            shiftsManagement.Show();
            this.Close();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
