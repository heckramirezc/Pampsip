using System;
using Pampsip.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ExtendedNavigationRenderer))]

namespace Pampsip.Droid.Renderers
{
	public class ExtendedNavigationRenderer : PageRenderer
    {
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var actionBar = ((Activity)Context).ActionBar;
            actionBar.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.YourImageInDrawable));
        }
    }
}