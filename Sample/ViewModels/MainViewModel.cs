using Caliburn.Micro;

namespace Sample
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        public object CurrentView { get; set; }

        public MainViewModel() { CurrentView = "Hello world!"; }
    }
}