using System;
using System.Collections.Generic;

namespace Infrastructure.DI
{
    public class InGameServiceLocator 
    {
        private readonly static Dictionary<Type, IEnumerable<object>> Services = new();

        public void AddListeners<T>(IEnumerable<object> service)
        {
            Services[typeof(T)] = service;
        }
        
        public IEnumerable<object> GetListeners(Type t)
        {
            return Services[t];
        }
    }
}