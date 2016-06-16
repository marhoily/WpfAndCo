using System;
using AutoMapper;
using Caliburn.Micro;
using Action = System.Action;

namespace Configurator
{
    public sealed class AgentEditViewModel : PropertyChangedBase
    {
        private readonly AgentEditAggregate _edit;
        private readonly Guid _agentId;
        private readonly EventStore _store;
        private readonly IMapper _mapper
            = new MapperConfiguration(cfg =>
                cfg.CreateMap<AgentEditAggregate.Agent, UpdateAgentComit>())
                .CreateMapper();

        private AgentEditAggregate.Agent _agent;

        public AgentEditAggregate.Agent Agent
        {
            get { return _agent; }
            private set
            {
                if (Equals(value, _agent)) return;
                _agent = value;
                NotifyOfPropertyChange();
            }
        }

        public AgentEditViewModel(
            AgentEditAggregate edit,
            Guid agentId,
            EventStore store)
        {
            _edit = edit;
            _agentId = agentId;
            _store = store;
            Update();
        }

        public void Apply() { Append(); Update(); }
        public void Update() => Agent = _edit.GetById(_agentId);
        public void Save() { Append(); Done?.Invoke(); }
        public void Cancel() => Done?.Invoke();
        public event Action Done;
        private void Append() => _store.Append(_mapper.Map<UpdateAgentComit>(Agent));
    }
}