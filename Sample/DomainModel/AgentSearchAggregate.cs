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

        public AgentSearchAggregate(IEnumerable<IComit> source)
        {
            foreach (var comit in source)
            {
                var add = comit as AddAgentComit;
                if (add != null) _agents[add.Id] = _mapper.Map<Agent>(add);

                var update = comit as UpdateAgentComit;
                if (update != null) _agents[update.Id] = _mapper.Map<Agent>(update);

                var delete = comit as DeleteAgentComit;
                if (delete != null) _agents.Remove(delete.Id);
            }
        }

        public List<Agent> Search(string like) =>
            _agents.Values
                .Where(a => Match(a, like))
                .Take(11)
                .ToList();

        private static bool Match(Agent a, string like)
            => Contains(a.FirstName, like)
               || Contains(a.LastName, like)
               || Contains(a.LastName, like)
               || Contains(a.UserName, like)
               || Contains(a.Organization, like);

        private static bool Contains(string str, string subStr)
            => str.IndexOf(subStr,
                StringComparison.InvariantCultureIgnoreCase) > 0;
    }
}