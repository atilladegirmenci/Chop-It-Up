using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : CollectableBase
{
    public override void Collected()
    {
        bool canCollect = BackpackSystem.instance.AddItem(collectableTypes.Log, amount);
        if (canCollect)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false; 
            StartCoroutine(AnimateCollect());
        }
    }

    private IEnumerator AnimateCollect()
    {
        Vector3 startPosition = transform.position; 
        Vector3 endPosition = PlayerMovement.instance.transform.position; 
        Vector3 controlPoint = (startPosition + endPosition) / 2 + Vector3.up * 2f; // Parabol için kontrol noktası
        float duration = 0.8f; // Animasyon süresi
        float elapsedTime = 0f;

        Vector3 startScale = transform.localScale; 
        Vector3 endScale = Vector3.zero; 

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; 

            // Parabol interpolasyonu (quadratic Bezier curve)
            Vector3 currentPosition =
                Mathf.Pow(1 - t, 2) * startPosition +
                2 * (1 - t) * t * controlPoint +
                Mathf.Pow(t, 2) * endPosition;

            // Ölçek interpolasyonu
            Vector3 currentScale = Vector3.Lerp(startScale, endScale, t);

            // Pozisyon ve ölçeği uygula
            transform.position = currentPosition;
            transform.localScale = currentScale;

            yield return null; // Bir sonraki frame'e geç
        }
        Sound.instance.CollectSound();
        Destroy(gameObject);

    }

}
