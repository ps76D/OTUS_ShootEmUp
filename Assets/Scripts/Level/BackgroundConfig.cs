using UnityEngine;

namespace Level
{
    [CreateAssetMenu(
        fileName = "BackgroundConfig",
        menuName = "BackgroundConfig/BackgroundConfig"
    )]
    public sealed class BackgroundConfig : ScriptableObject
    {
        [SerializeField]
        public float _startPositionY;

        [SerializeField]
        public float _endPositionY;

        [SerializeField]
        public float _movingSpeedY;
    }
}
