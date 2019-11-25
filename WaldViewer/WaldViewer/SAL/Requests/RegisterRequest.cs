using System;
using System.Collections.Generic;
using System.Text;

namespace WaldViewer.SAL.Requests
{
  public class RegisterRequest
  {
    public string Email { get; set; }
    public string password { get; set; }
    public string Name { get; set; }
  }
}
