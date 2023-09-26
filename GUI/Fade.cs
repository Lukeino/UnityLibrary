using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FadeTools
{
    public static class Fade
    {
        // function that creates a black RawImage that covers the entire Canvas and gradually makes it black
        public static void CreateRawImage()
        {
            // Find the Canvas in the scene
            Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        
            if (canvas == null)
            {
                Debug.LogError("No Canvas found in the scene. Make sure you have a Canvas GameObject in your scene.");
                return;
            }
        
            // Create a new GameObject for the RawImage as a child of the Canvas
            GameObject rawImageObject = new GameObject("FadeScreen");
            RawImage rawImage = rawImageObject.AddComponent<RawImage>();
            rawImage.transform.SetParent(canvas.transform, false);
        
            // Set the color of the RawImage to black
            rawImage.color = Color.black;
        
            // Set the size of the RawImage to match the size of the Canvas
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            rawImage.rectTransform.sizeDelta = canvasRect.sizeDelta;
        
            // Position the RawImage at the same position as the Canvas
            rawImage.rectTransform.position = canvasRect.position;
        
            // Ensure that the RawImage is at a suitable depth
            rawImageObject.transform.localPosition = Vector3.forward;
        
            // You may want to adjust the sorting order of the RawImage if needed
            rawImageObject.GetComponent<Canvas>().sortingOrder = 1;
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
