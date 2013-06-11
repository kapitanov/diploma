using System;
using System.Windows;
using System.Windows.Media;

namespace AISTek.DataFlow.PresentationExtensions
{
    public class AeroWindow : Window
    {
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (this.ExtendGlassFrame(new Thickness(-1)))
            {
                Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }
    }
}
