using System;
using Android.Content;
using Android.Graphics;
using Pampsip.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Label), typeof(XamarinLabelRenderer))]

namespace Pampsip.Droid.Renderers
{
    public class XamarinLabelRenderer : LabelRenderer
    {


        public XamarinLabelRenderer(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (!string.IsNullOrEmpty(e.NewElement?.FontFamily))
            {
                try
                {
                    using (var font = Typeface.CreateFromAsset(Context.ApplicationContext.Assets, e.NewElement.FontFamily + ".ttf"))
                    {
                        Control.Typeface = font;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

