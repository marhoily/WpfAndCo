using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Caliburn.Micro;

namespace Configurator
{
    public sealed class AgentSearchViewModel : PropertyChangedBase
    {
        private readonly Subject<string>
            _subject = new Subject<string>();
        private string _searchString;
        private List<AgentSearchAggregate.Agent> _searchResults;
        private bool _thereAreMoreResults;

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (value == _searchString) return;
                _searchString = value;
                _subject.OnNext(value);
            }
        }
        public List<AgentSearchAggregate.Agent> SearchResults
        {
            get { return _searchResults; }
            set
            {
                if (Equals(value, _searchResults)) return;
                _searchResults = value;
                NotifyOfPropertyChange();
            }
        }
        public bool ThereAreMoreResults
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
                .Subscribe(next =>
                {
                    ThereAreMoreResults = next.Count == 11;
                    if (ThereAreMoreResults) next.RemoveAt(10);
                    SearchResults = next;
                });

        }
    }
}