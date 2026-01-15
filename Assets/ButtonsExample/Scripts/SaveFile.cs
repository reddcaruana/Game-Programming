using System;
using System.Collections.Generic;

namespace ButtonsExample
{
    [Serializable]
    public struct SaveFile
    {
        public List<SavedTransformData> transforms;
    }
}