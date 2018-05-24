using System;
using System.ComponentModel;
using CoreAnimation;
using CoreGraphics;
using Foundation;
//using Pampsip.Controls;
using Pampsip.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Button), typeof(ButtonGestureRenderer))]
namespace Pampsip.iOS.Renderers
{
    public class ButtonGestureRenderer : ButtonRenderer
    {
        UIButton Button;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                
                var btn = (UIButton)Control;
                /*
                CAGradientLayer btnGradient = new CAGradientLayer();
                btnGradient.Frame = btn.Bounds;
                btnGradient.Colors = new CGColor[] { Color.Black.ToCGColor(), Color.White.ToCGColor() };
                btnGradient.Locations = new NSNumber[] { 0.0f, 0.1f };
                btn.Layer.AddSublayer(btnGradient);
                btn.Layer.MasksToBounds = true;
                btn.Layer.BorderColor = Color.Blue.ToCGColor();
                btn.Layer.BorderWidth = 2;
                btn.Layer.SetNeedsDisplay();
                */



                btn.Layer.BorderColor = UIColor.White.CGColor;
                btn.Layer.CornerRadius = 0;
                btn.Layer.MasksToBounds = false;
                btn.Layer.ShadowOffset = new CGSize(0, 3);
                btn.Layer.ShadowRadius = 5;
                btn.Layer.ShadowColor = Color.FromHex("000000").ToCGColor();
                btn.Layer.ShadowOpacity = 0.16f;
                btn.Layer.CornerRadius = (float)e.NewElement.BorderRadius;
            }
        }

    }
}
