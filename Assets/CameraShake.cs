using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public void Shake() 
    {
        StartCoroutine(IShake());
    }
    public IEnumerator IShake(float duration = .5f, float magnitude = 0.1f)
    {
        Vector3 originalPosition = transform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
