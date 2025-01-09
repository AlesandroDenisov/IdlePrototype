using IdleArcade.Data;
using IdleArcade.Core;
using IdleArcade.Services;
using IdleArcade.Services.Input;
using IdleArcade.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IdleArcade.Player
{
    public class PlayerMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Rigidbody _rigidbody;

        [Header("MOVEMENT")]
        [SerializeField] 
        private float _movementSpeed = 10f;
        [SerializeField]
        private float _rotationSpeed = 6f;

        private IInputService _inputService;
        private Camera _camera;

        private enum MovementType
        {
            MoveAndRotate,
            RotateAndMove,
        }

        [Header("CONTROLS")]
        [SerializeField]
        private MovementType _movementType;


        [Header("CAR WHEELS")]
        [SerializeField] private Transform ParentFL;
        [SerializeField] private Transform ParentFR;
        [SerializeField] private Transform wheelFL;
        [SerializeField] private Transform wheelFR;
        [SerializeField] private Transform wheelRL;
        [SerializeField] private Transform wheelRR;
        [SerializeField] float maxSteer = 40f;
        [SerializeField] float steerSpeed = 2f;

        private float _currentSteeringAngle;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _camera = Camera.main;
        }

        void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService == null)
                Debug.LogError("Input Service is not set. Unable to get Axis.");

            // TODO: NullReferenceException: Object reference not set to an instance of an object

            // Use input service to get the movement vector
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                if (_camera == null)
                    Debug.LogError("Camera is not set. Unable to transform direction.");

                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            //movementVector += Physics.gravity;

            //_characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
           // _rigidbody.MovePosition(_rigidbody.position + _movementSpeed * movementVector * Time.deltaTime);

            HandleMovement(movementVector);
            RotateWheelsBasedOnSpeed();
            UpdateSteering(movementVector);

        }

        public void ChangeSpeed(float speed)
        {
            _movementSpeed = speed;
        }

        public void ResetOriginalSpeed()
        {
            // TODO: take speed from ProgressData
            _movementSpeed = 10f;
        }

        private void HandleMovement(Vector3 direction)
        {
            if (direction.sqrMagnitude > 0.01f)
            {
                if (_movementType == MovementType.MoveAndRotate)
                {
                    MoveAndRotate(direction, direction.magnitude);
                }
                else
                {
                    RotateAndMove(direction, direction.magnitude);
                }

            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }

        private void MoveAndRotate(Vector3 direction, float speedMultiplier)
        {
            _rigidbody.velocity = direction * _movementSpeed * speedMultiplier;
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed));
        }

        private void RotateAndMove(Vector3 direction, float speedMultiplier)
        {
            _rigidbody.velocity = transform.forward * _movementSpeed * speedMultiplier;
            _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), _rotationSpeed));
        }

        private void RotateWheelsBasedOnSpeed()
        {
            float speed = _rigidbody.velocity.magnitude;
            float rotationAmount = speed * Time.fixedDeltaTime * 360; // Rotate 360 degrees per unit of speed per second
            RotateWheel(wheelFL, rotationAmount);
            RotateWheel(wheelFR, rotationAmount);
            RotateWheel(wheelRL, rotationAmount);
            RotateWheel(wheelRR, rotationAmount);
        }

        private void UpdateSteering(Vector3 direction)
        {

            if (direction == Vector3.zero)
            {
                return;
            }

            float stickDir = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float diff = stickDir - _currentSteeringAngle;

            diff = (diff + 180) % 360 - 180;

            if ((diff > maxSteer))
            {
                diff = maxSteer;
                _currentSteeringAngle = stickDir - maxSteer;
            }
            else if (diff < -maxSteer)
            {
                diff = -maxSteer;
                _currentSteeringAngle = stickDir + maxSteer;
            }

            _currentSteeringAngle += Mathf.Sign(diff) * Mathf.Min(Mathf.Abs(diff), steerSpeed);

            ApplySteering(diff);
        }

        private void ApplySteering(float steeringAngle)
        {
            ParentFL.localEulerAngles = new Vector3(ParentFL.localEulerAngles.x, steeringAngle, ParentFL.localEulerAngles.z);
            ParentFR.localEulerAngles = new Vector3(ParentFR.localEulerAngles.x, steeringAngle, ParentFR.localEulerAngles.z);
        }

        private void RotateWheel(Transform wheel, float amount)
        {
            wheel.Rotate(amount, 0, 0, Space.Self);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level) return;

            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            
            if (savedPosition != null)
                Warp(to: savedPosition);
        }

        private static string CurrentLevel()
        {
            return SceneManager.GetActiveScene().name;
        }

        private void Warp(Vector3Data to)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.detectCollisions = false;
            transform.position = to.AsUnityVector().AddY(0.1f); // up the player on his charController height
            _rigidbody.isKinematic = false;
            _rigidbody.detectCollisions = true;
        }
    }
}