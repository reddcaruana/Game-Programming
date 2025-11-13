using UnityEngine;

namespace ButtonsExample
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform doorObject;
        [SerializeField] private PushButton button;

        // Can be read by all classes, but only I can change the value
        public bool isOpen { get; private set; }

        // We listen to the push button
        private void OnEnable()
        {
            // We might have forgotten to set this uwu
            if (button == null)
            {
                Debug.LogWarning("I'm not connected to a button!");
                return;
            }

            button.OnTriggered += OnButtonTriggered;
        }

        // We stop listening to the push button
        private void OnDisable()
        {
            // We might have forgotten to set this uwu
            if (button == null)
            {
                Debug.LogWarning("I'm not connected to a button!");
                return;
            }

            // this unsubscribes
            button.OnTriggered -= OnButtonTriggered;
        }

        private void OnButtonTriggered(bool isPressed)
        {
            isOpen = isPressed;
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            doorObject.localPosition = isOpen
                ? Vector3.down * 1.5f
                : Vector3.zero;
        }
    }
}