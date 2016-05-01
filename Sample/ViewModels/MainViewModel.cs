using Caliburn.Micro;

namespace Alphabet
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        public object CurrentView { get; set; }

        public MainViewModel() { CurrentView = "Hello world!"; }
    }
}