using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Importar SceneManagement

public class Momm : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animador;
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool atacado;

    public NavMeshAgent agent;
    public float distancia_ataque;
    public float randio_vision;
    public bool gameOvER = false;

    public float distanciaGameOver = 1.0f; // Distancia mínima para activar Game Over

    void Start()
    {
        gameOvER=false;
        animador = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player"); // Asegúrate de que el Player tiene la etiqueta correcta
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > randio_vision)
        {
            agent.enabled = false;
            animador.SetBool("run", false);
            cronometro += Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animador.SetBool("walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    animador.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            agent.enabled = true;
            agent.SetDestination(target.transform.position);

            if (Vector3.Distance(transform.position, target.transform.position) > distancia_ataque && !atacado)
            {
                animador.SetBool("walk", false);
                animador.SetBool("run", true);
            }
            else
            {
                if (!atacado)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                    animador.SetBool("walk", false);
                    animador.SetBool("run", false);
                }
            }
        }

        if (atacado)
        {
            agent.enabled = false;
        }
    }

    void Update()
    {
        Comportamiento_Enemigo();

       
        if (Vector3.Distance(transform.position, target.transform.position) <= distanciaGameOver)
        {
            gameOvER = true;

        }
    }
}
