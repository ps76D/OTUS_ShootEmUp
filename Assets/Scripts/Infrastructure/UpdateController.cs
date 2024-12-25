using System;
using System.Collections.Generic;
using Infrastructure.CommonInterfaces;
using Infrastructure.Listeners;
using UnityEngine;

namespace Infrastructure
{
    public class UpdateController : MonoBehaviour
    {
        private IUpdatable[] _updatable;
        private IFixedUpdatable[] _fixedUpdatable;

        private void Start()
        {
            _updatable = FindObjectsOfTypeInterface<IUpdatable>();
            _fixedUpdatable = FindObjectsOfTypeInterface<IFixedUpdatable>();
        }

        private void Update()
        {
            foreach (var updatable in _updatable)
            {
                updatable.CustomUpdate();
            }
        }

        private void FixedUpdate()
        {
            foreach (var fixedUpdatable in _fixedUpdatable)
            {
                fixedUpdatable.CustomFixedUpdate();
            }
        }

        private static T[] FindObjectsOfTypeInterface<T>() where T : class
        {
            MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            List<T> result = new List<T>();

            foreach (var mono in monoBehaviours)
            {
                if (mono is T t)
                {
                    result.Add(t);
                }
            }
            return result.ToArray();
        }
    }
}