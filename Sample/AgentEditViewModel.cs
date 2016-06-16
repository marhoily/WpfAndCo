using System;
using AutoMapper;
using Caliburn.Micro;
using Action = System.Action;

namespace Configurator
{
    public sealed class AgentEditViewModel : PropertyChangedBase
    {
        private readonly AgentEditAggregate _edit;
        public Guid AgentId { get; }
        private readonly EventStore _store;
        private readonly IMapper _mapper
            = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AgentEditViewModel, UpdateAgentComit>();
                cfg.CreateMap<AgentEditAggregate.Agent, AgentEditViewModel>();
            }).CreateMapper();

        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                NotifyOfPropertyChange();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                NotifyOfPropertyChange();
            }
        }

        public AgentEditViewModel(
            AgentEditAggregate edit,
            Guid agentId,
            EventStore store)
        {
            _edit = edit;
            AgentId = agentId;
            _store = store;
            Update();
        }

        public void Apply() { Append(); Update(); }
        public void Update() => _mapper.Map(_edit.GetById(AgentId), this);
        public void Save() { Append(); Done?.Invoke(); }
        public void Cancel() => Done?.Invoke();
        public event Action Done;
        private void Append() => _store.Append(
            _mapper.Map<AddAgentComit>(this));
    }
}