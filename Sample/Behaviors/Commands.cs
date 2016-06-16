using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using FirstFloor.ModernUI.Windows.Media;

namespace Configurator
{
    public sealed class RowDoubleClickBehavior : Behavior<DataGrid>
    {
        protected override void OnAttached()
        {
            AssociatedObject.MouseDoubleClick += D;
        }

        private void D(object sender, MouseButtonEventArgs e)
        {
            var cell = ((DependencyObject) e.OriginalSource)
                .Ancestors().OfType<DataGridCell>().FirstOrDefault();
            if (cell != null)
            {
                var dx = AssociatedObject.DataContext;
                dx.GetType()
                    .InvokeMember(MethodName, BindingFlags.InvokeMethod,
                        null, dx, new[] {cell.DataContext});
            }
        }
        public string MethodName { get; set; }
      
    }

}