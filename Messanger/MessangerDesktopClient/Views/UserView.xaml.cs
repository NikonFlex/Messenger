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
using MessangerDesktopClient.Services;

namespace MessangerDesktopClient.Views
{
   public partial class UserView : UserControl
   {
      private Models.User _user;

      public UserView(Models.User user, string lastMessage)
      {
         InitializeComponent();
         _user = user;
         UserNameLabel.Content = user.Login;
         LastMessageLabel.Content = lastMessage;
      }

      private void userViewControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
      {
         ServicesContainer.Get<UserManager>().SetCompanionUser(_user);
      }
   }
}
