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
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        public void ShowStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }
    }
}
