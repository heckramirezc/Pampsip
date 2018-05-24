using System;
using Pampsip.Interfaces;
using UIKit;

namespace Pampsip.iOS.Services
{
    public class NavigationService : INavigationService
    {

        public NavigationService()
        {
        }

        public void HideStatusBar()
        {
            //UIApplication.SharedApplication.StatusBarHidden = true;
			UIApplication.SharedApplication.SetStatusBarHidden(true, UIStatusBarAnimation.Fade);
        }

		public void NavigationBarTranslucent()
		{
			//UIApplication.SharedApplication.bar
			//this.NavigationController.NavigationBar.Translucent = true;

		}

        public void ShowStatusBar()
        {
            //UIApplication.SharedApplication.StatusBarHidden = false;
			UIApplication.SharedApplication.SetStatusBarHidden(false, UIStatusBarAnimation.Fade);
        }
    }
}
