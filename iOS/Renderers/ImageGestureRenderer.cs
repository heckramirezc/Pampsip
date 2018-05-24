using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Pampsip.Controls;
using UIKit;
using Pampsip.iOS.Renderers;
using CoreGraphics;
using FFImageLoading.Forms;
using FFImageLoading.Forms.Touch;

//[assembly: ExportRenderer(typeof(ExtendedImage), typeof(ImageGestureRenderer))]
namespace Pampsip.iOS.Renderers
{
    public class ImageGestureRenderer : CachedImageRenderer
    {
        ExtendedImage Image;
        public ImageGestureRenderer()
        {            
            this.AddGestureRecognizer(new UILongPressGestureRecognizer((longPress) =>
            {
                if (longPress.State == UIGestureRecognizerState.Began)
                {                    
                    Image.InicioLongPress(Image, EventArgs.Empty);
                }
                if (longPress.State == UIGestureRecognizerState.Ended)
                {                    
                    CGPoint point = longPress.LocationInView(Window);
                    Controls.Point Point = new Controls.Point { X = point.X, Y = point.Y };
                    Image.FinLongPress(Image, Point);
                }
                if (longPress.State == UIGestureRecognizerState.Changed)
                {                                        
                    CGPoint point = longPress.LocationInView(Window);
                    Controls.Point Point = new Controls.Point { X = point.X, Y = point.Y };
                    Image.CambioLongPress(Image, Point);
                }

            }));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CachedImage> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
                Image = e.NewElement as ExtendedImage;
        }
    }
}