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
        public Guid Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string UserName { get; }
        public string Organization { get; }
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
        public ImmutableStack<IComit> Comits { get; private set; } = ImmutableStack<IComit>.Empty;

        public EventStore()
        {
            Comits = Comits
                .Push(new AddAgentComit(Guid.NewGuid(), "John", "Doe", "john.doe@gmail.com", "Microsoft"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Larae", "Surrett", "Larae.Surrett@gmail.com", "Microsoft"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Vernon", "Rhoten", "Vernon.Rhoten@yahoo.com", "Microsoft"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Tillie", "Aldaco", "Tillie.Aldaco@gmail.com", "Microsoft"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Allyn", "Hiott", "Allyn.Hiott@hotmail.com", "Microsoft"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Cherie", "George", "Cherie.George@yahoo.com", "Microsoft"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Earl", "Pound", "Earl.Pound@hotmail.com", "Microsoft"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Florence", "Grissett", "Florence.Grissett@hotmail.com", "Google"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Eliseo", "Brasel", "Eliseo.Brasel@gmail.com", "Google"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Laci", "Pates", "Laci.Pates@hotmail.com", "Google"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Elli", "Marano", "Elli.Marano@yahoo.com", "Google"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Odessa", "Kensinger", "Odessa.Kensinger@gmail.com", "Google"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Nanette", "Tinajero", "Nanette.Tinajero@gmail.com", "Google"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Gabriela", "Brayboy", "Gabriela.Brayboy@hotmail.com", "Luware"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Valeria", "Mcnamara", "Valeria.Mcnamara@hotmail.com", "Luware"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Minda", "Teitelbaum", "Minda.Teitelbaum@gmail.com", "Luware"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Lourie", "Royal", "Lourie.Royal@yahoo.com", "Luware"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Jonathan", "Mallari", "Jonathan.Mallari@hotmail.com", "Luware"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Petra", "Kilgore", "Petra.Kilgore@gmail.com", "Luware"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Lyda", "Culley", "Lyda.Culley@yahoo.com", "Luware"))
                .Push(new AddAgentComit(Guid.NewGuid(), "Emile", "Griest", "Emile.Griest@gmail.com", "Luware"));
        }
    }

}
