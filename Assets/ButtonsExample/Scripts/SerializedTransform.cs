using UnityEngine;

namespace ButtonsExample
{
    // A transform that can be captured and restored
    public class SerializedTransform : MonoBehaviour
    {
        /// <summary>
        /// Saves the transform info.
        /// </summary>
        public TransformData Capture()
        {
            var t = transform;
            return new TransformData
            {
                px = t.position.x,
                py = t.position.y,
                pz = t.position.z,
                
                rx = t.rotation.x,
                ry = t.rotation.y,
                rz = t.rotation.z,
                rw = t.rotation.w
            };
        }

        /// <summary>
        /// Restores the transform info.
        /// </summary>
        public void Restore(TransformData data)
        {
            transform.SetPositionAndRotation(
                new Vector3(data.px, data.py, data.pz),
                new Quaternion(data.rx, data.ry, data.rz, data.rw)
            );
        }
    }
}