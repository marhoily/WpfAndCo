using System.Reflection;
using Caliburn.Micro;

namespace Sample
{
    public sealed class AboutViewModel : PropertyChangedBase
    {
        public string Version
        {
            get
            {
                return Assembly
                    .GetExecutingAssembly()
                    .GetName().Version.ToString();
            }
        }
    }
}