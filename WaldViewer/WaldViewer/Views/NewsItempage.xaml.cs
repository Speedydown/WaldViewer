using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaldViewer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WaldViewer.Views
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class NewsItempage : BaseContentPage<NewsItemsViewModel>
  {
    public NewsItempage()
    {
      InitializeComponent();
    }
  }
}