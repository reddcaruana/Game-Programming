using System;

namespace ButtonsExample
{
    // This only represents one game object
    [Serializable]
    public struct SavedTransformData
    {
        public int entityId;
        public TransformData data;
    }
}