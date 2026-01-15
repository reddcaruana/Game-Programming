using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ButtonsExample
{
    public class SaveManager : MonoBehaviour
    {
        private const string fileName = "saveGame.json";
        private string savePath => Path.Combine(Application.persistentDataPath, fileName);

        [ContextMenu("Save")]
        public void Save()
        {
            // Look for ALL objects that have SerializedTransform attached
            var st = FindObjectsByType<SerializedTransform>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
            
            // Start a save file
            var file = new SaveFile
            {
                transforms = new List<SavedTransformData>(st.Length)
            };
            
            // Loop and Capture
            // st.for and press enter
            for (var i = 0; i < st.Length; i++)
            {
                var t = st[i];
                var entityId = t.GetEntityId().GetHashCode(); // Gives an integer ID
                
                file.transforms.Add(new SavedTransformData
                {
                    entityId = entityId,
                    data = t.Capture()
                });
            }

            // Convert to JSON data and write
            var json = JsonUtility.ToJson(file, prettyPrint: true);
            File.WriteAllText(savePath, json);
            
            Debug.Log($"Saved {file.transforms.Count} objects to {savePath}");
        }

        [ContextMenu("Load")]
        public void Load()
        {
            // First, check if the file exists
            if (!File.Exists(savePath))
            {
                Debug.Log($"No save file found at {savePath}");
                return;
            }
            
            // Load the data
            var json = File.ReadAllText(savePath);
            var file = JsonUtility.FromJson<SaveFile>(json);
            
            // Look for ALL objects that have SerializedTransform attached
            var st = FindObjectsByType<SerializedTransform>(FindObjectsInactive.Exclude, FindObjectsSortMode.InstanceID);
            var map = new Dictionary<int, SerializedTransform>(st.Length);
            
            // Register a map of things we want to load
            for (var i = 0; i < st.Length; i++)
            {
                var t = st[i];
                var entityId = t.GetEntityId().GetHashCode();
                map[entityId] = t;
            }
            
            // Keep track of how many items were restored
            var restored = 0;
            
            // Read all the file objects, and try to restore them
            for (var i = 0; i < file.transforms.Count; i++)
            {
                var entry = file.transforms[i];
                
                // Restore only if the object exists
                if (map.TryGetValue(entry.entityId, out var target))
                {
                    target.Restore(entry.data);
                    restored++;
                }
            }
            
            Debug.Log($"Loaded. Restored {restored}/{file.transforms.Count} objects from {savePath}");
        }
    }
}