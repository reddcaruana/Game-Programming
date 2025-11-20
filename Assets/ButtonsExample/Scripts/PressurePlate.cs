using UnityEngine;

namespace ButtonsExample
{
    /// <summary>
    /// A button (inherits from Button Base) that triggers
    /// when it meets a weight requirement.
    /// </summary>
    public class PressurePlate : ButtonBase
    {
        [Header("Activation Criteria")]
        [SerializeField] private float targetWeight = 10f;

        /// <inheritdoc />
        protected override bool IsPressed()
        {
            var totalWeight = 0f;
            foreach (var touchingObject in touchingObjects)
            {
                totalWeight += touchingObject.Weight;
            }
            
            return totalWeight >= targetWeight;
        }
    }
}