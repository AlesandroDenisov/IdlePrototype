using IdleArcade.Core.Factory;
using IdleArcade.Services;
using UnityEngine;

namespace IdleArcade.Enemy
{
    public class NPCRotateToPlayer : MonoBehaviour
    {
        public float RotationSpeed;

        private Transform _playerTransform;
        private Vector3 _positionToLook;

        public void Construct(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Update()
        {
            if (_playerTransform)
                RotateTowardsPlayer();
        }

        private void RotateTowardsPlayer()
        {
            UpdatePositionToLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDelta = _playerTransform.position - transform.position;
            _positionToLook = new Vector3(positionDelta.x, transform.position.y, positionDelta.z);
        }
    
        private Quaternion SmoothedRotation(Quaternion rotation, Vector3 positionToLook)
        {
            return Quaternion.Lerp(rotation, TargetRotation(positionToLook), SpeedFactor());
        }

        private Quaternion TargetRotation(Vector3 position)
        {
            return Quaternion.LookRotation(position);
        }

        private float SpeedFactor()
        {
            return RotationSpeed * Time.deltaTime;
        }
    }
}