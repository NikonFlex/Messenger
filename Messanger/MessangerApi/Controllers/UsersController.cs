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
   [Route("users")]
   public class UsersController : Controller
   {
      private MessangerDbContext _ctx;

      public UsersController(MessangerDbContext ctx)
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

      [HttpGet("CheckUser")]
      public bool IsUserExist(string login, string password)
      {
         if (_ctx.Users.FirstOrDefault(u => u.Login == login && u.Password == password) is not null)
            return true;
         return false;
      }

      [HttpPost]
      public void CreateNewUser([FromBody]UserDTO newUser)
      {
         if (_ctx.Users.FirstOrDefault(u => u.Login == newUser.Login) is not null) 
            return;

         _ctx.Users.Add(new User() { Login = newUser.Login, Password = newUser.Password });
         _ctx.SaveChanges();
      }

      //[HttpPut("{id}")]
      //public void ChangeUserName(int id, [FromBody] string value)
      //{
      //   User user = _ctx.Users.FirstOrDefault(u => u.Id == id);
      //   if (user is null)
      //      return;

      //   user.Name = value;
      //   _ctx.SaveChanges();
      //}

      //[HttpDelete("{id}")]
      //public void DeleteUser(int id)
      //{
      //   var user = _ctx.Users.FirstOrDefault(u => u.Id == id);
      //   _ctx.Users.Remove(user);

      //   _ctx.SaveChanges();
      //}
   }
}
