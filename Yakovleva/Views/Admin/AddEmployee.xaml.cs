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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private readonly YakovlevaContext _context;
        public event EventHandler EmployeeAdded;
        
        public AddEmployee()
        {
            InitializeComponent();
            _context = new YakovlevaContext();
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Text;
            string roleName = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(name)||string.IsNullOrWhiteSpace(surname)||
                string.IsNullOrWhiteSpace(password)||string.IsNullOrWhiteSpace(roleName))
            {
                MessageBox.Show("Заполните все поля.");
                return;
            }

            Role role = _context.Roles.FirstOrDefault(r => r.Name == roleName);
            if (role == null) 
            {
                MessageBox.Show("Выбранная роль не найдена.");
                return;
            }

            User newUser = new User
            {
                Name = name,
                Surname = surname,
                Login = login.ToLower(),
                Password = password,
                RoleId = role.Id,
                Status = "Active"
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            EmployeeAdded?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("Новый сотрудник успешно добавлен.");
            this.Close();
        }
    }
}
