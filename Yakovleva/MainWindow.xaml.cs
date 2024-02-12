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
using Yakovleva.Views.Admin;
using Yakovleva.Views.Waiter;
using Yakovleva.Views.Chef;

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
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            using (var context = new YakovlevaContext())
            {
                var user = context.Users.Include(u => u.Role).FirstOrDefault(u => u.Login == username && u.Password == password);

                if (user != null)
                {
                    switch (user.Role.Name) 
                    {
                        case "Администратор":
                            NavigateToWindow(new AdminWindow());
                            break;
                        case "Официант":
                            NavigateToWindow(new WaiterWindow());
                            break;
                        case "Повар":
                            NavigateToWindow(new ChefWindow());
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

        private void NavigateToWindow(Window window)
        {
            window.Show();
            Close();
        }
    }
}