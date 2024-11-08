using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;  // Referencia al jugador
    public float timeToNextDestination = 3f;  // Tiempo entre cada movimiento aleatorio
    public float range = 10f;  // Rango de movimiento aleatorio
    public float detectionRange = 15f;  // Rango de detecci�n para ver al jugador
    public float chaseSpeed = 5f;  // Velocidad del enemigo cuando detecta al jugador
    public float normalSpeed = 3f;  // Velocidad normal cuando no est� persiguiendo al jugador
    public float escapeRange = 5f;  // Rango dentro del cual el enemigo huye del jugador
    private bool isChasingPlayer = false;  // Flag para saber si el enemigo est� persiguiendo al jugador
    private bool isEscaping = false;  // Flag para saber si el enemigo est� huyendo del jugador

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // Obtenemos el componente NavMeshAgent
        agent.speed = normalSpeed;  // Establecemos la velocidad normal
        agent.stoppingDistance = 1f;  // Establecemos una distancia de parada cuando lleguemos al destino
        agent.angularSpeed = 120f;  // Ajustamos la velocidad de rotaci�n para que gire m�s r�pido
        agent.acceleration = 8f;  // Aceleraci�n del agente para que reaccione m�s r�pido
        InvokeRepeating("MoveToRandomLocation", 0, timeToNextDestination);  // Llamamos a MoveToRandomLocation peri�dicamente
    }

    void Update()
    {
        DetectPlayer();  // Verificamos si el jugador est� dentro del rango de detecci�n
    }

    void DetectPlayer()
    {
        if (player == null) return;  // Si no hay un jugador asignado, no hacemos nada

        // Calculamos la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verificamos si el jugador est� dentro del rango de detecci�n
        if (distanceToPlayer <= detectionRange)
        {
            // Usamos un raycast para asegurarnos de que no haya obst�culos entre el enemigo y el jugador
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))  // Verificamos si el raycast impact� con el jugador
                {
                    // Si el jugador est� dentro del rango de escape, el enemigo huye
                    if (distanceToPlayer <= escapeRange)
                    {
                        EscapeFromPlayer();
                    }
                    else
                    {
                        // Si el jugador est� dentro del rango de detecci�n, el enemigo lo persigue
                        ChasePlayer();
                    }
                }
            }
        }
        else
        {
            // Si el jugador est� fuera del rango de detecci�n, el enemigo vuelve a moverse aleatoriamente
            if (isChasingPlayer || isEscaping)
            {
                // Solo vuelve a moverse aleatoriamente si estaba persiguiendo o huyendo
                isChasingPlayer = false;
                isEscaping = false;
                agent.speed = normalSpeed;  // Restablecemos la velocidad normal
                InvokeRepeating("MoveToRandomLocation", 0, timeToNextDestination);  // Reanudamos el movimiento aleatorio
            }
        }
    }

    // Funci�n para hacer que el enemigo huya del jugador
    void EscapeFromPlayer()
    {
        if (isEscaping) return;  // Si el enemigo ya est� huyendo, no hacer nada

        isEscaping = true;
        isChasingPlayer = false;  // El enemigo ya no persigue al jugador

        agent.speed = chaseSpeed;  // Hacemos que el enemigo huya m�s r�pido
        Vector3 directionAwayFromPlayer = transform.position - player.position;  // Direcci�n opuesta al jugador
        Vector3 newDestination = transform.position + directionAwayFromPlayer;  // Nueva posici�n en direcci�n opuesta

        agent.SetDestination(newDestination);  // Establecemos la nueva posici�n de huida
    }

    // Funci�n para hacer que el enemigo persiga al jugador
    void ChasePlayer()
    {
        if (isChasingPlayer) return;  // Si el enemigo ya est� persiguiendo, no hacer nada

        isChasingPlayer = true;
        isEscaping = false;  // El enemigo ya no est� huyendo

        agent.speed = chaseSpeed;  // Aumentamos la velocidad para perseguir al jugador
        agent.SetDestination(player.position);  // Movemos al enemigo hacia el jugador
        CancelInvoke("MoveToRandomLocation");  // Detenemos el movimiento aleatorio mientras se persigue al jugador
    }

    void MoveToRandomLocation()
    {
        if (isChasingPlayer || isEscaping) return;  // Si el enemigo est� persiguiendo o huyendo, no se mueve aleatoriamente

        // Generamos una posici�n aleatoria dentro de un rango
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;  // Desplazamos la direcci�n aleatoria desde la posici�n actual

        // Aseguramos que el punto aleatorio est� sobre la superficie del NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            // Movemos al agente a la nueva posici�n calculada
            agent.SetDestination(hit.position);
        }
    }
}
