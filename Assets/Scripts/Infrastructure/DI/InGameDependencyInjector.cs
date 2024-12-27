using System;
using System.Collections.Generic;
using System.Reflection;
using GameManager;
using UnityEngine;

namespace Infrastructure.DI
{
    public class InGameDependencyInjector
    {
        private readonly InGameServiceLocator _serviceLocator;
        public InGameDependencyInjector(InGameServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }
        
        public void InjectLocalObject(MonoBehaviour monoBehaviour)
        {
            Type monoBehaviourType = monoBehaviour.GetType();
            var fields = monoBehaviourType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            for (int index = fields.Length - 1; index >= 0; index--)
            {
                FieldInfo fieldInfo = fields[index];
                
                if (!fieldInfo.IsDefined(typeof(InjectCustomLocalAttribute), false)) continue;
                
                Type fieldType = fieldInfo.FieldType;
                object value = _serviceLocator.GetService(fieldType);
                fieldInfo.SetValue(monoBehaviour, value);
            }
        }
        
        public void InjectLocal(MonoBehaviour monoBehaviour)
        {
            Type monoBehaviourType = monoBehaviour.GetType();
            var properties = monoBehaviourType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            for (int index = properties.Length - 1; index >= 0; index--)
            {
                FieldInfo property = properties[index];

                if (!property.IsDefined(typeof(InjectIEnumerableLocalAttribute), true)) continue;
                
                Type propertyType = property.FieldType;

                if (!propertyType.IsGenericType || propertyType.GetGenericTypeDefinition() != typeof(IEnumerable<>)) continue;
                
                Type currentProperty = propertyType.GetGenericArguments()[0];
                var allListeners = _serviceLocator.GetListeners(currentProperty);
                property.SetValue(monoBehaviour, allListeners);
            }
        }
    }
}