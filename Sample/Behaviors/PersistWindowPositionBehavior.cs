using System;
using System.Windows;
using System.Windows.Interactivity;
using Configurator.Properties;
using Newtonsoft.Json;

namespace Configurator
{
    public sealed class PersistWindowPositionBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            AssociatedObject.Initialized += RestorePosition;
            AssociatedObject.Closed += SavePosition;
        }

        private void RestorePosition(object sender, EventArgs e)
        {
            AssociatedObject.WindowStartupLocation = WindowStartupLocation.Manual;
            var jp = Settings.Default.WindowPosition;
            if (string.IsNullOrWhiteSpace(jp)) return;
            var p = JsonConvert.DeserializeObject<WindowPosition>(jp);
            AssociatedObject.WindowState = p.WindowState;
            AssociatedObject.Top = p.Top;
            AssociatedObject.Left = p.Left;
            AssociatedObject.Width = p.Width;
            AssociatedObject.Height = p.Height;
        }

        private void SavePosition(object sender, EventArgs e)
        {
            Settings.Default.WindowPosition = JsonConvert.SerializeObject(
                new WindowPosition
                {
                    WindowState = AssociatedObject.WindowState,
                    Top = AssociatedObject.Top,
                    Left = AssociatedObject.Left,
                    Width = AssociatedObject.Width,
                    Height = AssociatedObject.Height,
                    RestoreBounds = AssociatedObject.RestoreBounds
                });
            Settings.Default.Save();
        }

        private sealed class WindowPosition
        {
            public WindowState WindowState { get; set; }
            public double Top { get; set; }
            public double Left { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public Rect RestoreBounds { get; set; }
        }
    }
}