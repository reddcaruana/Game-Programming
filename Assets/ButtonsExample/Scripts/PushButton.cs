using System;
using System.Collections.Generic;
using ButtonsExample.Contracts;
using UnityEngine;

namespace ButtonsExample
{
    public class PushButton : MonoBehaviour
    {
        // An event that other objects can register to
        // Also called a delegate function
        public event Action<bool> OnTriggered;
        
        [SerializeField] private bool isPressed;
        [SerializeField] private Transform buttonTop;
        
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
            
            // Broadcasts the isPressed value to anyone listening
            if (OnTriggered != null)
                OnTriggered.Invoke(isPressed);
            
            UpdateVisual();
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
            
            if (OnTriggered != null)
                OnTriggered.Invoke(isPressed);
            
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            buttonTop.localPosition = isPressed // is pressed?
                ? Vector3.down * 0.05f // true
                : Vector3.zero; // false
        }
    }
}