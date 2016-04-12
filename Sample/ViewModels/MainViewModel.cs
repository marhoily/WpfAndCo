using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;

namespace Sample
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        private readonly Func<MessageContext> _func;

        public MainViewModel(Func<MessageContext> func)
        {
            _func = func;
        }

        public void Load()
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
    }
}