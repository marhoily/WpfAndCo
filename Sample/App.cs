using System;
using FirstFloor.ModernUI.Presentation;
using Sample.Properties;

namespace Sample
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
