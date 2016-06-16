using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using Caliburn.Micro;

namespace Configurator
{
    using Agent = AgentSearchAggregate.Agent;

    public sealed class AgentSearchViewModel : PropertyChangedBase
    {
        private readonly Subject<string>
            _subject = new Subject<string>();
        private string _searchString;
        private List<Agent> _searchResults;
        private Visibility _thereAreMoreResults;

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (value == _searchString) return;
                _searchString = value;
                _subject.OnNext(value);
                NotifyOfPropertyChange();
            }
        }
        public List<Agent> SearchResults
        {
            get { return _searchResults; }
            set
            {
                if (Equals(value, _searchResults)) return;
                _searchResults = value;
                NotifyOfPropertyChange();
            }
        }
        public Visibility ThereAreMoreResults
        {
            get { return _thereAreMoreResults; }
            set
            {
                if (value == _thereAreMoreResults) return;
                _thereAreMoreResults = value;
                NotifyOfPropertyChange();
            }
        }

        public AgentSearchViewModel(AgentSearchAggregate searchAggregate)
        {
            _subject.Throttle(TimeSpan.FromSeconds(.5))
                .Select(searchAggregate.Search)
                .SubscribeOnDispatcher()
                .Subscribe(ShowSearchResult);
            ShowSearchResult(searchAggregate.Search(null));
        }

        private void ShowSearchResult(List<Agent> next)
        {
            var more = next.Count == 11;
            if (more) next.RemoveAt(10);
            SearchResults = next;
            ThereAreMoreResults = more
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public void Activate(Agent agent)
        {

        }
    }
}