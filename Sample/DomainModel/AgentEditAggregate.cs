using System;
using System.Collections.Generic;
using AutoMapper;

namespace Configurator
{
    public sealed class AgentEditAggregate
    {
        private readonly EventStore _source;

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
        public sealed class Organization
        {
            public Guid Id { get; }
            public string Name { get; }

            public Organization(Guid id, string name)
            {
                Id = id;
                Name = name;
            }
        }
        private readonly Dictionary<Guid, Agent>
            _agents = new Dictionary<Guid, Agent>();
        private readonly Dictionary<Guid, Organization>
            _organizations = new Dictionary<Guid, Organization>();

        private readonly IMapper _mapper
            = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AddAgentComit, Agent>();
                cfg.CreateMap<UpdateAgentComit, Agent>();
            })
            .CreateMapper();

        public AgentEditAggregate(EventStore source)
        {
            _source = source;
        }

        private void Aggregate()
        {
            foreach (var comit in _source.Comits)
            {
                Agents(comit);
                Organizations(comit);
            }
        }

        private void Organizations(IComit comit)
        {
            var add = comit as AddOrganizationComit;
            if (add != null) _organizations[add.Id] = _mapper.Map<Organization>(add);

            //var update = comit as UpdateOrganizationComit;
            //if (update != null) _organizations[update.Id] = _mapper.Map<Organization>(update);
            //
            //var delete = comit as DeleteOrganizationComit;
            //if (delete != null) _organizations.Remove(delete.Id);
        }

        private void Agents(IComit comit)
        {
            var add = comit as AddAgentComit;
            if (add != null) _agents[add.Id] = _mapper.Map<Agent>(add);

            var update = comit as UpdateAgentComit;
            if (update != null) _agents[update.Id] = _mapper.Map<Agent>(update);

            var delete = comit as DeleteAgentComit;
            if (delete != null) _agents.Remove(delete.Id);
        }

        public Agent GetById(Guid id)
        {
            Aggregate();
            return _agents[id];
        }
    }
}