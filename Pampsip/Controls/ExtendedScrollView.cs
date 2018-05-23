using System;
using Xamarin.Forms;

namespace Pampsip.Controls
{
	public class ExtendedScrollView : ScrollView
    {
		public ExtendedScrollView() : base(){}
		public static readonly BindableProperty ScrollingEnabledProperty = BindableProperty.Create<ExtendedScrollView, bool>(p => p.ScrollingEnabled, true);
		public static readonly BindableProperty ScrollBarEnabledProperty = BindableProperty.Create<ExtendedScrollView, bool>(p => p.ScrollBarEnabled, true);
        public bool ScrollingEnabled
        {
            set
            {
                SetValue(ScrollingEnabledProperty, value);
            }
            get
            {
                return (bool)GetValue(ScrollingEnabledProperty);
            }
        }

		public bool ScrollBarEnabled
        {
            set
            {
				SetValue(ScrollBarEnabledProperty, value);
            }
            get
            {
				return (bool)GetValue(ScrollBarEnabledProperty);
            }
        }
    }
}
