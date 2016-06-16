using System;
using System.Collections.Immutable;

namespace Configurator
{
    public interface IComit { }

    public sealed class AddOrganizationComit : IComit
    {
        public Guid Id { get; }
        public string Name { get; }
        public ImmutableArray<string> NotReadyReasons { get; }

        public AddOrganizationComit(Guid id, string name, ImmutableArray<string> notReadyReasons)
        {
            Id = id;
            Name = name;
            NotReadyReasons = notReadyReasons;
        }
    }


    public enum Availability
    {
        Away, TemporarilyAway, Busy, DoNotDisturb
    }
    public sealed class AddPresenceState : IComit
    {
        public Guid Id { get; }
        public Availability Availability { get; }
        public string Activity { get; }

    }
    public sealed class AddReasonTypeComit : IComit
    {
        public Guid Id { get; }
        public string Name { get; }
        public bool IsDefault { get; }
        public TimeSpan TimeThresholdForColoring { get; }
        public string Color { get; }
        public string PresenceState { get; }
    }
    public sealed class AddAgentComit : IComit
    {
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }
        public string Organization { get; }

        public AddAgentComit(Guid id, string firstName, string lastName, string userName, string organization)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Organization = organization;
        }
    }
    public sealed class UpdateAgentComit : IComit
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Organization { get; set; }

    }
    public sealed class DeleteAgentComit : IComit
    {
        public Guid Id { get; }
    }
    public sealed class AgentModel
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }
        public string Organization { get; }
    }

    public sealed class EventStore
    {
        public ImmutableQueue<IComit> Comits { get; private set; } = ImmutableQueue<IComit>.Empty;

        public EventStore()
        {
            Comits = Comits
                .Enqueue(Organization("Microsoft"))
                .Enqueue(Organization("Google"))
                .Enqueue(Organization("Luware"))
                .Enqueue(Agent("John", "Doe", "john.doe@gmail.com", "Microsoft"))
                .Enqueue(Agent("Larae", "Surrett", "Larae.Surrett@gmail.com", "Microsoft"))
                .Enqueue(Agent("Vernon", "Rhoten", "Vernon.Rhoten@yahoo.com", "Microsoft"))
                .Enqueue(Agent("Tillie", "Aldaco", "Tillie.Aldaco@gmail.com", "Microsoft"))
                .Enqueue(Agent("Allyn", "Hiott", "Allyn.Hiott@hotmail.com", "Microsoft"))
                .Enqueue(Agent("Cherie", "George", "Cherie.George@yahoo.com", "Microsoft"))
                .Enqueue(Agent("Earl", "Pound", "Earl.Pound@hotmail.com", "Microsoft"))
                .Enqueue(Agent("Florence", "Grissett", "Florence.Grissett@hotmail.com", "Google"))
                .Enqueue(Agent("Eliseo", "Brasel", "Eliseo.Brasel@gmail.com", "Google"))
                .Enqueue(Agent("Laci", "Pates", "Laci.Pates@hotmail.com", "Google"))
                .Enqueue(Agent("Elli", "Marano", "Elli.Marano@yahoo.com", "Google"))
                .Enqueue(Agent("Odessa", "Kensinger", "Odessa.Kensinger@gmail.com", "Google"))
                .Enqueue(Agent("Nanette", "Tinajero", "Nanette.Tinajero@gmail.com", "Google"))
                .Enqueue(Agent("Gabriela", "Brayboy", "Gabriela.Brayboy@hotmail.com", "Luware"))
                .Enqueue(Agent("Valeria", "Mcnamara", "Valeria.Mcnamara@hotmail.com", "Luware"))
                .Enqueue(Agent("Minda", "Teitelbaum", "Minda.Teitelbaum@gmail.com", "Luware"))
                .Enqueue(Agent("Lourie", "Royal", "Lourie.Royal@yahoo.com", "Luware"))
                .Enqueue(Agent("Jonathan", "Mallari", "Jonathan.Mallari@hotmail.com", "Luware"))
                .Enqueue(Agent("Petra", "Kilgore", "Petra.Kilgore@gmail.com", "Luware"))
                .Enqueue(Agent("Lyda", "Culley", "Lyda.Culley@yahoo.com", "Luware"))
                .Enqueue(Agent("Emile", "Griest", "Emile.Griest@gmail.com", "Luware"));
        }

        private static AddAgentComit Agent(string firstName, string lastName, string userName, string organization)
        {
            return new AddAgentComit(Guid.NewGuid(), firstName, lastName, userName, organization);
        }
        private static AddOrganizationComit Organization(string name)
        {
            return new AddOrganizationComit(
                Guid.NewGuid(), name, ImmutableArray<string>.Empty);
        }

        public void Append(IComit comit)
        {
            Comits = Comits.Enqueue(comit);
        }
    }

}
