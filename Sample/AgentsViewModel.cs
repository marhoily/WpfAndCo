using System;
using Caliburn.Micro;

namespace Configurator
{
    public sealed class AgentsViewModel : PropertyChangedBase
    {
        public object CurrentView { get; set; }

        public AgentsViewModel(
            AgentSearchViewModel search,
            Func<Guid, AgentEditViewModel> edit)
        {
            CurrentView = search;
            search.Activated += agent =>
            {
                var editView = edit(agent.Id);
                editView.Done += () => CurrentView = search;
                CurrentView = editView;
            };
        }
    }
}