using System.Text;
using System.Windows;
using Yakovleva.Models;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace Yakovleva
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Получаем введенные данные из TextBox и PasswordBox
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new YakovlevaContext())
            {
                var user = context.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == username && u.Password == password);
                if (user != null)
                {
                    switch (user.Role.Name) 
                    {
                        case "Админ":
                            Views.Admin.AdminWindow adminWindow = new Views.Admin.AdminWindow();
                            adminWindow.Show();
                            this.Close();
                            break;
                        case "Официант":
                            Views.Waiter.WaiterWindow waiterWindow = new Views.Waiter.WaiterWindow();
                            waiterWindow.Show();
                            this.Close();
                            break;
                        case "Повар":
                            Views.Chef.ChefWindow chefWindow = new Views.Chef.ChefWindow();
                            chefWindow.Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Неизвестная роль пользователя");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }
    }
}