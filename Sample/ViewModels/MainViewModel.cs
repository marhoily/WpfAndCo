using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Caliburn.Micro;

namespace Sample
{
    public sealed class MainViewModel : PropertyChangedBase
    {
        public void Load()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<MessageDbAutofacModule>();

            builder.Register(_ => new MessageContext());

            var container = builder.Build();
            using (var ctx = container.Resolve<Func<MessageContext>>()())
            {
                ctx.Database.EnsureCreated();
                ctx.Conversations.Add(new Conversation
                {
                    Sender = "a",
                    Receiver = "b",
                    Messages = {
                        new TextMessage {Sender = "a", Receiver = "b", Text = "text"}
                    }
                });
                ctx.Conversations.Add(new Conversation
                {
                    Sender = "b",
                    Receiver = "a",
                });
                ctx.SaveChanges();
            }
            using (var ctx = container.Resolve<Func<MessageContext>>()())
            {
                Conversations = ctx.Conversations.ToList();
                NotifyOfPropertyChange(nameof(Conversations));
            }

        }

        public List<Conversation> Conversations { get; set; }
    }
}