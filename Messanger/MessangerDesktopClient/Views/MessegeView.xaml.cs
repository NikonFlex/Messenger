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

namespace MessangerDesktopClient.Views
{
   public partial class MessegeView : UserControl
   {
      private Models.Message _message;

      public MessegeView(string userName, Models.Message messege)
      {
         InitializeComponent();
         _message = messege;
         UserNameLabel.Content = userName;
         MessageLabel.Content = messege.Value;
      }
   }
}
