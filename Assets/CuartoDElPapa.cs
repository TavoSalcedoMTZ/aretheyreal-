using System.Collections;
using UnityEngine;
using TMPro;

public class CuartoDElPapa : MonoBehaviour
{
    public TextMeshProUGUI dialogoText;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ShowAndHideText("No puedo entrar aquí, no sé cómo podría reaccionar"));
            Debug.Log("Espera");
        }
    }

    void OnTriggerExit(Collider collision)
    {
        StopAllCoroutines(); // Detener cualquier coroutine activa
        dialogoText.text = ""; // Limpiar el texto al salir
        Debug.Log("Espera");
    }

    private IEnumerator ShowAndHideText(string message)
    {
        // Mostrar el texto letra por letra
        dialogoText.text = "";
        foreach (char letter in message)
        {
            dialogoText.text += letter; // Añadir letra por letra
            yield return new WaitForSeconds(0.05f); // Esperar un poco antes de mostrar la siguiente letra
        }

        // Esperar un momento antes de empezar a borrar
        yield return new WaitForSeconds(1f);

        // Borrar el texto letra por letra
        for (int i = message.Length - 1; i >= 0; i--)
        {
            dialogoText.text = dialogoText.text.Remove(i, 1); // Borrar letra por letra
            yield return new WaitForSeconds(0.05f); // Esperar un poco antes de borrar la siguiente letra
        }

        // Opcional: Reiniciar el proceso
        yield return new WaitForSeconds(1f);
        StartCoroutine(ShowAndHideText(message)); // Iniciar de nuevo
    }
}
