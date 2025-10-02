using UnityEngine;

public class HelloWorld : MonoBehaviour
{
    // We use to set up components on an object
    // Fires while the level is loading
    private void Awake()
    {
        Debug.Log("I am awake! :)");
    }

    // We use to set up variables connected to this script
    // e.g. Health, character's name, speed
    // Fires AFTER the scene loads (and the script is enabled)
    void Start()
    {
        Debug.Log("Start is running!");
    }

    // Per-frame cycle
    // We perform code that needs to be quick
    void Update()
    {
        Debug.Log("Update is running!");
    }

    // Physics update
    // Runs at 50fps and is used ideally with rigidbody
    private void FixedUpdate()
    {
        Debug.Log("Fixed Update is running!");
    }

    // Always fires after Update (per-frame update)
    // This is useful when we want to delay specific actions
    // e.g. updating a camera after the player has moved
    private void LateUpdate()
    {
        Debug.Log("I am updating late!");
    }

    // Fires when the object is enabled
    // We can trigger this manually using the checkbox on the script
    private void OnEnable()
    {
        Debug.Log("I am turned on ;)");
    }

    // Fires when the object is disabled
    // We can trigger this manually using the checkbox on the script
    private void OnDisable()
    {
        Debug.Log("I am turned off :(");
    }
}
