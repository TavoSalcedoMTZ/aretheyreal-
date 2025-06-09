using System.Collections;
using UnityEngine;
using TMPro;

public class TextOpacity : MonoBehaviour
{
    private TextMeshPro textMeshPro; 
    public float fadeDuration = 1f;    

    private void Start()
    {
  
        if (textMeshPro == null)
            textMeshPro = GetComponent<TextMeshPro>();
    }


    public void FadeOut()
    {
        StartCoroutine(FadeTextAlpha(1f, 0f));
    }


    public void FadeIn()
    {
        StartCoroutine(FadeTextAlpha(0f, 1f));
    }

    private IEnumerator FadeTextAlpha(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = textMeshPro.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            textMeshPro.color = new Color(color.r, color.g, color.b, newAlpha);
            yield return null;
        }

    
        textMeshPro.color = new Color(color.r, color.g, color.b, endAlpha);
    }
}
