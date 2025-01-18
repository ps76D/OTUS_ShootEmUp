using UnityEngine;

namespace Level
{
    public sealed class LevelBounds : MonoBehaviour
    {
        [SerializeField] private Camera _cam;
        
        [SerializeField] private Transform _backTransform;
        
        [SerializeField] private Transform _leftBorder;

        [SerializeField] private Transform _rightBorder;

        [SerializeField] private Transform _downBorder;

        [SerializeField] private Transform _topBorder;

        public Transform BackTransform => _backTransform;
        
        private void Start()
        {
            _cam = FindObjectOfType<Camera>();

            var position = _cam.transform.position;
            float leftBorder = _cam.ViewportToWorldPoint(new Vector3(0.95f, 0.5f, position.z)).x;
            
            Vector3 newLeftPosition = _leftBorder.position;
            newLeftPosition.x = leftBorder;
            _leftBorder.position = newLeftPosition;

            float rightBorder = _cam.ViewportToWorldPoint(new Vector3(0.05f, 0.5f, position.z)).x;
            
            Vector3 newRightPosition = _rightBorder.position;
            newRightPosition.x = rightBorder;
            _rightBorder.position = newRightPosition;
        }
        
        public bool CheckIsInBounds(Vector3 position)
        {
            float positionX = position.x;
            float positionY = position.y;
            return positionX > _leftBorder.position.x
                   && positionX < _rightBorder.position.x
                   && positionY > _downBorder.position.y
                   && positionY < _topBorder.position.y;
        }

        public Transform GetLeftBorder()
        {
            return _leftBorder;
        }
        
        public Transform GetRightBorder()
        {
            return _rightBorder;
        }
    }
}