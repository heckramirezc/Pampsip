using System;
using Xamarin.Forms;

namespace Pampsip.Controls
{
	public class ExtendedPdfView : WebView
    {
		public static readonly BindableProperty UriProperty = BindableProperty.Create(nameof(Uri), typeof(string), typeof(ExtendedPdfView));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }
    }
}