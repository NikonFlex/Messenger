using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerDesktopClient.Models
{
   public class Message
   {
      public string Value { get; set; }
      public int FromId { get; set; }
      public int ToId { get; set; }
      public DateTime SendTime { get; set; }
   }
}
