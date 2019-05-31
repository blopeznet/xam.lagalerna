using Autofac;
using System;

namespace Xam.LaGalerna.ViewModels.Base
{
    public class Locator
    {
        IContainer _container;
        ContainerBuilder _containerBuilder;

        public static Locator Instance { get; } = new Locator();

        public Locator()
        {
            _containerBuilder = new ContainerBuilder();

            _containerBuilder.RegisterType<MainViewModel>();
            _containerBuilder.RegisterType<RssDetailViewModel>();            
        }

        public T Resolve<T>() => _container.Resolve<T>();

        public object Resolve(Type type) => _container.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface => _containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void Register<T>() where T : class => _containerBuilder.RegisterType<T>();

        public void Build() => _container = _containerBuilder.Build();
    }
}
