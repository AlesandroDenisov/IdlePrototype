using UnityEngine;
using IdleArcade.Core;

namespace IdleArcade.VFX
{
    // This class handles smoke emissions when the car is driving
    public class SmokeEmission : MonoBehaviour
    {
        /// The particle system to use to emit smoke
        public ParticleSystem Smokes;

        protected ParticleSystem.EmissionModule _smokes;

        //private CharacterController _characterController;
        private Rigidbody _rigidbody;

        public void Start()
        {
            // Initialize the emission module
            _smokes = Smokes.emission;
            _smokes.enabled = false;

            // Get the CharacterController component
            //_characterController = GetComponent<CharacterController>();

            // Get the Rigidbody component
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            // Check if the object has moved
            //if (_characterController.velocity.magnitude > Constants.Epsilon)
            if (_rigidbody.velocity.magnitude > Constants.Epsilon)
            {
                // Enable smoke emission if the object is moving
                _smokes.enabled = true;
            }
            else
            {
                // Disable smoke emission if the object is not moving
                _smokes.enabled = false;
            }
        }
    }
}