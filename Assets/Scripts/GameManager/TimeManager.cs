using UnityEngine;

namespace GameManager
{
    public static class TimeManager
    {
        public static void StopTime(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }
    }
}