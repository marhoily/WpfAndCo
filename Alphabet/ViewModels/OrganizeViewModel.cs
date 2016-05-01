using System.Linq;
using Caliburn.Micro;

namespace Alphabet
{
    public sealed class OrganizeViewModel : PropertyChangedBase
    {
        private readonly LettersStore _lettersStore;
        private CategoryViewModel _selectedCategory;
        private LetterViewModel _selectedAssignedLetter;
        private LetterViewModel _selectedAvailableLetter;
        private readonly BindableCollection<LetterViewModel> _letters;

        public OrganizeViewModel(LettersStore lettersStore)
        {
            _lettersStore = lettersStore;
            _letters = _lettersStore.Load();
            AvailableLetters = new BindableCollection<LetterViewModel>();
            AssignedLetters = new BindableCollection<LetterViewModel>();
            Load();
        }

        public LetterViewModel SelectedAvailableLetter
        {
            get { return _selectedAvailableLetter; }
            set
            {
                if (Equals(value, _selectedAvailableLetter)) return;
                _selectedAvailableLetter = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanAdd));
            }
        }

        public IObservableCollection<LetterViewModel> AvailableLetters { get; set; }

        public LetterViewModel SelectedAssignedLetter
        {
            get { return _selectedAssignedLetter; }
            set
            {
                if (Equals(value, _selectedAssignedLetter)) return;
                _selectedAssignedLetter = value;
                NotifyOfPropertyChange();
            }
        }

        public IObservableCollection<LetterViewModel> AssignedLetters { get; set; }

        public bool CanAdd => SelectedAvailableLetter != null;
        public void Add()
        {
            SelectedAvailableLetter.CategoryVms.Add(SelectedCategory);
            AssignedLetters.Add(SelectedAvailableLetter);
            AvailableLetters.Remove(SelectedAvailableLetter);
        }
        public void Remove() {}

        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (Equals(value, _selectedCategory)) return;
                _selectedCategory = value;
                NotifyOfPropertyChange();

                AvailableLetters.Clear();
                AvailableLetters.AddRange(
                    _letters.Where(l => !l.CategoryVms.Contains(value)));
                AssignedLetters.Clear();
                AssignedLetters.AddRange(
                    _letters.Where(l => l.CategoryVms.Contains(value)));
            }
        }

        public IObservableCollection<CategoryViewModel> Categories { get; set; }

        public void New() => Categories.Add(SelectedCategory = new CategoryViewModel("blha"));
        public void Delete() => Categories.Remove(SelectedCategory);

        public void Load()
        {
            var categoryViewModels = _letters.SelectMany(x => x.CategoryVms).Distinct();
            Categories = new BindableCollection<CategoryViewModel>(
                categoryViewModels);
        }

        public void Save() => _lettersStore.Save(_letters);
    }
}
