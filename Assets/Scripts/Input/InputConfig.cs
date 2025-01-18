using UnityEngine;

namespace Input
{
    [CreateAssetMenu(
        fileName = "InputConfig",
        menuName = "InputConfig/InputConfig"
    )]
    public sealed class InputConfig : ScriptableObject
    {
        [SerializeField] private float _moveStep = 1.0f;

        public float MoveStep => _moveStep;
    }
}
