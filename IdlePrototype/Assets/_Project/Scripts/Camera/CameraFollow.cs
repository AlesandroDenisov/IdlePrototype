using UnityEngine;

namespace IdleArcade.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float _rotationAngleX;
        [SerializeField] private float _rotationAngleY;
        [SerializeField] private float _rotationAngleZ;
        [SerializeField] private float _distance;
        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetZ;

        private Transform _following;

        private void LateUpdate()
        {
            if (_following == null)
            {
                Debug.Log("CameraFollow - LateUpdate: _following is null");
                return;
            }

            Quaternion rotation = Quaternion.Euler(_rotationAngleX, _rotationAngleY, _rotationAngleZ);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following)
        {
            if (following == null)
            {
                Debug.LogError("CameraFollow - Follow: following is null");
                return;
            }

            _following = following.transform;
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.x += _offsetX;
            followingPosition.y += _offsetY;
            followingPosition.z += _offsetZ;

            return followingPosition;
        }
    }
}