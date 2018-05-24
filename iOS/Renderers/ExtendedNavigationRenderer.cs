using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;
using Pampsip.iOS.Renderers;
using CoreGraphics;
using Pampsip.Pages.Menu;
using System.Drawing;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(ExtendedNavigationRenderer))]
namespace Pampsip.iOS.Renderers
{
	public class ExtendedNavigationRenderer : NavigationRenderer
	{
		public ExtendedNavigationRenderer()
		{
		}
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			NavigationBar.Translucent = true;
			App.NavigationBarHeight = NavigationBar.Frame.Height;
			var img = UIImage.FromBundle("headerGeneral.png");
			this.NavigationBar.SetBackgroundImage(img, UIBarMetrics.Default);
			NavigationBar.ShadowImage = new UIImage();            
		}
	}
}