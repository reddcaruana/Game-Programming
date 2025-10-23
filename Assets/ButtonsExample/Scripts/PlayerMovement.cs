using UnityEngine;
using UnityEngine.InputSystem;

namespace ButtonsExample
{
    // Attribute to automatically attach related scripts
    [RequireComponent(typeof(Rigidbody), typeof(PlayerInput))]
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody myRigidbody;
    }
}
