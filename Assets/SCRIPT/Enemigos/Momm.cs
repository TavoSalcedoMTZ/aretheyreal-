using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Momm : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animador;

    public GameObject target;
    public bool atacado;

    public NavMeshAgent agent;
    public float distancia_ataque;
    public float randio_vision;
    public bool gameOvER = false;

    public float distanciaGameOver = 1.0f;

    private Vector3 destinoAleatorio;

    void Start()
    {
        gameOvER = false;
        animador = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    public void Comportamiento_Enemigo()
    {
        float distanciaAlJugador = Vector3.Distance(transform.position, target.transform.position);

        if (distanciaAlJugador > randio_vision)
        {
            animador.SetBool("run", false);
            cronometro += Time.deltaTime;

            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;

                if (rutina == 1)
                {
                    Vector3 puntoAleatorio;
                    if (RandomPoint(transform.position, 10f, out puntoAleatorio))
                    {
                        destinoAleatorio = puntoAleatorio;
                        agent.SetDestination(destinoAleatorio);
                        animador.SetBool("walk", true);
                        agent.isStopped = false;
                    }
                }
            }

            if (rutina == 0)
            {
                animador.SetBool("walk", false);
                agent.isStopped = true;
            }

            // Si llegamos al destino, detener la caminata
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                animador.SetBool("walk", false);
                rutina = 0;
            }
        }
        else
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            agent.isStopped = false;
            agent.SetDestination(target.transform.position);

            if (distanciaAlJugador > distancia_ataque && !atacado)
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
            agent.isStopped = true;
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

    // Método para encontrar un punto aleatorio válido en el NavMesh
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = center;
        return false;
    }
}
