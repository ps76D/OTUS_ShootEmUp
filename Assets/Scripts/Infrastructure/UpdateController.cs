using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.CommonInterfaces;
using Infrastructure.DI;
using Infrastructure.Listeners;
using UnityEngine;

namespace Infrastructure
{
    public class UpdateController : MonoBehaviour, IPauseGameListener, IFinishGameListener, IResumeGameListener, IInGameListener
    {
        [SerializeField] private float _duration = 3f;
        [SerializeField] private float _startOffset;
        
        private IUpdatable[] _updatable;
        private IFixedUpdatable[] _fixedUpdatable;

        private GameStateMachine _gameStateMachine;

        [SerializeField] private bool _isNeedUpdate;

        private void Start()
        {
            _updatable = FindObjectsOfTypeInterface<IUpdatable>();
            _fixedUpdatable = FindObjectsOfTypeInterface<IFixedUpdatable>();
        }
        
        private void Update()
        {
            if (!_isNeedUpdate) return;
            foreach (IUpdatable updatable in _updatable)
            {
                updatable.CustomUpdate();
            }
        }

        private void FixedUpdate()
        {
            if (!_isNeedUpdate) return;
            foreach (IFixedUpdatable fixedUpdatable in _fixedUpdatable)
            {
                fixedUpdatable.CustomFixedUpdate();
            }
        }

        private static T[] FindObjectsOfTypeInterface<T>() where T : class
        {
            var monoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            var result = new List<T>();

            foreach (MonoBehaviour mono in monoBehaviours)
            {
                if (mono is T t)
                {
                    result.Add(t);
                }
            }
            return result.ToArray();
        }

        public void PauseGame()
        {
            _isNeedUpdate = false;
        }

        public void FinishGame()
        {
            _isNeedUpdate = false;
        }

        public void ResumeGame()
        {
            _isNeedUpdate = false;
        }

        public void InGame()
        {
            Debug.Log("InGame in Update Controller");
            _isNeedUpdate = false;
            StartCoroutine(StartUpdateCoroutine());
        }
        
        private IEnumerator StartUpdateCoroutine()
        {
            yield return new WaitForSeconds(_duration + _startOffset);
            
            _isNeedUpdate = true;
        }
    }
}