using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    // Variables publicas
    public int numHours, secPerHour;
    public Text timerText;

    public UnityEvent GameOver, Win, Lose;

    private int time;

    public static LevelManager Instance { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        // Inicializacion de variables
        time = 0;
        timerText.text = time + ":00";
        InvokeRepeating("UpdateTime", secPerHour, secPerHour);
    }
    // Funcion para cambiar la hora; 
    void UpdateTime()
    {
        time++;
        timerText.text = time + ":00";
        if (time == 6)
        {
            GameOver.Invoke();
            Win.Invoke();
        }
    }

}