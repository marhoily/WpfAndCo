using Caliburn.Micro;

namespace Configurator
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        public object CurrentView { get; } = new CurrentViewModel();
    }
}