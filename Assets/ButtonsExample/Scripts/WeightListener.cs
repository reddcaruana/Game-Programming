using TMPro;
using UnityEngine;

namespace ButtonsExample
{
    public class WeightListener : MonoBehaviour
    {
        [SerializeField] private PressurePlate pressurePlate;
        [SerializeField] private TMP_Text text;

        private void OnEnable()
        {
            // Subscribe
            pressurePlate.OnWeightChanged += OnWeightChanged;
        }

        private void OnDisable()
        {
            // Unsubscribe
            pressurePlate.OnWeightChanged -= OnWeightChanged;
        }

        private void OnWeightChanged(float currentWeight)
        {
            text.text = $"{currentWeight}kg";
        }
    }
}