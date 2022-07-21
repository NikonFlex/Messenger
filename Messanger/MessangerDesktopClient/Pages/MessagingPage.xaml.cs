using MessangerDesktopClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
      public MessagingPage()
      {
         InitializeComponent();
         createUsersListView();
      }

      private void createUsersListView()
      {
         MainWindow.GetAllUsers().ContinueWith(response =>
         {
            foreach (Models.User user in response.Result)
               if (user.Id != ServicesContainer.Get<UserManager>().CurrentUser.Id)
                  Dispatcher.Invoke(new Action(() => createUserView(user)));
         });

         ServicesContainer.Get<IEventService>().OnCompanionUserChange += startChatWithUser;
         ServicesContainer.Get<IEventService>().OnCompanionUserSendNewMessage += updateMessagesList;
      }

      private void createUserView(Models.User user)
      {
         UsersStackPanel.Children.Add(new Views.UserView(user, "Hello"));
      }

      private void startChatWithUser()
      {
         CompanionNameLabel.Content = ServicesContainer.Get<UserManager>().CompanionUser.Login;
         MessageTextBox.IsReadOnly = false;
         MessagesListBox.Items.Clear();

         //TODO: Load last messages beetween users
      }

      private void updateMessagesList()
      {
         MainWindow.GetLastMessagesFromCompanion().ContinueWith(response =>
         {
            if (response.Result.Count > 0)
            {
               foreach (Models.Message message in response.Result)
                  Dispatcher.Invoke(new Action(() => createMessageView(ServicesContainer.Get<UserManager>().CompanionUser.Login, message)));

               ServicesContainer.Get<UserManager>().SetLastMessageTimeFromCompanion(response.Result.Last().SendTime.ToUniversalTime());
            }
         });
      }

      private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.Key == Key.Enter)
         {
            sendMessage(MessageTextBox.Text.ToString());
            MessageTextBox.Text = "";
         }
      }

      private void sendMessage(string value)
      {
         var newMessage = new Models.Message()
         {
            Value = value,
            FromId = ServicesContainer.Get<UserManager>().CurrentUser.Id,
            ToId = ServicesContainer.Get<UserManager>().CompanionUser.Id
         };
         MainWindow.SendNewMessage(newMessage);

         createMessageView(ServicesContainer.Get<UserManager>().CurrentUser.Login, newMessage);
      }

      private void createMessageView(string senderName, Models.Message message)
      {
         MessagesListBox.Items.Add(new Views.MessegeView(senderName, message));
      }
   }
}
