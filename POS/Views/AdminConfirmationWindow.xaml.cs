using POS.DataAccess;
using POS.Models;
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

namespace POS.Views
{
    /// <summary>
    /// Interaction logic for AdminConfirmationWindow.xaml
    /// </summary>
    public partial class AdminConfirmationWindow : Window
    {
        public bool IsConfirmed { get; set; }

        public AdminConfirmationWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = true;
            DialogResult = true;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                        IsConfirmed = true;
                        DialogResult = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("This is user");
                    }

                    this.Close();
                }
                else
                {
                    // Failed login
                    MessageBox.Show("Invalid username or password.");
                }
            }
    
        }
    }
}
