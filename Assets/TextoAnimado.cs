    using System.Collections;
using UnityEngine;
using TMPro;

public class TextoAnimado : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public float velocidadEscritura = 0.05f;
    public float velocidadBorrado = 0.02f;

    public IEnumerator EscribirTexto(string contenido)
    {
        texto.text = "";
        foreach (char letra in contenido)
        {
            texto.text += letra;
            yield return new WaitForSeconds(velocidadEscritura);
        }
    }

    public IEnumerator BorrarTexto()
    {
        while (texto.text.Length > 0)
        {
            texto.text = texto.text.Substring(0, texto.text.Length - 1);
            yield return new WaitForSeconds(velocidadBorrado);
        }
    }

    public void Limpiar() => texto.text = "";
}
