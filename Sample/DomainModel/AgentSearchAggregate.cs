using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Configurator
{
    public sealed class AgentSearchAggregate
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

        public AgentSearchAggregate(EventStore source)
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

        public List<Agent> Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return _agents.Values.Take(11).ToList();
            return _agents.Values
                .Where(a => Match(a, searchString))
                .Take(11)
                .ToList();
        }

        private static bool Match(Agent a, string searchString)
            => Contains(a.FirstName, searchString)
               || Contains(a.LastName, searchString)
               || Contains(a.LastName, searchString)
               || Contains(a.UserName, searchString)
               || Contains(a.Organization, searchString);

        private static bool Contains(string str, string subStr)
            => str.IndexOf(subStr,
                StringComparison.InvariantCultureIgnoreCase) > 0;
    }
}