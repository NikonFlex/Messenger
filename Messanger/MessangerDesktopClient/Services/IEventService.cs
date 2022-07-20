using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerDesktopClient.Services
{
   interface IEventService
   {
      public delegate void CompanionUserChangeHadler();
      public event CompanionUserChangeHadler OnCompanionUserChange;

      public delegate void CompanionUserSendNewMessageHadler();
      public event CompanionUserSendNewMessageHadler OnCompanionUserSendNewMessage;
   }
}
