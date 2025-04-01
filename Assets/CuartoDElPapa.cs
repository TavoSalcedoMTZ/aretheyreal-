using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CuartoDElPapa : MonoBehaviour
{
 
    public Image timerFill;              // Imagen con el fillAmount para el temporizador
    public int BotellasPapa = 0;         // Contador de botellas del Papa

    private float timer = 30f;            // Tiempo inicial del temporizador
    private bool isTimerRunning = false;  // Para controlar si el temporizador está en ejecución
    public TextMeshProUGUI BotellasPapaText;  // Texto para mostrar el número de botellas del Papa
    public Momm mama;
    void Start()
    {
        StartCoroutine(Timer());  // Iniciar el temporizador
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BotellaScript botellaScript = collision.gameObject.GetComponentInChildren<BotellaScript>();

            if (botellaScript != null && botellaScript.isPickedUp)
            {
                botellaScript.Drop();
                BotellasPapa++;
            }
            else
            {
            }
        }
    }
    private void Update()
    {
        BotellasPapaText.text = BotellasPapa.ToString();  // Actualizar el texto de las botellas del Papa
    }

    private IEnumerator Timer()
    {
        isTimerRunning = true;
        float totalTime = timer;  // Guardamos el tiempo total

        while (timer > 0)
        {
            // Actualizar la barra de tiempo
            timerFill.fillAmount = timer / totalTime;

            yield return new WaitForSeconds(1f);
            timer -= 1f;
        }

        // Si se acaba el tiempo, reducir BotellasPapa y reiniciar el temporizador
        BotellasPapa--;

        if (BotellasPapa <= 0)
        {
            ExecuteEvent();  // Llamar a Game Over si ya no hay botellas
        }
        else
        {
            timer = 30f;
            StartCoroutine(Timer());  // Reiniciar temporizador
        }
    }

    private void ExecuteEvent()
    {
        mama.gameOvER = true;
    }
}
