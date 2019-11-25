using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WaldViewer.Common;
using WaldViewer.Exceptions;
using WaldViewer.SAL.Requests;
using WaldViewer.SAL.Responses;
using Xamarin.Essentials;

namespace WaldViewer.SAL
{
  public class Authentication
  {
    private static ISettings Settings => CrossSettings.Current;
    public static bool IsAuthenticated { get; private set; }

    public static bool IsTokenValid => IsTokenSet && ExpiresAt >= DateTime.Now;
    public static bool IsTokenSet => Token != null;

    public static string Email { get => Settings.GetValueOrDefault(nameof(Email), null); set => Settings.AddOrUpdateValue(nameof(Email), value); }
    public static string Password { get => Settings.GetValueOrDefault(nameof(Password), null); set => Settings.AddOrUpdateValue(nameof(Password), value); } 
    public static string Token { get => Settings.GetValueOrDefault(nameof(Token), null); set => Settings.AddOrUpdateValue(nameof(Token), value); }
    public static DateTime ExpiresAt { get => Settings.GetValueOrDefault(nameof(ExpiresAt), DateTime.MinValue); set => Settings.AddOrUpdateValue(nameof(ExpiresAt), value); }
    public static string RefreshToken { get => Settings.GetValueOrDefault(nameof(RefreshToken), null); set => Settings.AddOrUpdateValue(nameof(RefreshToken), value); }

    

    public static async Task<bool> Authenticate()
    {
      if (IsTokenSet && !IsTokenValid)
      {
        return await RenewToken();
      }

      if (IsAuthenticated)
      {
        return true;
      }

      if (!IsTokenSet)
      {
        return await RegisterApp();
      }

      IsAuthenticated = true;

      return true;
    }

    public static async Task<bool> Login()
    {
      try
      {
        var request = new LoginRequest
        {
          GrandType = "Password",
          Email = Email,
          Password = Password,
        };

        var response = await PostData<TokenResponse>("OAuth/Register/", request);

        if (response != null)
        {
          Token = response.Token;
          RefreshToken = response.RefreshToken;
          ExpiresAt = response.ExpiresAt;

          IsAuthenticated = true;
        }

        return response != null;

      }
      catch (CouldNotFetchDataException)
      {
        return false;
      }
    }

    private static async Task<bool> RenewToken()
    {
      try
      {
        var request = new RenewTokenRequest
        {
          RefreshToken = RefreshToken,
          GrantType = "Refresh Token"
        };

        var response = await PostData<TokenResponse>("/OAuth//Login", request);

        if (response != null)
        {
          Token = response.Token;
          RefreshToken = response.RefreshToken;
          ExpiresAt = response.ExpiresAt;

          IsAuthenticated = true;
        }

        return response != null;

      }
      catch (CouldNotFetchDataException)
      {
        return false;
      }
    }

    private static async Task<bool> RegisterApp()
    {
      try
      {
        var request = new RegisterRequest
        {
          Name = DeviceInfo.Model,
          Email = $"Waldnet{Guid.NewGuid().ToString().Replace("-", string.Empty)}@app.nl",
          password = Guid.NewGuid().ToString().Replace("-", "A"),
        };

        var response = await PostData<TokenResponse>("OAuth/Register/", request);

        if (response != null)
        {
          Token = response.Token;
          RefreshToken = response.RefreshToken;
          ExpiresAt = response.ExpiresAt;

          IsAuthenticated = true;
        }

        Email = request.Email;
        Password = request.Email;

        return response != null;

      }
      catch (CouldNotFetchDataException)
      {
        return false;
      }
    }

    private static async Task<TResult> PostData<TResult>(string endpoint, object request) where TResult : class, new()
    {
      try
      {
        var jsonRequest = JsonConvert.SerializeObject(request, Formatting.Indented);

        using (var client = new HttpClient())
        {
          var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

          var response = await client.PostAsync(Constants.ApiHost + endpoint, content);
          var responseJson = await response.Content.ReadAsStringAsync();

          if (response.IsSuccessStatusCode)
          {
            return JsonConvert.DeserializeObject<TResult>(responseJson);
          }
          else
          {
            throw new CouldNotFetchDataException(response.StatusCode, responseJson);
          }
        }
      }
      catch (Exception ex)
      {
        if (ex is CouldNotFetchDataException)
        {
          throw;
        }

        //TODO appcenter

        return null;
      }
      
    }
  }
}
