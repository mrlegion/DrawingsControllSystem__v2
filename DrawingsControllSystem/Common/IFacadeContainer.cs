using System;

namespace DrawingsControllSystem.Common
{
    public interface IFacadeContainer
    {
        void RegisterType<TType>() where TType : class;
        void RegisterType<TType, TConcrete>() where TConcrete : class, TType;
        void RegisterSingleton<TType>() where TType : class;
        void RegisterSingleton<TType, TConcrete>() where TConcrete : class, TType;
        TTypeToResolve Resolve<TTypeToResolve>();
        object Resolve(Type type);
    }
}