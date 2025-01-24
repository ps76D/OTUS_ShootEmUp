using System;
using System.Collections;
using Infrastructure;
using Infrastructure.DI;
using Infrastructure.Listeners;
using UI.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class StartCountdownWidget : UIScreen, IInGameListener, IResumeGameListener
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Image _blockRaycast;
        [SerializeField] private string _playAnim = "Appear";
        [SerializeField] private float _duration = 3f;
        [SerializeField] private float _startOffset;

        private void Awake()
        {
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

        public void InGame()
        {
            StartCoroutine(StartCountdown());
        }

        public void ResumeGame()
        {
            StartCoroutine(StartCountdown());
        }

        private IEnumerator StartCountdown()
        {
            _blockRaycast.raycastTarget = true;
            yield return new WaitForSecondsRealtime(_startOffset);
            
            _animator.Play(_playAnim);
            yield return new WaitForSecondsRealtime(_duration);
            
            _blockRaycast.raycastTarget = false;
        }
    }
}