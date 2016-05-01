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
        private const string Store = "../../../letters.json";

        public BindableCollection<VM> Load() =>
            new BindableCollection<VM>(Exists(Store)
                ? DeserializeObject<VM[]>(ReadAllText(Store))
                : Empty<VM>());

        public void Save(IEnumerable<VM> letters) =>
            WriteAllText(Store,
                SerializeObject(letters, Formatting.Indented));
    }
}