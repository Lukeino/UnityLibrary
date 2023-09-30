using System.Collections.Generic;
using UnityEngine;

namespace ActionTools
{
    /// <summary>
    /// A class for managing interactable objects in the scene.
    /// </summary>
    public static class Action
    {
        /// <summary>
        /// Checks if the object pointed at is interactable.
        /// </summary>
        /// <param name="tagIndex">The index of the tag in the collection if found, otherwise -1.</param>
        /// <param name="maxRaycastDistance">The maximum distance for the raycast.</param>
        /// <returns>True if the object is interactable, false otherwise.</returns>
        public bool IsObjectInteractable(out int tagIndex, float maxRaycastDistance)
        {
            tagIndex = -1; // Initialize tag index to -1 (not found)

            // Cast a ray from the camera's position in the direction it's facing
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRaycastDistance))
            {
                // Check if the tag of the hit object is in the collection
                string hitTag = hit.collider.gameObject.tag;
                tagIndex = interactableTags.IndexOf(hitTag);

                if (tagIndex != -1)
                {
                    // The object is interactable, and its tag is in the collection
                    return true;
                }
            }

            // No interactable object found or the tag is not in the collection
            return false;
        }
    }
}
