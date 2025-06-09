using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFlash : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro; 
    public float flashSpeed = 2f;      

    private Color originalColor;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        originalColor = textMeshPro.color;
    }

    void Update()
    {
        float alpha = Mathf.PingPong(Time.time * flashSpeed, 1f); 
        Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        textMeshPro.color = newColor;
    }
}
