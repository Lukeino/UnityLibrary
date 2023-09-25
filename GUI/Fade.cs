using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FadeTools
{
    public static class Fade
    {
        // function that creates a black RawImage that covers the entire screen and gradually makes it black
        public static int CreateRawImage()
        {
            // create a new rawimage totally black
            GameObject rawImageObject = new GameObject("FadeScreen");
            RawImage rawImage = rawImageObject.AddComponent<RawImage>();

            // set the color of the raw image
            rawImage.color = Color.black;

            // cover the entire camera
            Camera mainCamera = Camera.main;
            float cameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
            float cameraHeight = mainCamera.orthographicSize * 2;

            rawImage.rectTransform.sizeDelta = new Vector2(cameraWidth, cameraHeight);
            rawImageObject.transform.position = new Vector3(0, 0, 1);
            rawImageObject.transform.SetParent(mainCamera.transform);

            return 0;
        }

        // function that makes the screen fade in
        public static IEnumerator FadeIn(RawImage rawImage, float duration)
        {
            float elapsedTime = 0f;

            // takes the initial color of the rawimage
            Color initialColor = rawImage.color;

            // takes the color that the rawimage must assume
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

            while(elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
                rawImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // make sure the final alpha color assumed by the image is 1f
            rawImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, 1f);
        }

        // function that makes the screen fade out
        public static IEnumerator FadeOut(RawImage rawImage, float duration)
        {
            float elapsedTime = 0f;

            // takes the initial color of the rawimage
            Color initialColor = rawImage.color;

            // takes the color that the rawimage must assume
            Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f); // Imposta l'alpha a 0 per renderlo completamente trasparente

            while (elapsedTime < duration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
                rawImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // make sure the final alpha color assumed by the image is 0f
            rawImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0f);
        }
    }
}
