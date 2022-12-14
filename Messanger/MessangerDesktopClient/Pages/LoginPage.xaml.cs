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
         MessageLabel.Content = "Verifying ...";
         MainWindow.CheckUserExisting(LoginTextBox.Text, PasswordTextBox.Text).ContinueWith(res =>
         {
            Dispatcher.Invoke(new Action(() => tryLogin(res.Result)));
         });
      }

      private void tryLogin(Models.CheckUserResponse apiAnswer)
      {
         if (apiAnswer.IsExist)
         {
            NavigationService.Navigate(new MessagingPage());
            Services.ServicesContainer.Get<Services.UserManager>().SetCurrentUser(apiAnswer.User);
         }
         else
            MessageLabel.Content = apiAnswer.Messege;
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
