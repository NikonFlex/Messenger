﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessangerDesktopClient.Models
{
   public class CheckUserResponse
   {
      public bool IsExist { get; set; }
      public int UserId { get; set; }
      public string Messege { get; set; }
   }
}
