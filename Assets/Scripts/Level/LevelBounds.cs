using UnityEngine;

namespace Level
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [SerializeField] private Transform _leftBorder;

        [SerializeField] private Transform _rightBorder;

        [SerializeField] private Transform _downBorder;

        [SerializeField] private Transform _topBorder;
        
        public bool CheckIsInBounds(Vector3 position)
        {
            float positionX = position.x;
            float positionY = position.y;
            return positionX > this._leftBorder.position.x
                   && positionX < this._rightBorder.position.x
                   && positionY > this._downBorder.position.y
                   && positionY < this._topBorder.position.y;
        }
    }
}