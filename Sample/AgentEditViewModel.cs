using System;
using AutoMapper;
using Caliburn.Micro;
using Action = System.Action;

namespace Configurator
{
    public sealed class AgentEditViewModel : PropertyChangedBase
    {
        public Guid Id { get; }
        private readonly AgentEditAggregate _edit;
        private readonly EventStore _store;
        private readonly IMapper _mapper
            = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AgentEditViewModel, UpdateAgentComit>();
                cfg.CreateMap<AgentEditAggregate.Agent, AgentEditViewModel>();
            }).CreateMapper();

        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _organization;

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
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value == _userName) return;
                _userName = value;
                NotifyOfPropertyChange();
            }
        }
        public string Organization
        {
            get { return _organization; }
            set
            {
                if (value == _organization) return;
                _organization = value;
                NotifyOfPropertyChange();
            }
        }

        public AgentEditViewModel(
            AgentEditAggregate edit,
            Guid id,
            EventStore store)
        {
            _edit = edit;
            Id = id;
            _store = store;
            Update();
        }

        public void Apply() { Append(); Update(); }
        public void Update() => _mapper.Map(_edit.GetById(Id), this);
        public void Save() { Append(); Done?.Invoke(); }
        public void Cancel() => Done?.Invoke();
        public event Action Done;
        private void Append() => _store.Append(
            _mapper.Map<UpdateAgentComit>(this));
    }
}