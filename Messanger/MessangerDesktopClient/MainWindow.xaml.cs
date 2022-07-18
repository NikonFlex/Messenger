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
         MainFrame.Content = new Pages.LoginPage();
         _client.BaseAddress = new Uri("https://localhost:7025/");
         _client.DefaultRequestHeaders.Accept.Clear();
         _client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));
      }

      public static async Task<bool> CheckUserExisting(Models.User user)
      {
         //TODO: ["login"] = user.Login, ["password"] = user.Password
         var query = new Dictionary<string, string>()
         {
            ["login"] = "Nikon",
            ["password"] = "NikonPass"
         };
         var uri = QueryHelpers.AddQueryString(_client.BaseAddress.ToString() + "users/CheckUser", query);

         var response = await _client.GetAsync(uri);
         return await response.Content.ReadAsAsync<bool>();
      }
   }
}
