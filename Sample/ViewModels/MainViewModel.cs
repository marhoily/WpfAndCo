using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using NEventStore;

namespace NesViewer.Ui
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        private readonly Func<MessageContext> _func;
        private readonly IStoreEvents _storeEvents;

        public MainViewModel(Func<MessageContext> func,
            IStoreEvents storeEvents)
        {
            _func = func;
            _storeEvents = storeEvents;
        }

        private static readonly Guid Id =
            new Guid("F4FD8794-8235-4D80-A3E9-6795CDFFBE13");

        private int _current = 0;
        public void Load()
        {
            var eventStream = _storeEvents.OpenStream(Id, _current, _current + 100);
            Events = eventStream.CommittedEvents;
            NotifyOfPropertyChange(() => Events);
            _current += 100;
        }

        public void Load8()
        {
            using (var ctx = _func())
            {
                ctx.Database.EnsureCreated();
                ctx.Conversations.Add(new Conversation
                {
                    Sender = "a",
                    Receiver = "b",
                    Messages =
                    {
                        new TextMessage
                        {
                            Sender = "a",
                            Receiver = "b",
                            Text = "text"
                        }
                    }
                });
                ctx.Conversations.Add(new Conversation
                {
                    Sender = "b",
                    Receiver = "a",
                });
                ctx.SaveChanges();
            }
            using (var ctx = _func())
            {
                Conversations = ctx.Conversations.ToList();
                NotifyOfPropertyChange(() => Conversations);
            }

        }

        public List<Conversation> Conversations { get; set; }
        public ICollection<EventMessage> Events { get; set; }
    }
}