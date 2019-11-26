using System;
using System.Collections.Generic;
using System.Text;
using WaldViewer.BL;
using Xamarin.Forms;

namespace WaldViewer.TemplateSelectors
{
  public class NewsItemTemplateSelector : DataTemplateSelector
  {
    public DataTemplate HightlightedTemplate { get; set; }
    public DataTemplate ItemTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container) => ((NewsItem)item).Highlighted ? HightlightedTemplate : ItemTemplate;
  }
}
