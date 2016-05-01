using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Alphabet.Behaviors
{
    public sealed class FocusOnUpdateBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.DataContextChanged += (s, e) =>
            {
                AssociatedObject.Focus();
                AssociatedObject.SelectAll();
            };
        }
    }
}
