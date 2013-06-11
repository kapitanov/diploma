using System.Windows;
using System.Windows.Interactivity;

namespace AISTek.DataFlow.PresentationExtensions
{
    public sealed class HideCloseButtonBehaviour : Behavior<Window>
    {
        private CloseButtonHider hider;

        protected override void OnAttached()
        {
            hider = new CloseButtonHider(AssociatedObject);

            hider.Hide();

            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            hider.Show();
            base.OnDetaching();
        }
    }
}
