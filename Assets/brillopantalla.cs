using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BrilloPantalla : MonoBehaviour
{
    public Image panel; // Asigna el panel en el inspector
    public float velocidad = 1.0f; // Velocidad del cambio de alpha
    public GameObject botonwin;
    public GameObject textowin;

    public void PantallaDesvanecimiento()
    {
        StartCoroutine(AumentarBrillo());
    }

    IEnumerator AumentarBrillo()
    {
        Color colorPanel = panel.color;
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * velocidad;
            colorPanel.a = Mathf.Clamp01(alpha);
            panel.color = colorPanel;
            yield return null;
        }
        botonwin.SetActive(true);
        textowin.SetActive(true);


    }
}
