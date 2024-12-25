using System;
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
    }
}