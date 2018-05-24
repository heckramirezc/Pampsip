using System;
using Foundation;
using Pampsip.Controls;
using Pampsip.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedLabel), typeof(ExtendedLabelRenderer))]
namespace Pampsip.iOS.Renderers
{
    public class ExtendedLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control != null && Element != null)
            {
                UpdateUi((ExtendedLabel)Element);
            }
        }
        
        private void UpdateUi(ExtendedLabel extendedElement)
        {
            if (!string.IsNullOrEmpty(extendedElement.Text))
            {
                var strikethrough = extendedElement.IsStrikeThrough ? NSUnderlineStyle.Single : NSUnderlineStyle.None;
                
                Control.AttributedText = new NSMutableAttributedString(extendedElement.Text, Control.Font, strikethroughStyle: strikethrough);
                LayoutSubviews();
            }
        }
    }
}