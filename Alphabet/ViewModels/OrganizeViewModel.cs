using Caliburn.Micro;

namespace Alphabet
{
    public sealed class OrganizeViewModel : PropertyChangedBase
    {
        public CategoryViewModel SelectedCategory { get; set; }
        public IObservableCollection<CategoryViewModel> Categories { get; set; }

        public LetterViewModel SelectedAvailableLetter { get; set; }
        public IObservableCollection<LetterViewModel> AvailableLetters { get; set; }

        public LetterViewModel SelectedAssignedLetter { get; set; }
        public IObservableCollection<LetterViewModel> AssignedLetters { get; set; }

        public void Add() {}
        public void Remove() {}
        public void New() {}
        public void Delete() {}
        public void Load() {}
        public void Save() {}
    }
}
