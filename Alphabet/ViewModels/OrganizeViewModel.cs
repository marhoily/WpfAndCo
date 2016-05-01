using Caliburn.Micro;

namespace Alphabet
{
    public sealed class OrganizeViewModel : PropertyChangedBase
    {
        private readonly LettersStore _lettersStore;
        private CategoryViewModel _selectedCategory;
        private LetterViewModel _selectedAssignedLetter;
        private LetterViewModel _selectedAvailableLetter;

        public OrganizeViewModel(LettersStore lettersStore)
        {
            _lettersStore = lettersStore;
            Categories = new BindableCollection<CategoryViewModel>();
        }

        public LetterViewModel SelectedAvailableLetter
        {
            get { return _selectedAvailableLetter; }
            set
            {
                if (Equals(value, _selectedAvailableLetter)) return;
                _selectedAvailableLetter = value;
                NotifyOfPropertyChange();
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

        public void Add() {}
        public void Remove() {}

        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (Equals(value, _selectedCategory)) return;
                _selectedCategory = value;
                NotifyOfPropertyChange();


            }
        }

        public IObservableCollection<CategoryViewModel> Categories { get; set; }

        public void New() => Categories.Add(SelectedCategory = new CategoryViewModel("blha"));
        public void Delete() => Categories.Remove(SelectedCategory);
        public void Load() { }
        public void Save() { }
    }
}
