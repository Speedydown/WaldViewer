using System;
using System.Collections.Generic;
using System.Text;

namespace WaldViewer.BL
{
  public class NewsItem
  {
    public string Title { get; set; }
    public string Body { get; set; } = "Het was niet de eerste keer dat deze vaart een auto liet zinken..... Tientallen gingen hier inmiddels te water";
    public string PublishedAt { get; set; }
    public string Imageurl { get; set; }
    public string Url { get; set; }
    public bool Highlighted { get; set; }
  }
}
