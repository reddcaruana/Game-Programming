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
        [Range(1f, 10f)]
        private float speed = 5f; // a float - a number with a point, followed by f
        
        [SerializeField]
        private string playerName = "Hero"; // a string - collection of characters
        
        [SerializeField]
        private bool isAlive = true; // a boolean - always true/false

        [SerializeField]
        private Vector2 bounds = new(4, 4);
        // This is a C# feature - if you declare the data type
        // new() doesn't need to repeat it.

        [SerializeField] private Renderer meshRenderer;
        
        public void Update()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            var direction = new Vector3(x, 0, y); // a temporary Vector3 for movement
            
            // direction.normalized will make sure the player moves consistently
            transform.Translate(direction.normalized * speed * Time.deltaTime);
            
            // Prints the direction (square) and its normalized (circular) value
            // Debug.Log($"Direction: {direction} / {direction.normalized}");
            
            // PSEUDOCODE
            // If K is pressed
            // Reduce health by 1
            if (Input.GetKeyDown(KeyCode.K))
            {
                // This is already a good step forward
                // health -= 1;
                // health = Mathf.Clamp(health, 0, 10);

                // Maximum between health-1 and zero
                // if health = -1, it resets to zero
                health = Mathf.Max(health - 1, 0);
            }
        }

        private void LateUpdate()
        {
            // || -> OR (will fire if one of the two conditions is true) 
            // if (transform.position.x > bounds.x || transform.position.x < -bounds.x)
            // {
            //     // Start Panicking!
            //     meshRenderer.material.color = Color.red;
            // }
            // else
            // {
            //     meshRenderer.material.color = Color.white;
            // }

            // Mathf.Abs -> Converts all numbers to positive
            var outX = Mathf.Abs(transform.position.x) > bounds.x;
            var outZ = Mathf.Abs(transform.position.z) > bounds.y;
            var outOfBounds = outX || outZ; // condition -> is out of bounds on X OR Z

            // Panic when out of bounds or low on health
            var panic = outOfBounds || health <= 1;
            
            // Inline condition that substitutes color based on the boundaries
            meshRenderer.material.color = panic
                                            ? Color.red // -> if true
                                            : Color.white; // -> if false
            
            // CHALLENGE:
            // Using Input -> reduce health when K is pressed
            // Enter panic mode if Health <= 1 or is out of bounds
            // BONUS: Health shouldn't go below zero.
        }

        // IMGUI -> Immediate Mode GUI
        // only used for testing
        private void OnGUI()
        {
            var rect = new Rect(10, 10, 200, 30);
            GUI.Label(rect, $"Health: {health}");
        }

        // This fires when the script is added or reset
        private void Reset()
        {
            meshRenderer = GetComponentInChildren<MeshRenderer>();
            
            // Warning message in case there is no renderer here
            if (meshRenderer == null)
            {
                Debug.LogWarning("Could not find a mesh renderer!");
            }
        }
    }
}