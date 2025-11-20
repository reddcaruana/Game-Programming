using System;
using System.Collections.Generic;
using ButtonsExample.Contracts;
using UnityEngine;

namespace ButtonsExample
{
    /// <summary>
    /// The basic functionality for a button.
    /// (if this script breaks, all buttons break in the same way)
    ///
    /// Abstract classes cannot be used as a component.
    /// They can only be inherited from.
    /// </summary>
    public abstract class ButtonBase : MonoBehaviour
    {
        // An event that other objects can register to
        // Also called a delegate function
        // Will send which button was pressed, and if it is pressed
        public event Action<ButtonBase, bool> OnTriggered;
        
        [SerializeField] private Transform buttonTop;
        
        // A collection of colliders touching this trigger
        // Accessed by my children as well, but they cannot write to this
        protected List<IPhysicsObject> touchingObjects { get; private set; } = new();
        // Since we're not serializing this list,
        // we need to create an instance of it here.
        
        private void OnTriggerEnter(Collider other)
        {
            var otherRb = other.attachedRigidbody;
            if (otherRb == null) return; // No Rigidbody was found
            if (!otherRb.TryGetComponent<IPhysicsObject>(out var physicsObject)) return; // Not a physics object
            if (touchingObjects.Contains(physicsObject)) return; // Already registered
            
            touchingObjects.Add(physicsObject);
            
            // Broadcasts the isPressed value to anyone listening
            // this -> the object that sends the message from the scene
            if (OnTriggered != null)
                OnTriggered.Invoke(this, IsPressed());
            
            UpdateVisual();
        }

        private void OnTriggerExit(Collider other)
        {
            var otherRb = other.attachedRigidbody;
            if (otherRb == null) return; // No Rigidbody was found
            if (!otherRb.TryGetComponent<IPhysicsObject>(out var physicsObject)) return; // Not a physics object
            if (!touchingObjects.Contains(physicsObject)) return; // Not registered anymore

            touchingObjects.Remove(physicsObject);
            
            if (OnTriggered != null)
                OnTriggered.Invoke(this, IsPressed());
            
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            buttonTop.localPosition = IsPressed() // is pressed?
                ? Vector3.down * 0.05f // true
                : Vector3.zero; // false
        }

        /// <summary>
        /// Forces a child to check if it meets the pressed criteria.
        /// </summary>
        protected abstract bool IsPressed();
    }
}