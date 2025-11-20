using UnityEngine;

namespace ButtonsExample
{
    /// <summary>
    /// A button (inherits from Button Base) that triggers
    /// when an object is on top of it.
    /// </summary>
    public class PushButton : ButtonBase
    {
        [Header("Activation Criteria")]
        [SerializeField] private int targetObjects = 1;

        /// <inheritdoc />
        protected override bool IsPressed()
        {
            return touchingObjects.Count >= targetObjects;
        }
    }
}