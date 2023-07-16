using System.Windows;
using POS.DataAccess;
using POS.Models;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace POS.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

            Window window = Window.GetWindow(this); 
            window.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new POSDbContext())
            {
                User user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    if (user.Role == "Admin")
                    {
                        AdminMainWindow adminMainWindow = new AdminMainWindow();
                        adminMainWindow.Show();
                    }
                    else
                    {
                        NormalUserMainWindow normalUserMainWindow = new NormalUserMainWindow();
                        normalUserMainWindow.Show();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.");
                }
            }
        }


        private void PasswordBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }
    }
}
