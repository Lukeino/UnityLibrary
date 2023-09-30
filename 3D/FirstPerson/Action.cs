using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ActionTools
{
    /// <summary>
    /// A static class for managing interactable objects in the scene.
    /// </summary>
    public static class Action
    {
        /// <summary>
        /// Saves a list of tags to a binary file.
        /// </summary>
        /// <param name="tags">The list of tags to be saved.</param>
        public static void SaveTags(List<string> tags)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string filePath = Application.persistentDataPath + "/tags.dat";
            FileStream fileStream = new FileStream(filePath, FileMode.Create);

            formatter.Serialize(fileStream, tags);
            fileStream.Close();
        }

        /// <summary>
        /// Checks if an object is interactable based on its tag and raycasting from the camera.
        /// </summary>
        /// <param name="tagIndex">Index of the tag in the provided list.</param>
        /// <param name="maxRaycastDistance">Maximum distance for raycasting.</param>
        /// <returns>True if an interactable object is found, false otherwise.</returns>
        public static bool IsObjectInteractable(out int tagIndex, float maxRaycastDistance)
        {
            tagIndex = -1; // Initialize tag index to -1 (not found)
            List<string> interactableTags = LoadTags();

            // Cast a ray from the camera's position in the direction it's facing
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRaycastDistance))
            {
                // Check if the tag of the hit object is in the provided list
                string hitTag = hit.collider.gameObject.tag;
                tagIndex = interactableTags.IndexOf(hitTag);

                if (tagIndex != -1)
                {
                    // The object is interactable, and its tag is in the provided list
                    return true;
                }
            }

            // No interactable object found or the tag is not in the list
            return false;
        }

        /// <summary>
        /// Loads a list of tags from a binary file.
        /// </summary>
        /// <returns>The list of tags loaded from the file.</returns>
        private static List<string> LoadTags()
        {
            string filePath = Application.persistentDataPath + "/tags.dat";
            if (File.Exists(filePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                List<string> tags = (List<string>)formatter.Deserialize(fileStream);
                fileStream.Close();
                return tags;
            }
            else
            {
                Debug.LogError("Save file not found.");
                return new List<string>();
            }
        }
    }
}
