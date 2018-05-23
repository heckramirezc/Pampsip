using System;
using Xamarin.Forms;

namespace Pampsip.Controls
{
    public class ExtendedButton : Button
    {
        public EventHandler<Point> FinLongPress;
        public EventHandler<Point> CambioLongPress;
    }
}
