using UnityEngine;
using UnityEngine.InputSystem;

namespace ButtonsExample
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerCarry : MonoBehaviour
    {
        [SerializeField] private Transform source; // Where I'm checking for items
        [SerializeField] private float radius = 1f; // How big the range is

        // The object that we're currently holding
        [SerializeField]
        private Pickupable objectInHands;
        
        private void OnInteract(InputValue value)
        {
            // If we're already holding an object, there's no need to continue the method
            if (objectInHands != null)
            {
                objectInHands.Drop();
                objectInHands = null;
                return;
            }
            
            // The origin point
            var origin = source.position;
            var results = new Collider[4]; // The result of our raycast
            
            // Will return the number of objects found in space
            var hitCount = Physics.OverlapSphereNonAlloc(origin, radius, results);

            // Quick loop to print object names
            for (var i = 0; i < hitCount; i++)
            {
                // Make a result readable
                var result = results[i];
                if (result.attachedRigidbody == null) continue; // Breaks the loop

                // Look for the attached Rigidbody
                // Try to get the Pickupable script
                // If it doesn't exist, this loop fails
                // ! equates to a false
                if (!result.attachedRigidbody.TryGetComponent<Pickupable>(out var pickupable))
                    continue;

                // Pick up the object
                objectInHands = pickupable; // Lets the script know we're holding an object
                pickupable.Pickup(source);
                
                // Breaks the function if one item was picked up
                return;
            }
        }
    }
}