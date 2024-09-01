using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance;
    private Coroutine coroutine;
    private Vector3 originalPosition;

    private void Awake()
    {
        // Singleton pattern to allow easy access from other scripts
        if (Instance == null)
        {
            Instance = this;
            originalPosition = transform.localPosition;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            // Generate a random offset for the camera within the specified magnitude
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            // Apply the offset
            transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsed += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Reset the camera to its original position
        transform.localPosition = originalPosition;
    }

    public void TriggerShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
}