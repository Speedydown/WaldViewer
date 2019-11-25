using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WaldViewer.Exceptions
{
  public class CouldNotFetchDataException : Exception
  {
    public HttpStatusCode StatusCode { get; set; }
    public string Body { get; set; }

    public CouldNotFetchDataException(HttpStatusCode code, string body)
    {
      StatusCode = code;
      Body = body;
    }
  }
}
