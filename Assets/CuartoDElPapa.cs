using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CuartoDElPapa : MonoBehaviour
{
    public TextMeshProUGUI dialogoText;  // Texto para el diálogo
    public TextMeshProUGUI timerText;    // Texto para mostrar el temporizador
    public int BotellasPapa = 0;         // Contador de botellas del Papa
    private bool isTimerRunning = false;  // Para controlar si el temporizador está en ejecución        

    private float timer = 30f;  // Tiempo inicial del temporizador (45 segundos)

    void Start()
    {
        // Iniciar el temporizador al comenzar el juego
        StartCoroutine(Timer());
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Obtener el componente BotellaScript del jugador
            BotellaScript botellaScript = collision.gameObject.GetComponentInChildren<BotellaScript>();

            if (botellaScript != null && botellaScript.isPickedUp)
            {
                // Si la botella está en la mano, eliminarla
                botellaScript.Drop();  // Llamamos al método Drop para soltar la botella

                // Incrementar el contador de botellas
                BotellasPapa++;

                // Mostrar el mensaje en el dialogo
                StartCoroutine(ShowAndHideText("Botellas del Papa: " + BotellasPapa));
            }
            else
            {
                // Si no tiene la botella, mostrar el mensaje original
                StartCoroutine(ShowAndHideText("No puedo entrar aquí, no sé cómo podría reaccionar"));
            }

            Debug.Log("Espera");
        }
    }

    void OnTriggerExit(Collider collision)
    {
        // Detener la coroutine del temporizador si el jugador sale del área
        StopCoroutine("Timer");
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

    // Coroutine que maneja el temporizador
    private IEnumerator Timer()
    {
        isTimerRunning = true;  // Indicamos que el temporizador está corriendo

        while (isTimerRunning)  // Solo seguimos ejecutando el temporizador si está habilitado
        {
            // Actualizar el texto del temporizador cada segundo
            timerText.text = "" + Mathf.Ceil(timer); // Mostrar el tiempo restante en segundos

            // Esperar un segundo
            yield return new WaitForSeconds(1f);

            // Reducir el tiempo del temporizador
            timer -= 1f;

            if (timer <= 0)  // Si el tiempo se agota
            {
                // Si se acaba el tiempo, reducir BotellasPapa y reiniciar el temporizador
                BotellasPapa--;

                // Verificar si BotellasPapa llega a 0
                if (BotellasPapa <= 0)
                {
                    // Ejecutar el evento cuando BotellasPapa llegue a cero
                    ExecuteEvent();
                    yield break;  // Terminar la coroutine del temporizador
                }

                // Reiniciar el temporizador a 45 segundos para el siguiente ciclo
                timer = 30f;
            }
        }
    }

    // Evento a ejecutar cuando BotellasPapa llega a cero
    private void ExecuteEvent()
    {
        // Cargar la escena de Game Over
        SceneManager.LoadScene("Game over");
    }
}
    