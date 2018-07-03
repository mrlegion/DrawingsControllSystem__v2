// BACKUP THIS CLASS

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Windows.Media;

//namespace DrawingsControllSystem.Common
//{
//    internal static class Container
//    {
//        private static readonly IDictionary<Type, RegisteredObject> registeredObjects;

//        static Container()
//        {
//            registeredObjects = new Dictionary<Type, RegisteredObject>();
//        }

//        public static void RegisterType<TType>() where TType : class
//        {
//            Register<TType, TType>(false, null);
//        }

//        public static void RegisterType<TType, TConcrete>() where TConcrete : class, TType
//        {
//            Register<TType, TConcrete>(false, null);
//        }

//        public static void RegisterSingleton<TType>() where TType : class
//        {
//            RegisterSingleton<TType, TType>();
//        }

//        public static void RegisterSingleton<TType, TConcrete>() where TConcrete : class, TType
//        {
//            Register<TType, TConcrete>(true, null);
//        }

//        public static void RegisterInstance<TType>(TType instance) where TType : class
//        {
//            RegisterInstance<TType, TType>(instance);
//        }

//        public static void RegisterInstance<TType, TConcrete>(TConcrete instance) where TConcrete : class, TType
//        {
//            Register<TType, TConcrete>(true, instance);
//        }

//        public static TTypeToResolve Resolve<TTypeToResolve>()
//        {
//            return (TTypeToResolve)ResolveObject(typeof(TTypeToResolve));
//        }

//        public static object Resolve(Type type)
//        {
//            return ResolveObject(type);
//        }

//        private static void Register<TType, TConcrete>(bool isSingleton, TConcrete instance)
//        {
//            Type type = typeof(TType);

//            if (registeredObjects.ContainsKey(type))
//                registeredObjects.Remove(type);

//            registeredObjects.Add(type, new RegisteredObject(typeof(TConcrete), isSingleton, instance));
//        }

//        private static object ResolveObject(Type type)
//        {
//            var registeredObject = registeredObjects[type];

//            if (registeredObject == null)
//                throw new ArgumentOutOfRangeException($"The type {type.Name} has not been registered");

//            return GetInstance(registeredObject);
//        }

//        private static object GetInstance(RegisteredObject registeredObject)
//        {
//            object instance = registeredObject.SingletonInstance;

//            if (instance == null)
//            {
//                var parameters = ResolveConstructorParameters(registeredObject);
//                instance = registeredObject.CreateInstance(parameters.ToArray());
//            }

//            return instance;
//        }

//        private static IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
//        {
//            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
//            return constructorInfo.GetParameters().Select(parameter => ResolveObject(parameter.ParameterType));
//        }

//        private class RegisteredObject
//        {
//            private readonly bool isSingleton;

//            public Type ConcreteType { get; private set; }

//            public object SingletonInstance { get; private set; }

//            public RegisteredObject(Type concreteType, bool isSingleton, object instance)
//            {
//                ConcreteType = concreteType;
//                this.isSingleton = isSingleton;
//                SingletonInstance = instance;
//            }

//            public object CreateInstance(params object[] args)
//            {
//                object instance = Activator.CreateInstance(ConcreteType, args);
//                if (isSingleton)
//                    SingletonInstance = instance;
//                return instance;
//            }
//        }
//    }
//}