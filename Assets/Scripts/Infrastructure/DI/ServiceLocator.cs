using System;
using System.Collections.Generic;

namespace Infrastructure.DI
{
    public static class ServiceLocator 
    {
        private readonly static Dictionary<Type, object> ServicesObjects = new();
        private readonly static Dictionary<Type, IEnumerable<object>> Services = new();

        public static void AddListeners<T>(IEnumerable<object> service)
        {
            Services[typeof(T)] = service;
        }

        public static IEnumerable<T> GetListeners<T>() where T : class
        {
            return Services[typeof(T)] as IEnumerable<T>;
        }

        public static IEnumerable<object> GetListeners(Type t)
        {
            return Services[t];
        }
        
        public static void AddService(Type serviceType, object service)
        {
            ServicesObjects[serviceType] = service;
        }
        
        public static T GetService<T>() where T : class
        {
            return ServicesObjects[typeof(T)] as T;
        }
        
        public static object GetService(Type serviceType)
        {
            return ServicesObjects[serviceType];
        }
    }
}