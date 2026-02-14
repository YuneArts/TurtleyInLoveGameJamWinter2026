using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Flicker : MonoBehaviour
{
    // Reference to the button's Image component (or MaskableGraphic for Text/other elements)
    public MaskableGraphic graphicToFlicker;
    public float minInterval = 0.1f;
    public float maxInterval = 0.5f;
    public bool startOn = true;
    private bool isBlinking = false;

    void Start()
    {
        if (graphicToFlicker == null)
        {
            // Try to get the Image component if not assigned manually
            graphicToFlicker = GetComponent<Image>();
            if (graphicToFlicker == null)
            {
                Debug.LogError("No MaskableGraphic component found on " + gameObject.name);
                return;
            }
        }
        graphicToFlicker.enabled = startOn;
        StartFlickering();
    }

    public void StartFlickering()
    {
        if (isBlinking) return; // Prevent starting the coroutine multiple times
        isBlinking = true;
        StartCoroutine(FlickerCoroutine());
    }

    IEnumerator FlickerCoroutine()
    {
        while (isBlinking)
        {
            // Toggle the graphic's visibility
            graphicToFlicker.enabled = !graphicToFlicker.enabled;

            // Wait for a random duration between intervals
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }

    // Optional: A public method to stop the flickering, e.g., when the button is clicked
    public void StopFlickering()
    {
        isBlinking = false;
        if (graphicToFlicker != null)
        {
            graphicToFlicker.enabled = true; // Ensure the button is visible and clickable
        }
        StopCoroutine(FlickerCoroutine());
    }
}
