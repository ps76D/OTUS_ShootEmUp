using System;
using System.Collections;
using Infrastructure.Listeners;
using UnityEngine;

namespace GameManager
{
    public class TimeManager : MonoBehaviour, IPauseGameListener, IStartGameListener, IFinishGameListener, IResumeGameListener, IInGameListener
    {
        [SerializeField] private float _duration = 3f;
        [SerializeField] private float _startOffset;
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void StopTime(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }

        public void PauseGame()
        {
            StopTime(true);
        }

        public void FinishGame()
        {
            StopTime(true);
        }

        public void StartGame()
        {
            StopTime(false);
        }

        public void ResumeGame()
        {
            StartCoroutine(StopTimeCoroutine(false));
        }
        
        public void InGame()
        {
            StopTime(true);
            StartCoroutine(StopTimeCoroutine(false));
        }

        private IEnumerator StopTimeCoroutine(bool value)
        {
            yield return new WaitForSecondsRealtime(_duration + _startOffset);
            
            StopTime(value);
        }
    }
}