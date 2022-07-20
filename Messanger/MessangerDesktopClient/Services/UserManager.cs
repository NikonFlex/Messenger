using MessangerDesktopClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MessangerDesktopClient.Services
{
   public class UserManager : IEventService
   {
      private Timer _timer = new() { Interval = 5000 };

      private event IEventService.CompanionUserChangeHadler _onCompanionUserChange;
      private event IEventService.CompanionUserSendNewMessageHadler _onCompanionUserSendNewMessage;

      public User CurrentUser { get; private set; }
      public User CompanionUser { get; private set; }
      public DateTime LastMessegeTimeFromCompanion { get; private set; }

      public UserManager() 
      {
         ServicesContainer.Register<IEventService>(this);
      }

      event IEventService.CompanionUserChangeHadler IEventService.OnCompanionUserChange
      {
         add => _onCompanionUserChange += value;
         remove => _onCompanionUserChange -= value;
      }

      event IEventService.CompanionUserSendNewMessageHadler IEventService.OnCompanionUserSendNewMessage
      {
         add => _onCompanionUserSendNewMessage += value;
         remove => _onCompanionUserSendNewMessage -= value;
      }

      private void SetTimer()
      {
         _timer.Elapsed += OnTimedEvent;
         _timer.AutoReset = true;
         _timer.Enabled = true;
      }

      private void OnTimedEvent(object source, ElapsedEventArgs e)
      {
         _onCompanionUserSendNewMessage?.Invoke();
      }


      public void SetCurrentUser(User user) => CurrentUser = user;

      public void SetCompanionUser(User user)
      {
         CompanionUser = user;
         LastMessegeTimeFromCompanion = DateTime.Now;
         _onCompanionUserChange?.Invoke();
         SetTimer();
      }

      public void SetLastMessageTimeFromCompanion(DateTime time) => LastMessegeTimeFromCompanion = time;
   }
}
