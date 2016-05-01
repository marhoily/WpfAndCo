using System;
using Alphabet.Properties;
using FirstFloor.ModernUI.Presentation;

namespace Alphabet
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
