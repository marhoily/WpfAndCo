using System;
using Caliburn.Micro;

namespace Configurator
{
    public sealed class AgentsViewModel : PropertyChangedBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                if (Equals(value, _currentView)) return;
                _currentView = value;
                NotifyOfPropertyChange();
            }
        }

        public AgentsViewModel(
            AgentSearchViewModel search,
            Func<Guid, AgentEditViewModel> edit)
        {
            CurrentView = search;
            search.AgentSelected += agent =>
            {
                var editView = edit(agent.Id);
                editView.Done += () =>
                {
                    CurrentView = search;
                    search.RefreshData();
                };
                CurrentView = editView;
            };
        }
    }
}