using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Unity;

namespace DrawingsControllSystem.Common
{
    public sealed class Container : IFacadeContainer
    {
        private readonly IUnityContainer container = new UnityContainer();

        public void RegisterType<TType>() where TType : class
        {
            container.RegisterType<TType>();
        }

        public void RegisterType<TType, TConcrete>() where TConcrete : class, TType
        {
            container.RegisterType<TType, TConcrete>();
        }

        public void RegisterSingleton<TType>() where TType : class
        {
            container.RegisterSingleton<TType>();
        }

        public void RegisterSingleton<TType, TConcrete>() where TConcrete : class, TType
        {
            container.RegisterSingleton<TType, TConcrete>();
        }


        // TODO: Подумать как реализовать
        //public static void RegisterInstance<TType>(TType instance) where TType : class
        //{
        //    container.RegisterInstance()
        //}

        //public static void RegisterInstance<TType, TConcrete>(TConcrete instance) where TConcrete : class, TType
        //{
        //}

        public TTypeToResolve Resolve<TTypeToResolve>()
        {
            return container.Resolve<TTypeToResolve>();
        }

        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }
    }
}