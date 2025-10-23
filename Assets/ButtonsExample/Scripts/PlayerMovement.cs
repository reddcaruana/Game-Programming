using UnityEngine;
using UnityEngine.InputSystem;

namespace ButtonsExample
{
    // Attribute to automatically attach related scripts
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        // Serialized Fields
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float turnSpeed = 1080f;
        
        // Components
        private Rigidbody myRigidbody;

        // Runtime Variables
        private Vector3 direction;

        // This is where we look up components
        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        // A reliable update method for physics (Rigidbodies)
        private void FixedUpdate()
        {
            var movement = direction * moveSpeed; // first, apply the controls
            movement.y = myRigidbody.linearVelocity.y; // reapply the falling speed
            
            myRigidbody.linearVelocity = movement;
            myRigidbody.angularVelocity = Vector3.zero; // stops random rotation

            // Early return
            // The code will stop here if this is true
            // Stops rotating if there is no movement
            // Magnitude - all the variables inside a vector combined in a single number
            // Epsilon - a tiny number
            if (direction.magnitude <= Mathf.Epsilon) return;
            
            // We can transition the object's rotation 
            var currentRotation = myRigidbody.rotation;
            var targetRotation = Quaternion.LookRotation(direction);
            var rotation = Quaternion.RotateTowards(currentRotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
            
            myRigidbody.MoveRotation(rotation);
        }

        // Triggered by the PlayerInput component
        private void OnMove(InputValue value)
        {
            var input = value.Get<Vector2>(); // Reads the actual input from the input system
            direction = Vector3.forward * input.y // adds up a Vector3 in steps
                        + Vector3.right * input.x; // instead of a new() version
        }
    }
}
