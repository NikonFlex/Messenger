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
   public class CheckUserResponse
   {
      public bool IsExist { get; set; }
      public UserDTO User { get; set; } 
      public string Messege { get; set; }
   }

   [Route("")]
   public class DefaultController : Controller
   {
      [HttpGet]
      public string Get()
      {
         return "Api is working";
      }

   }

   [Route("users")]
   public class UsersController : Controller
   {
      private MessangerDbContext _ctx;

      public UsersController(MessangerDbContext ctx)
      {
         _ctx = ctx;
      }

      [HttpGet("GetAllUsers")]
      public List<UserDTO> GetAllUsers()
      {
         return _ctx.Users.Select(user => new UserDTO()
         {
            Id = user.Id,
            Login = user.Login
         }).ToList();
      }

      //[HttpGet("GetAllUsers")]
      //public string GetAllUsers()
      //{
      //   return "users";
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
      public ActionResult IsUserExist(string login, string password)
      {
         var user = _ctx.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
         if (user is not null)
         {
            return new JsonResult(new CheckUserResponse() 
            { 
               IsExist = true, 
               User = new UserDTO()
               {
                  Id = user.Id,
                  Login = user.Login
               },
               Messege = "Successfully verified"
            });
         }
         else
         {
            return new JsonResult(new CheckUserResponse()
            {
               IsExist = false,
               User = new UserDTO()
               {
                  Id = -1,
                  Login = ""
               },
               Messege = "No such user or incorrect verify data"
            });
         }
      }

      //[HttpPost]
      //public void CreateNewUser([FromBody] User newUser)
      //{
      //   if (_ctx.Users.FirstOrDefault(u => u.Login == newUser.Login) is not null)
      //      return;

      //   _ctx.Users.Add(new User() { Login = newUser.Login, Password = newUser.Password });
      //   _ctx.SaveChanges();
      //}

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
