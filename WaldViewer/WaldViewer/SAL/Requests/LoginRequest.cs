using System;
using System.Collections.Generic;
using System.Text;

namespace WaldViewer.SAL.Requests
{
  public class LoginRequest
  {
    public string Email { get; set; }
    public string Password { get; set; }
    public string GrandType { get; set; }
  }
}
