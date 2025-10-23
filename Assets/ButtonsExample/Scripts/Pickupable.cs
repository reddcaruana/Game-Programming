using UnityEngine;

namespace ButtonsExample
{
    [RequireComponent(typeof(Rigidbody))]
    public class Pickupable : MonoBehaviour
    {
        private Rigidbody myRigidbody;

        private void Awake()
        {
            myRigidbody = GetComponent<Rigidbody>();
        }

        public void Pickup()
        {
            Debug.Log("I was picked up");
        }

        public void Drop()
        {
            Debug.Log("I was dropped");
        }
    }
}