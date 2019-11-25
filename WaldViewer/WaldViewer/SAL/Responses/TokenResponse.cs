using System;
using System.Collections.Generic;
using System.Text;

namespace WaldViewer.SAL.Responses
{
  public class TokenResponse
  {
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public string RefreshToken { get; set; }
  }
}
