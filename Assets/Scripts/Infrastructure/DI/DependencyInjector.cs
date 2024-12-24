using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Infrastructure.DI
{
    public static class DependencyInjector
    {
        public static void Inject(MonoBehaviour monoBehaviour)
        {
            Type monoBehaviourType = monoBehaviour.GetType();
            var properties = monoBehaviourType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            for (int index = properties.Length - 1; index >= 0; index--)
            {
                FieldInfo property = properties[index];

                if (!property.IsDefined(typeof(InjectIEnumerableAttribute), true)) continue;
                
                Type propertyType = property.FieldType;

                if (!propertyType.IsGenericType || propertyType.GetGenericTypeDefinition() != typeof(IEnumerable<>)) continue;
                
                Type currentProperty = propertyType.GetGenericArguments()[0];
                var allListeners = ServiceLocator.GetListeners(currentProperty);
                property.SetValue(monoBehaviour, allListeners);
            }
        }
        
        public static void InjectObject(MonoBehaviour monoBehaviour)
        {
            Type monoBehaviourType = monoBehaviour.GetType();
            var fields = monoBehaviourType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            for (int index = fields.Length - 1; index >= 0; index--)
            {
                FieldInfo fieldInfo = fields[index];
                
                if (!fieldInfo.IsDefined(typeof(InjectCustomAttribute), false)) continue;
                
                Type fieldType = fieldInfo.FieldType;
                object value = ServiceLocator.GetService(fieldType);
                fieldInfo.SetValue(monoBehaviour, value);
            }
        }
    }
}