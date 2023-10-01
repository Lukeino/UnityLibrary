using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            Color initialColor = rawImage.color;

            // Takes the color that the raw image must assume
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

            while (elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                rawImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Make sure the final alpha color assumed by the image is 1f
            rawImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, 1f);
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
            Color initialColor = rawImage.color;

            // Takes the color that the raw image must assume
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f); // Sets alpha to 0 to make it completely transparent

            while (elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                rawImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Make sure the final alpha color assumed by the image is 0f
            rawImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0f);
        }
    }
}
