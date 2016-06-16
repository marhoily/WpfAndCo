using Caliburn.Micro;

namespace Configurator
{
    public sealed class AgentsViewModel : PropertyChangedBase
    {
        public object CurrentView { get; set; }

        public AgentsViewModel() { CurrentView = "Hello world!"; }
    }
}