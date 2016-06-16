﻿using System;
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
        private readonly Func<string, List<Agent>> _search;

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
            _search = searchAggregate.Search;
            _subject.Throttle(TimeSpan.FromSeconds(.5))
                .Select(_search)
                .SubscribeOnDispatcher()
                .Subscribe(ShowSearchResult);
            RefreshData();
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

        public event Action<Agent> AgentSelected;
        public void SelectAgent(Agent agent) 
            => AgentSelected?.Invoke(agent);
        public void RefreshData() => ShowSearchResult(_search(null));
    }
}