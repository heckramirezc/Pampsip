using System;
using Xamarin.Forms;

namespace Pampsip.Controls
{
    public class ExtendedListView : ListView
    {
        public ExtendedListView() : base() { }
		public static readonly BindableProperty IsScrollEnableProperty = BindableProperty.Create(nameof(IsScrollEnable), typeof(bool), typeof(ExtendedListView), false);

		public bool IsScrollEnable
        {
			get => (bool)GetValue(IsScrollEnableProperty);
			set => SetValue(IsScrollEnableProperty, value);
        }
    }
}