using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Variables públicas
    public int numHours = 6; // Número total de horas de duración
    public int secPerHour = 60; // Número de segundos por hora en el juego (puedes ajustarlo)
    public TextMeshProUGUI timerText; // UI para mostrar el temporizador
    public UnityEvent Win; // Evento cuando se gana
    public UnityEvent GameOver; // Evento cuando se pierde

    public GameObject menuperdida;
    public GameObject HUDplayer;
    public GameObject canvas;
    public GameObject MODELOMAMA;
    public GameObject Screemer;
    private int totalTimeInSeconds; // Tiempo total en segundos (horas convertidas a segundos)
    private int timeRemaining; // Tiempo restante en segundos
    public Momm mama;
    public static LevelManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        menuperdida.SetActive(false);
        Screemer.SetActive(false);
        MODELOMAMA.SetActive(true);
        HUDplayer.SetActive(true);
        canvas.SetActive(true);

        // Inicialización de variables
        totalTimeInSeconds = numHours * secPerHour; // Calcula el tiempo total en segundos
        timeRemaining = totalTimeInSeconds; // El tiempo restante empieza con el tiempo total

        // Mostrar el tiempo inicial
        UpdateTimerText();

        // Actualiza cada segundo
        InvokeRepeating("UpdateTime", 1f, 1f); // Actualización cada segundo (1 segundo de retraso inicial, luego cada 1 segundo)
    }

    // Función para actualizar el temporizador
    void UpdateTime()
    {
        // Decrementa el tiempo restante
        timeRemaining--;

        // Si el tiempo llega a 0, invocar el evento Win y detener el temporizador
        if (timeRemaining <= 0)
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Win.Invoke();
            CancelInvoke("UpdateTime"); // Detiene la repetición
        }

        // Actualiza el texto del temporizador
        UpdateTimerText();


        
    }

    // Función para actualizar el texto del temporizador en la UI
    void UpdateTimerText()
    {
        int hours = timeRemaining / secPerHour; // Calcula las horas restantes
        int minutes = (timeRemaining % secPerHour); // Calcula los minutos restantes dentro de la hora

        // Muestra el tiempo en formato HH:MM (Ejemplo: 06:00)
        timerText.text = string.Format("{0:D2}:{1:D2}", hours, minutes);
    }

    private void Update()
    {
        if (mama.gameOvER)
        {
      
            Screemer.SetActive(true);
            MODELOMAMA.SetActive(false);
            HUDplayer.SetActive(false);
            canvas.SetActive(false);
            StartCoroutine(GameOverSequence());
      



        }
         
        }
    
    private IEnumerator GameOverSequence()
    {
        Screemer.SetActive(true);
        yield return new WaitForSeconds(2);
        Cursor.lockState = CursorLockMode.None;
        menuperdida.SetActive(true);
        Cursor.visible = true;
        GameOver.Invoke();
   

    }
}
