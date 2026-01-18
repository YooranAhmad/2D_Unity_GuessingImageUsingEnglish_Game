using UnityEngine;
using System.Collections;

public class FinishAnimator : MonoBehaviour
{
    [Header("Fade Settings")]
    public CanvasGroup canvasGroup;   // drag CanvasGroup di sini
    public float fadeDuration = 1f;
    public float zoomScale = 1.2f;

    private Vector3 originalScale;
    private bool isAnimating = false;

    void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        originalScale = transform.localScale;

        // Biar gak langsung hilang total di awal, kita aktifkan tapi alpha=0
        gameObject.SetActive(false);
        canvasGroup.alpha = 0f;
    }

    public void ShowFinish()
    {
        Debug.Log("✅ ShowFinish() terpanggil dari GameController");

        if (isAnimating) return;
        isAnimating = true;

        gameObject.SetActive(true); // pastikan muncul dulu

        StopAllCoroutines();
        StartCoroutine(FadeAndZoom());
    }

    private IEnumerator FadeAndZoom()
    {
        Debug.Log("🎬 Animasi finish dimulai");

        float timer = 0f;
        canvasGroup.alpha = 0f;
        transform.localScale = originalScale * 0.5f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            // pastikan alpha naik dari 0 → 1
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            transform.localScale = Vector3.Lerp(originalScale * 0.5f, originalScale * zoomScale, t);

            yield return null;
        }

        // Biar efek bounce kecil di akhir
        transform.localScale = originalScale * 1.1f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = originalScale;

        canvasGroup.alpha = 1f;
        isAnimating = false;
        Debug.Log("🎉 Animasi selesai, FinishImage tampil penuh!");
    }
}
