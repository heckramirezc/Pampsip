﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using Foundation;
using Pampsip.Controls;
using Pampsip.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedPdfView), typeof(ExtendedPdfViewRenderer))]
namespace Pampsip.iOS.Renderers
{
	public class ExtendedPdfViewRenderer : ViewRenderer<ExtendedPdfView, UIWebView>
    {
		protected override void OnElementChanged(ElementChangedEventArgs<ExtendedPdfView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                SetNativeControl(new UIWebView());
            }
            if (e.OldElement != null)
            {
                // Cleanup
            }
            if (e.NewElement != null)
            {
				var customWebView = Element as ExtendedPdfView;
                string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, string.Format("Content/{0}", WebUtility.UrlEncode(customWebView.Uri)));
                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
                Control.ScalesPageToFit = true;
            }
        }
    }
}