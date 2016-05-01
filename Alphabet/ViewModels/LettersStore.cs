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

        public BindableCollection<VM> Load()
        {
            var exists = Exists(Store);
            var letterViewModels = exists
                ? DeserializeObject<VM[]>(ReadAllText(Store))
                : Empty<VM>();
            return new BindableCollection<VM>(letterViewModels);
        }

        public void Save(IEnumerable<VM> letters)
        {
            WriteAllText(Store, 
                SerializeObject(letters, 
                    Formatting.Indented));
        }
    }
}