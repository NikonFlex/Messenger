using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerDesktopClient.Models
{
   public class User
   {
      public string Login { private set; get; }
      public string Password { private set; get; }

      public User(string login, string password)
      {
         Login = login;
         Password = password;
      }
   }
}
