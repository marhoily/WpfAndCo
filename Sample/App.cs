using System;
using FirstFloor.ModernUI.Presentation;
using NesViewer.Ui.Properties;

namespace NesViewer.Ui
{
    public partial class App 
    {
        public App()
        {
            InitializeComponent();
            AppearanceManager.Current.ThemeSource =
                new Uri(Settings.Default.ThemeSource, UriKind.Relative);
        }
    }
}
