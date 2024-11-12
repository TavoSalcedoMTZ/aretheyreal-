using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemigoAI : MonoBehaviour
{
    public NavMeshAgent agente; // El NavMeshAgent del enemigo
    public Transform jugador;   // El jugador
    public float velocidadNormal = 3.5f; // Velocidad normal
    public float velocidadPersecucion = 7f; // Velocidad cuando persigue
    public float rangoVisibilidad = 10f; // Rango de visión del enemigo
    public float rangoPersecucion = 5f; // Rango de persecución
    public float rangoAtaque = 1f; // Rango a donde el enemigo puede atacar

    private bool persiguiendoJugador = false;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        agente.speed = velocidadNormal;
    }

    void Update()
    {
        if (!persiguiendoJugador)
        {
            MoverAleatoriamente();
            DetectarJugador();
        }
        else
        {
            PerseguirJugador();
        }
    }

    // Método para mover aleatoriamente por el mapa
    void MoverAleatoriamente()
    {
        if (!agente.hasPath)
        {
            Vector3 puntoAleatorio = new Vector3(
                Random.Range(transform.position.x - 10f, transform.position.x + 10f), // X random
                transform.position.y,    // Mantener la misma altura
                Random.Range(transform.position.z - 10f, transform.position.z + 10f)  // Z random
            );

            agente.SetDestination(puntoAleatorio);
        }
    }

    // Detecta si el enemigo ve al jugador (sin obstáculos)
    void DetectarJugador()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, jugador.position, out hit, rangoVisibilidad);
        Debug.DrawRay(transform.position, jugador.position, Color.red, 1f);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.CompareTag("Jugador"))
            {
                // El jugador está visible
                Debug.Log("Jugador detectado");
                persiguiendoJugador = true;
                agente.speed = velocidadPersecucion;
            }
        }
    }

    // Método para perseguir al jugador
    void PerseguirJugador()
    {
        // Si el jugador está fuera del rango de persecución, volver a moverse aleatoriamente
        if (Vector3.Distance(transform.position, jugador.position) > rangoPersecucion)
        {
            persiguiendoJugador = false;
            agente.speed = velocidadNormal;
        }
        else
        {
            agente.destination = jugador.position; // El enemigo se mueve hacia el jugador

            // Si el enemigo está cerca del jugador, lo "ataca"
            if (Vector3.Distance(transform.position, jugador.position) <= rangoAtaque)
            {
                AtacarJugador();
            }
        }
    }

    // Método para manejar el ataque (Game Over)
    void AtacarJugador()
    {
        // Llamar a la escena de Game Over cuando el enemigo "atrapa" al jugador
        Debug.Log("¡Has sido atrapado!");
        SceneManager.LoadScene("GameOver");
    }
}
