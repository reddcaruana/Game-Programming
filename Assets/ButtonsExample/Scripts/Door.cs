using System.Collections.Generic;
using UnityEngine;

namespace ButtonsExample
{
    // Keywords representing different states for a door
    // To avoid boolean soup
    public enum DoorState
    {
        Closed,
        Open,
        Locked,
        Broken
    }
    
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform doorObject;
        [SerializeField] private ButtonBase[] buttons;

        // Key/Value pair similar to PHP
        // We don't have to create this inside a method
        private Dictionary<ButtonBase, bool> buttonStates = new();

        [SerializeField] private DoorState currentState;
        [SerializeField] private bool shouldBreakWhenOpen;
        
        // Can be read by all classes, but only I can change the value
        // Returns the private variable
        public DoorState CurrentState => currentState;

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
            
            CheckDoor();
        }

        /// <summary>
        /// This changes the door state based on the conditions.
        /// </summary>
        private void CheckDoor()
        {
            switch (CurrentState)
            {
                case DoorState.Closed:
                    
                    // When the door is closed, we just check if it should open
                    // assigns isPressed to every value in buttonStates (true/false)
                    foreach (var isPressed in buttonStates.Values)
                    {
                        if (!isPressed) return;
                    }

                    currentState = shouldBreakWhenOpen
                        ? DoorState.Broken
                        : DoorState.Open;
                    
                    break;
                
                case DoorState.Open:
                    
                    // if any button isn't pressed, we close the door
                    foreach (var isPressed in buttonStates.Values)
                    {
                        if (!isPressed)
                        {
                            currentState = DoorState.Closed;
                            break; // We stop the switch from checking more code
                        }
                    }

                    break;
            }
            
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            var isOpen = CurrentState is DoorState.Open or DoorState.Broken;
            doorObject.localPosition = isOpen
                ? Vector3.down * 1.5f
                : Vector3.zero;
        }
    }
}