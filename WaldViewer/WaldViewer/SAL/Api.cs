using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WaldViewer.BL;
using WaldViewer.Common;
using WaldViewer.Exceptions;

namespace WaldViewer.SAL
{
  internal static class Api
  {
    private static void AuthenticatedCheck()
    {
      if (!Authentication.IsAuthenticated)
      {
        throw new InvalidOperationException("App is not authenticated");
      }
    }

    public static async Task<NewsItem[]> GetItems()
    {
      AuthenticatedCheck();
      return await Getdata<NewsItem[]>("Waldnet/");
      
    }

    private static async Task<TResult> Getdata<TResult>(string endpoint)
    {
      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authentication.Token}");

        var response = await client.GetAsync($"{Constants.ApiHost}{endpoint}");
        var responseJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
          return JsonConvert.DeserializeObject<TResult>(responseJson);
        }

        throw new CouldNotFetchDataException(response.StatusCode, responseJson);
      }
    }
  }
}
