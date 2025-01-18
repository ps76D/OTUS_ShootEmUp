using System;
using Infrastructure.CommonInterfaces;
using Level;
using UnityEngine;


namespace Input
{
    public sealed class InputManager : IUpdatable
    {
        private readonly float _moveStep;
        
        public InputManager(InputConfig config)
        {
            _moveStep = config.MoveStep;
        }

        public bool IsActive = true;
        public event Action OnPlayerFire;
        
        public event Action<float> OnPlayerMove;
        
        public void CustomUpdate()
        {
            if (!IsActive) return;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                OnPlayerFire?.Invoke();
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                OnPlayerMove?.Invoke(-_moveStep);
            }
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                OnPlayerMove?.Invoke(_moveStep);
            }
            else
            {
                OnPlayerMove?.Invoke(0);
            }
        }
    }
}