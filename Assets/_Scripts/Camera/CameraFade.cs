using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class CameraFade : MonoBehaviour
{
    [SerializeField]
    private float _fadeDuration = 0.5f; // Time taken for the fade

    private Image _fadeImage;     // The image used for the fade (assigned from the canvas)

    private void Awake()
    {
        _fadeImage = GetComponentInChildren<Image>();

        if (_fadeImage == null)
        {
            throw new Exception("No Fade Image");
        }
    }

    void Start()
    {
        // Ensure the image starts fully transparent
        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, 0f);
    }

    // Fade In (fade from black to clear)
    public IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer <= _fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / _fadeDuration);
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, alpha);
            yield return null;
        }
    }

    // Fade Out (fade from clear to black)
    public IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer <= _fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / _fadeDuration);
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, alpha);
            yield return null;
        }
    }
}
