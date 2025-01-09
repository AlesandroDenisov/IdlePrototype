
using IdleArcade.Core.Factory;
using IdleArcade.Services;
using UnityEngine;

namespace IdleArcade.Player
{
    public class RotateToTarget : MonoBehaviour
    {
        public float Speed;

        private Transform _targetTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();
/*
            if (IsPlayerExist())
                InitializePlayerTransform();
            else
                _gameFactory.PlayerCreated += OnPlayerCreated;*/
        }

        private void Update()
        {
            if (IsInitialized())
                RotateTowardsTarget();
        }

        private void OnDestroy()
        {
/*            if (_gameFactory != null)
                _gameFactory.PlayerCreated -= OnPlayerCreated;*/
        }

/*        private bool IsPlayerExist()
        {
            return _gameFactory.PlayerGameObject != null;
        }*/

        private void RotateTowardsTarget()
        {
            UpdatePositionToLookAt();
            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDelta = _targetTransform.position - transform.position;
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
            return Speed * Time.deltaTime;
        }

        private bool IsInitialized()
        {
            return _targetTransform != null;
        }

/*        private void OnPlayerCreated()
        {
            InitializeTargetTransform();
        }*/

        private void InitializeTargetTransform()
        {
            //_targetTransform = _gameFactory.PlayerGameObject.transform;
        }
    }
}
