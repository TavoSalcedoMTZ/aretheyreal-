using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momm : MonoBehaviour
{
    public Animator animador;  // El componente Animator
    public GameObject target; // El jugador

    void Start()
    {
        // Obtener el componente Animator y asignar el jugador
        animador = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    void Update()
    {
        // Llamar a la función para actualizar las animaciones
        ActualizarAnimaciones();
    }

    void ActualizarAnimaciones()
    {
        // Obtener la distancia entre el enemigo y el jugador
        float distancia = Vector3.Distance(transform.position, target.transform.position);

        // Si la distancia al jugador es mayor a 5, hacer que camine
        if (distancia > 5)
        {
            animador.SetBool("walk", true);
            animador.SetBool("run", false);
        }
        else // Si la distancia al jugador es menor o igual a 5, correr
        {
            animador.SetBool("walk", false);
            animador.SetBool("run", true);
        }
    }
}