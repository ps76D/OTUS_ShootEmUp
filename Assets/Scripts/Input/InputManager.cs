using System;
using Infrastructure.CommonInterfaces;
using UnityEngine;

namespace Input
{
    public sealed class InputManager : MonoBehaviour, IUpdatable
    {
        [SerializeField] private float _moveStep = 1.0f;
        
        public static event Action OnPlayerFire;
        
        public static event Action<float> OnPlayerMove;
        
        public void CustomUpdate()
        {
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