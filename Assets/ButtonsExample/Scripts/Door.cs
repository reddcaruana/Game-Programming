using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ButtonsExample
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform doorObject;
        [SerializeField] private ButtonBase[] buttons;

        // Key/Value pair similar to PHP
        // We don't have to create this inside a method
        private Dictionary<ButtonBase, bool> buttonStates = new();
        
        // Can be read by all classes, but only I can change the value
        public bool isOpen
        {
            get
            {
                // Loops through all true/false values
                foreach (var isPressed in buttonStates.Values)
                {
                    // If one button isn't pressed, we're not open
                    if (!isPressed) return false;
                }

                // All buttons are pressed (^~.~)^
                return true;
            }
        }

        // We listen to the push button
        private void OnEnable()
        {
            // We avoid duplication later
            buttonStates.Clear();
            
            foreach (var button in buttons)
            {
                // We might have forgotten to set this uwu
                if (button == null)
                {
                    Debug.LogWarning("I'm not connected to a button!");
                    continue; // Skips the loop if we find one empty button
                }

                buttonStates[button] = false; // every button starts off disabled
                button.OnTriggered += OnButtonTriggered;
            }
        }

        // We stop listening to the push button
        private void OnDisable()
        {
            foreach (var button in buttons)
            {
                // The button might have been destroyed before me
                // this unsubscribes
                if (button != null)
                    button.OnTriggered -= OnButtonTriggered;
            }
            
            buttonStates.Clear();
        }

        /// <summary>
        /// Will listen to any buttons and receive their status.
        /// </summary>
        /// <param name="button">The button that changed.</param>
        /// <param name="isPressed">The value of that button.</param>
        private void OnButtonTriggered(ButtonBase button, bool isPressed)
        {
            Debug.Log($"{button.name} was changed");
            
            // Change the state of a single button
            buttonStates[button] = isPressed;
            
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