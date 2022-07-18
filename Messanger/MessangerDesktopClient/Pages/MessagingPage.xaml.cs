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
   public partial class MessagingPage : Page
   {
      private int _userId;

      public MessagingPage(int userId)
      {
         InitializeComponent();
         _userId = userId;
      }

      private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
      {
         UsersStackPanel.Children.Add(new UserView("Nikon", "Hello"));
         string message;
         if (e.Key == Key.Enter)
         {
            message = MessageTextBox.Text.ToString();
            int a = 8;
         }
      }
   }
}
