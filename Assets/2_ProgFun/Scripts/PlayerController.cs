using UnityEngine;

// Defines a library in C#
namespace _2_ProgFun
{
    public class PlayerController : MonoBehaviour
    {
        // The four simple data types that are used in games
        [SerializeField] // Exposes private variables inside the Inspector
        [Min(0)]
        private int health = 100; // an integer - a whole number
        
        [SerializeField]
        [Range(1f, 5f)]
        private float speed = 5f; // a float - a number with a point, followed by f
        
        [SerializeField]
        private string playerName = "Hero"; // a string - collection of characters
        
        [SerializeField]
        private bool isAlive = true; // a boolean - always true/false

        public void Update()
        {
            var move = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.right * move * speed * Time.deltaTime);
        }
    }
}