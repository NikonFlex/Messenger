using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.WebUtilities;
using System.Diagnostics;

namespace MessangerDesktopClient
{
   public partial class MainWindow : Window
   { 
      private static HttpClient _client = new HttpClient();

      public MainWindow()
      {
         InitializeComponent();
         Services.ServicesContainer.Register(new Services.UserManager());
         MainFrame.Content = new Pages.LoginPage();

         _client.BaseAddress = new Uri("https://localhost:7025/");
         _client.DefaultRequestHeaders.Accept.Clear();
         _client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));
      }
      
      public static async Task<Models.CheckUserResponse> CheckUserExisting(string login, string password)
      {
         //TODO: ["login"] = user.Login, ["password"] = user.Password
         var query = new Dictionary<string, string>()
         {
            ["login"] = login,
            ["password"] = password
         };
         var uri = QueryHelpers.AddQueryString(_client?.BaseAddress?.ToString() + "users/CheckUser", query);

         var response = await _client.GetAsync(uri);
         return await response.Content.ReadAsAsync<Models.CheckUserResponse>();
      }

      public static async void SendNewMessage(Models.Message message)
      {
         var uri = _client?.BaseAddress?.ToString() + "messages";
         var response = await _client.PostAsJsonAsync(uri, message);
      }

      public static async Task<List<Models.User>> GetAllUsers()
      {
         var uri = _client?.BaseAddress?.ToString() + "users/GetAllUsers";
         var response = await _client.GetAsync(uri);
         return await response.Content.ReadAsAsync<List<Models.User>>();
      }

      public static async Task<List<Models.Message>> GetLastMessagesFromCompanion()
      {
         //https://localhost:7025/messages/GetNewMessagesFromUser?fromId=12&toId=14&lastMsgTime=2022-07-20%2000%3A30%3A31.0724729
         var a = Services.ServicesContainer.Get<Services.UserManager>().LastMessegeTimeFromCompanion.ToFileTimeUtc().ToString();
         var query = new Dictionary<string, string>()
         {
            ["fromId"] = Services.ServicesContainer.Get<Services.UserManager>().CompanionUser.Id.ToString(),
            ["toId"] = Services.ServicesContainer.Get<Services.UserManager>().CurrentUser.Id.ToString(),
            ["lastMsgTime"] = Services.ServicesContainer.Get<Services.UserManager>().LastMessegeTimeFromCompanion.Ticks.ToString()
         };
         var uri = QueryHelpers.AddQueryString(_client?.BaseAddress?.ToString() + "messages/GetNewMessagesFromUser", query);

         var response = await _client.GetAsync(uri);
         return await response.Content.ReadAsAsync<List<Models.Message>>();
      }
   }
}
