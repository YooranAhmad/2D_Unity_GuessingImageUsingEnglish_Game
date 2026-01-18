using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageAnimator : MonoBehaviour
{
    private Image image;
    private Vector3 originalScale;
    private Coroutine currentAnimation;

    void Awake()
    {
        image = GetComponent<Image>();
        originalScale = transform.localScale;
        image.enabled = false;
    }

    public void Show(Sprite icon, float duration = 1f)
    {
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(Animate(icon, duration));
    }

    private IEnumerator Animate(Sprite icon, float duration)
    {
        image.enabled = true;
        image.sprite = icon;
        Color color = image.color;
        color.a = 0;
        image.color = color;
        transform.localScale = originalScale * 0.5f;

        // Fade In + Scale Up
        float t = 0;
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            float p = t / 0.2f;
            color.a = Mathf.Lerp(0, 1, p);
            image.color = color;
            transform.localScale = Vector3.Lerp(originalScale * 0.5f, originalScale * 1.1f, p);
            yield return null;
        }

        // Hold (stay visible)
        yield return new WaitForSeconds(duration - 0.4f);

        // Fade Out
        t = 0;
        while (t < 0.2f)
        {
            t += Time.deltaTime;
            float p = t / 0.2f;
            color.a = Mathf.Lerp(1, 0, p);
            image.color = color;
            yield return null;
        }

        image.enabled = false;
    }
}
