using System;
using Infrastructure.Listeners;
using UnityEngine;

namespace GameManager
{
    public class TimeManager : MonoBehaviour, IPauseGameListener, IStartGameListener, IFinishGameListener, IResumeGameListener
    {
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
            StopTime(false);
        }
    }
}