using System;

namespace ButtonsExample
{
    // Serializable makes it possible to save data
    [Serializable]
    public struct TransformData
    {
        public float px, py, pz; // Position
        public float rx, ry, rz, rw; // Rotation
    }
}