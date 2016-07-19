using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using Newtonsoft.Json.Linq;
using NEventStore;
using NEventStore.Persistence.Sql.SqlDialects;
using NEventStore.Serialization;

namespace NesViewer.Ui
{
    public sealed class AppBootstrapper : BootstrapperBase
    {
        private ILifetimeScope _container;

        public AppBootstrapper() { Initialize(); }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(type => type.Name.EndsWith("ViewModel"))
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(type => type.Name.EndsWith("View"))
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<WindowManager>()
                .As<IWindowManager>()
                .SingleInstance();

            builder.RegisterType<EventAggregator>()
                .As<IEventAggregator>()
                .SingleInstance();

            builder.RegisterType<MessageContext>();
            // IStoreEvents
            builder.RegisterInstance(Wireup.Init()
                .UsingSqlPersistence(@"connectionStringName")
                    .WithDialect(new MsSqlDialect())
                    .UsingCustomSerialization(new MySerializer())
                //  .InitializeStorageEngine()
                //  .UsingJsonSerialization()
                //                        .Compress()
                .Build())
                .SingleInstance();
            _container = builder.Build();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            if (key == null && _container.IsRegistered(serviceType))
                return _container.Resolve(serviceType);

            throw new Exception(string.Format(
                "Could not locate any instances of contract {0}.",
                key ?? serviceType.Name));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.Resolve(typeof(IEnumerable<>)
                .MakeGenericType(serviceType)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }

    public class MySerializer : ISerialize
    {
        public void Serialize<T>(Stream output, T graph)
        {

        }

        public T Deserialize<T>(Stream input)
        {
            if (typeof(T) == typeof(List<EventMessage>))
            {
                var readToEnd = new StreamReader(input).ReadToEnd();
                var jArr = JArray.Parse(readToEnd);

                var eventMessages = new List<EventMessage>();
                foreach (var x in jArr)
                {
                    var eventMessage = new EventMessage();
                    eventMessage.Headers = x["Headers"]
                        .ToObject<Dictionary<string, object>>();
                    eventMessage.Body = x["Body"];
                    eventMessages.Add(eventMessage);
                }
                return (T)(object)eventMessages;
            }
            return default(T);
        }
    }
}