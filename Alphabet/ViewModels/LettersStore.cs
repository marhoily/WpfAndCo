using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using static System.IO.File;
using static Newtonsoft.Json.JsonConvert;

namespace Alphabet
{
    using VM = LetterViewModel;
    public sealed class LettersStore
    {
        private const string Store = "../../../letters.txt";

        public BindableCollection<VM> Load()
            => new BindableCollection<VM>(Exists(Store)
                ? DeserializeObject<string[]>(ReadAllText(Store)).Select(x => new VM(x))
                : Enumerable.Empty<VM>());

        public void Save(IEnumerable<VM> letters)
            => WriteAllText(Store, SerializeObject(letters.Select(x => x.Code)));
    }
}