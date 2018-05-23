using System;
using Xamarin.Forms;

namespace Pampsip.Common
{
    public static  class FontResources
    {
        public static readonly string ButtonFont = Device.OnPlatform("NunitoSans-Light", "NunitoSans-Light", null);
        public static readonly string LabelFont = Device.OnPlatform("NunitoSans-Light", "NunitoSans-Light", null);   
        public static readonly string Label2Font = Device.OnPlatform("NunitoSans-Bold", "NunitoSans-Bold", null);   
        public static readonly string EntryFont = Device.OnPlatform("OpenSans", "OpenSans-Regular", null);   

    }
}
