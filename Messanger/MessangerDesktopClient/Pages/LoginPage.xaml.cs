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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MessangerDesktopClient.Pages
{
   public partial class LoginPage : Page
   {
      public LoginPage()
      {
         InitializeComponent();
      }

      private void LoginButton_Click(object sender, RoutedEventArgs e)
      {
         Models.User user = new(LoginTextBox.Text, PasswordTextBox.Text);
         MessageLabel.Content = "Verifying ...";
         MainWindow.CheckUserExisting(user).ContinueWith(res =>
         {
            Dispatcher.Invoke(new Action(() => tryLogin(res.Result)));
         });
      }

      private void tryLogin(bool apiAnswer)
      {
         if (apiAnswer)
            NavigationService.Navigate(new MessagingPage());
         else
            MessageLabel.Content = "Incorrect Login or Password";
      }

      private void LoginTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         LoginTextBox.Text = "";
         LoginTextBox.IsReadOnly = false;
      }

      private void PasswordTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         PasswordTextBox.Text = "";
         PasswordTextBox.IsReadOnly = false;
      }
   }
}
