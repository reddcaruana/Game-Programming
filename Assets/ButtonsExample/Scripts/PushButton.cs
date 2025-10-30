using System.Collections.Generic;
using ButtonsExample.Contracts;
using UnityEngine;

namespace ButtonsExample
{
    public class PushButton : MonoBehaviour
    {
        [SerializeField] private bool isPressed;

        // A collection of colliders touching this trigger
        private List<IPhysicsObject> touchingObjects = new();
        // Since we're not serializing this list,
        // we need to create an instance of it here.
        
        private void OnTriggerEnter(Collider other)
        {
            var otherRb = other.attachedRigidbody;
            if (otherRb == null) return; // No Rigidbody was found
            if (!otherRb.TryGetComponent<IPhysicsObject>(out var physicsObject)) return; // Not a physics object
            if (touchingObjects.Contains(physicsObject)) return; // Already registered
            
            touchingObjects.Add(physicsObject);
            isPressed = true;
        }

        private void OnTriggerExit(Collider other)
        {
            var otherRb = other.attachedRigidbody;
            if (otherRb == null) return; // No Rigidbody was found
            if (!otherRb.TryGetComponent<IPhysicsObject>(out var physicsObject)) return; // Not a physics object
            if (!touchingObjects.Contains(physicsObject)) return; // Not registered anymore

            touchingObjects.Remove(physicsObject);
            if (touchingObjects.Count > 0) return;
            
            isPressed = false;
        }
    }
}