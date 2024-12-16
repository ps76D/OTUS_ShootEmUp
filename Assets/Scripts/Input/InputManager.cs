using System;
using UnityEngine;

namespace Input
{
    public sealed class InputManager : MonoBehaviour
    {
        [SerializeField] private float _moveStep = 1.0f;
        
        public static event Action OnPlayerFire;
        
        public static event Action<float> OnPlayerMove;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                OnPlayerFire?.Invoke();
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                OnPlayerMove?.Invoke(-this._moveStep);
            }
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                OnPlayerMove?.Invoke(this._moveStep);
            }
            else
            {
                OnPlayerMove?.Invoke(0);
            }
        }
    }
}