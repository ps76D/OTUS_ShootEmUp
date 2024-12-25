using System;
using System.Collections.Generic;

namespace Infrastructure.DI
{
    public class InGameServiceLocator 
    {
        private readonly static Dictionary<Type, object> ServicesObjects = new();
        private readonly static Dictionary<Type, IEnumerable<object>> Services = new();

        public void AddListeners<T>(IEnumerable<object> service)
        {
            Services[typeof(T)] = service;
        }

        public IEnumerable<T> GetListeners<T>() where T : class
        {
            return Services[typeof(T)] as IEnumerable<T>;
        }

        public IEnumerable<object> GetListeners(Type t)
        {
            return Services[t];
        }
        
        public void AddService(Type serviceType, object service)
        {
            ServicesObjects[serviceType] = service;
        }
        
        public T GetService<T>() where T : class
        {
            return ServicesObjects[typeof(T)] as T;
        }
        
        public object GetService(Type serviceType)
        {
            return ServicesObjects[serviceType];
        }
    }
}