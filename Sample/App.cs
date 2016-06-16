using System;
using FirstFloor.ModernUI.Presentation;
using Configurator.Properties;

namespace Configurator
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
