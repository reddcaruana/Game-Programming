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
