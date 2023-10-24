using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Drawing;
using System;

namespace UnityLibrary
{
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

    namespace FadeTools
    {
        /// <summary>
        /// A utility class for handling screen fading and RawImage creation.
        /// </summary>
        public static class Fade
        {
            /// <summary>
            /// Gradually fades in a RawImage over a specified duration.
            /// </summary>
            /// <param name="rawImage">The RawImage to fade in.</param>
            /// <param name="duration">The duration of the fade-in effect.</param>
            /// <returns>An IEnumerator for coroutine usage.</returns>
            public static IEnumerator FadeIn(RawImage rawImage, float duration)
            {
                float elapsedTime = 0f;

                // Takes the initial color of the raw image
                UnityEngine.Color initialColor = rawImage.color;

                // Takes the color that the raw image must assume
                UnityEngine.Color targetColor = new UnityEngine.Color(initialColor.r, initialColor.g, initialColor.b, 1f);

                // While elapsedTime < duration, execute a Mathf.Lerp function to change over time the alpha of the screen till elapsedTime reaches duration
                while (elapsedTime < duration)
                {
                    float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                    rawImage.color = new UnityEngine.Color(targetColor.r, targetColor.g, targetColor.b, alpha);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Make sure the final alpha color assumed by the image is 1f
                rawImage.color = new UnityEngine.Color(targetColor.r, targetColor.g, targetColor.b, 1f);
            }

            /// <summary>
            /// Gradually fades out a RawImage over a specified duration.
            /// </summary>
            /// <param name="rawImage">The RawImage to fade out.</param>
            /// <param name="duration">The duration of the fade-out effect.</param>
            /// <returns>An IEnumerator for coroutine usage.</returns>
            public static IEnumerator FadeOut(RawImage rawImage, float duration)
            {
                float elapsedTime = 0f;

                // Takes the initial color of the raw image
                UnityEngine.Color initialColor = rawImage.color;

                // Takes the color that the raw image must assume
                UnityEngine.Color targetColor = new UnityEngine.Color(initialColor.r, initialColor.g, initialColor.b, 0f); // Sets alpha to 0 to make it completely transparent

                // While elapsedTime < duration, execute a Mathf.Lerp function to change over time the alpha of the screen till elapsedTime reaches duration
                while (elapsedTime < duration)
                {
                    float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                    rawImage.color = new UnityEngine.Color(targetColor.r, targetColor.g, targetColor.b, alpha);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                // Make sure the final alpha color assumed by the image is 0f
                rawImage.color = new UnityEngine.Color(targetColor.r, targetColor.g, targetColor.b, 0f);
            }
        }
    }
}
