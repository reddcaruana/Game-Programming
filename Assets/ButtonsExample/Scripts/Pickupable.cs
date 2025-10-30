using UnityEngine;

namespace ButtonsExample
{
    /// <summary>
    /// This script handles the act of picking up and dropping an object.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class Pickupable : MonoBehaviour
    {
        private Rigidbody myRigidbody;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            // Early return if the object isn't picked up
            if (transform.parent == null) return;
            var target = transform.parent;
            
            // Moves the object so that it always matches the parent
            myRigidbody.MovePosition(target.position); // Moves in place
            myRigidbody.MoveRotation(target.rotation); // Rotates to face the player
        }

        public void Pickup(Transform target)
        {
            Debug.Log("I was picked up");
            
            // Parent this to the "hand" (currently)
            transform.SetParent(target);

            // Problem: If we don't move this manually, it hovers in place
            myRigidbody.isKinematic = true; // Deactivates physics (it will stick to the hand)
        }

        public void Drop()
        {
            Debug.Log("I was dropped");
            
            // Unparent from the hand
            transform.SetParent(null);

            myRigidbody.isKinematic = false; // Activates physics
        }
    }
}