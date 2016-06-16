using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool IsDefault { get;  }
        public TimeSpan TimeThresholdForColoring { get;  }
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

    public sealed class AgentEditingAggregate
    {
        
    }

    public sealed class EventStore
    {
        public ImmutableStack<IComit> Comits { get;
            private set; } = ImmutableStack<IComit>.Empty;
    }
}
