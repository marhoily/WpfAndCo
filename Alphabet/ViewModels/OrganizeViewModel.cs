using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using MoreLinq;

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
                NotifyOfPropertyChange(nameof(CanRemove));
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
        public bool CanRemove => SelectedAssignedLetter != null;

        public void Remove()
        {
            SelectedAssignedLetter.CategoryVms.Remove(SelectedCategory);
            AvailableLetters.Add(SelectedAssignedLetter);
            AssignedLetters.Remove(SelectedAssignedLetter);
        }

        public CategoryViewModel SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (Equals(value, _selectedCategory)) return;
                _selectedCategory = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(CanEdit));
                NotifyOfPropertyChange(nameof(CanDelete));
                AvailableLetters.Clear();
                AssignedLetters.Clear();
                if (value == null) return;
                AvailableLetters.AddRange(
                    _letters.Where(l => !l.Categories.Contains(value.Name)));
                AssignedLetters.AddRange(
                    _letters.Where(l => l.Categories.Contains(value.Name)));
            }
        }

        public IObservableCollection<CategoryViewModel> Categories { get; set; }

        public bool CanEdit => SelectedCategory != null;
        public void New() => Categories.Add(SelectedCategory = new CategoryViewModel("blha"));
        public bool CanDelete => SelectedCategory != null;
        public void Delete()
        {
            foreach (var l in _letters)
                l.CategoryVms.Remove(SelectedCategory);
            Categories.Remove(SelectedCategory);
        }

        public void Load() =>
            Categories = new BindableCollection<CategoryViewModel>(
                _letters.SelectMany(x => x.CategoryVms).DistinctBy(x => x.Name));

        public void Save() => _lettersStore.Save(_letters);
    }
}
