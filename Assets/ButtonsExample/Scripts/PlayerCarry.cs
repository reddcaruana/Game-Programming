using UnityEngine;
using UnityEngine.InputSystem;

namespace ButtonsExample
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerCarry : MonoBehaviour
    {
        [SerializeField] private Transform source; // Where I'm checking for items
        [SerializeField] private float radius = 1f; // How big the range is
        
        private void OnInteract(InputValue value)
        {
            // The origin point
            var origin = source.position;
            var results = new RaycastHit[4]; // The result of our raycast
            
            // Will return the number of objects found in space
            var hitCount = Physics.SphereCastNonAlloc(origin, radius, transform.forward, results);

            // Quick loop to print object names
            for (var i = 0; i < hitCount; i++)
            {
                Debug.Log($"I found {results[i].transform.name}");
            }
        }
    }
}