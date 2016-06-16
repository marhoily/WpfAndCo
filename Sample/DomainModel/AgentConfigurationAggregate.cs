using System;
using System.Collections.Generic;
using AutoMapper;

namespace Configurator
{
    public sealed class AgentConfigurationAggregate
    {
        public sealed class Agent
        {
            public Guid Id { get; }
            public string FirstName { get; }
            public string LastName { get; }
            public string UserName { get; }
            public string Organization { get; }

            public Agent(Guid id,
                string firstName,
                string lastName,
                string userName,
                string organization)
            {
                Id = id;
                FirstName = firstName;
                LastName = lastName;
                UserName = userName;
                Organization = organization;
            }

        }
        private readonly Dictionary<Guid, Agent>
            _agents = new Dictionary<Guid, Agent>();

        private readonly IMapper _mapper
            = new MapperConfiguration(cfg =>
                cfg.CreateMap<AddAgentComit, Agent>())
                .CreateMapper();

        public AgentConfigurationAggregate(EventStore source)
        {
            foreach (var comit in source.Comits)
            {
                var add = comit as AddAgentComit;
                if (add != null) _agents[add.Id] = _mapper.Map<Agent>(add);

                var update = comit as UpdateAgentComit;
                if (update != null) _agents[update.Id] = _mapper.Map<Agent>(update);

                var delete = comit as DeleteAgentComit;
                if (delete != null) _agents.Remove(delete.Id);
            }
        }

        public Agent GetById(Guid id) => _agents[id];
    }
}