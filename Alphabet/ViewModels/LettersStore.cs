using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Newtonsoft.Json;
using static System.IO.File;
using static System.Linq.Enumerable;
using static Newtonsoft.Json.JsonConvert;

namespace Alphabet
{
    using VM = LetterViewModel;
    public sealed class LettersStore
    {
        public BindableCollection<VM> Letters { get; }
        private const string Store = "../../../letters.json";

        public LettersStore()
        {
            Letters = new BindableCollection<VM>(Exists(Store)
                ? DeserializeObject<VM[]>(ReadAllText(Store))
                : Empty<VM>());
        }

        public void Save() => WriteAllText(Store,
            SerializeObject(Letters, Formatting.Indented));
    }
}