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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private readonly YakovlevaContext _context;
        public event EventHandler EmployeeAdded;
        
        public AddEmployeeWindow()
        {
            InitializeComponent();
            _context = new YakovlevaContext();
            LoadRoles();
        }

        private void LoadRoles()
        {
            try
            {
                RoleComboBox.ItemsSource = _context.Roles.ToList();
                RoleComboBox.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке ролей: " + ex.Message);
            }
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                string name = NameTextBox.Text;
                string surname = SurnameTextBox.Text;
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Text;
                Role selectedRole = RoleComboBox.SelectedItem as Role;

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) ||
                    string.IsNullOrWhiteSpace(password) || selectedRole == null)
                {
                    MessageBox.Show("Заполните все поля.");
                    return;
                }

                User newUser = new User
                {
                    Name = name,
                    Surname = surname,
                    Login = login.ToLower(),
                    Password = password,
                    RoleId = selectedRole.Id,
                    Status = "Active"
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                EmployeeAdded?.Invoke(this, EventArgs.Empty);

                MessageBox.Show("Новый сотрудник успешно добавлен.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникла ошибка: " + ex.Message);
            }
        }
    }
}
