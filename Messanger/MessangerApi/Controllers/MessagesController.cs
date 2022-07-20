using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessangerApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MessangerApi.DTOs;

namespace MessangerApi.Controllers
{
   [Route("messages")]
   public class MessagesController : Controller
   {
      private MessangerDbContext _ctx;

      public MessagesController(MessangerDbContext ctx)
      {
         _ctx = ctx;
      }

      //[HttpGet]
      //public List<User> GetAllUsers()
      //{
      //   var users = _ctx.Users.ToList();
      //   return users;
      //}

      //[HttpGet("{id}")]
      //public string GetUserById(int id)
      //{
      //   User? user = _ctx.Users.FirstOrDefault(u => u.Id == id);
      //   if (user is null)
      //      return "";

      //   return user.Name;
      //}

      [HttpGet("GetNewMessagesFromUser")]
      public List<MessageDTO> GetNewMessagesFromUser(int fromId, int toId, long lastMsgTime)
      {
         //TODO: exclude AsEnumerable
         return _ctx.Messages.Where(msg => msg.FromId == fromId && msg.ToId == toId).
                                       Select(msg => new MessageDTO()
                                       {
                                          FromId = fromId,
                                          ToId = toId,
                                          Value = msg.Value,
                                          SendTime = msg.SendTime,
                                       }).
                                       AsEnumerable().Where(msg => msg.SendTime.Ticks > lastMsgTime).
                                       ToList();

      }

      [HttpPost]
      public void CreateNewMessage([FromBody] MessageDTO newMessage)
      {
         _ctx.Messages.Add(new Message()
         {
            Value = newMessage.Value,
            FromId = newMessage.FromId,
            ToId = newMessage.ToId,
            SendTime = DateTime.Now,
         });
         _ctx.SaveChanges();
      }

      //   [HttpPut("{id}")]
      //   public void ChangeUserName(int id, [FromBody] string value)
      //   {
      //      User user = _ctx.Users.FirstOrDefault(u => u.Id == id);
      //      if (user is null)
      //         return;

      //      user.Name = value;
      //      _ctx.SaveChanges();
      //   }

      //   [HttpDelete("{id}")]
      //   public void DeleteUser(int id)
      //   {
      //      var user = _ctx.Users.FirstOrDefault<User>(u => u.Id == id);
      //      _ctx.Users.Remove(user);

      //      _ctx.SaveChanges();
      //   }
      //}
   }
}
