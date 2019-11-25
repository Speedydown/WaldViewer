using System;
using System.Collections.Generic;
using System.Text;

namespace WaldViewer.SAL.Requests
{
  public class RenewTokenRequest
  {
    public string RefreshToken { get; set; }
    public string GrantType { get; set; }
  }
}
