using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;  // Referencia al jugador
    public float timeToNextDestination = 3f;  // Tiempo entre cada movimiento aleatorio
    public float range = 10f;  // Rango de movimiento aleatorio
    public float detectionRange = 15f;  // Rango de detección para ver al jugador
    public float chaseSpeed = 5f;  // Velocidad del enemigo cuando detecta al jugador
    public float normalSpeed = 3f;  // Velocidad normal cuando no está persiguiendo al jugador
    public float escapeRange = 5f;  // Rango dentro del cual el enemigo huye del jugador
    private bool isChasingPlayer = false;  // Flag para saber si el enemigo está persiguiendo al jugador
    private bool isEscaping = false;  // Flag para saber si el enemigo está huyendo del jugador

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // Obtenemos el componente NavMeshAgent
        agent.speed = normalSpeed;  // Establecemos la velocidad normal
        agent.stoppingDistance = 1f;  // Establecemos una distancia de parada cuando lleguemos al destino
        agent.angularSpeed = 120f;  // Ajustamos la velocidad de rotación para que gire más rápido
        agent.acceleration = 8f;  // Aceleración del agente para que reaccione más rápido
        InvokeRepeating("MoveToRandomLocation", 0, timeToNextDestination);  // Llamamos a MoveToRandomLocation periódicamente
    }

    void Update()
    {
        DetectPlayer();  // Verificamos si el jugador está dentro del rango de detección
    }

    void DetectPlayer()
    {
        if (player == null) return;  // Si no hay un jugador asignado, no hacemos nada

        // Calculamos la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verificamos si el jugador está dentro del rango de detección
        if (distanceToPlayer <= detectionRange)
        {
            // Usamos un raycast para asegurarnos de que no haya obstáculos entre el enemigo y el jugador
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))  // Verificamos si el raycast impactó con el jugador
                {
                    // Si el jugador está dentro del rango de escape, el enemigo huye
                    if (distanceToPlayer <= escapeRange)
                    {
                        EscapeFromPlayer();
                    }
                    else
                    {
                        // Si el jugador está dentro del rango de detección, el enemigo lo persigue
                        ChasePlayer();
                    }
                }
            }
        }
        else
        {
            // Si el jugador está fuera del rango de detección, el enemigo vuelve a moverse aleatoriamente
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

    // Función para hacer que el enemigo huya del jugador
    void EscapeFromPlayer()
    {
        if (isEscaping) return;  // Si el enemigo ya está huyendo, no hacer nada

        isEscaping = true;
        isChasingPlayer = false;  // El enemigo ya no persigue al jugador

        agent.speed = chaseSpeed;  // Hacemos que el enemigo huya más rápido
        Vector3 directionAwayFromPlayer = transform.position - player.position;  // Dirección opuesta al jugador
        Vector3 newDestination = transform.position + directionAwayFromPlayer;  // Nueva posición en dirección opuesta

        agent.SetDestination(newDestination);  // Establecemos la nueva posición de huida
    }

    // Función para hacer que el enemigo persiga al jugador
    void ChasePlayer()
    {
        if (isChasingPlayer) return;  // Si el enemigo ya está persiguiendo, no hacer nada

        isChasingPlayer = true;
        isEscaping = false;  // El enemigo ya no está huyendo

        agent.speed = chaseSpeed;  // Aumentamos la velocidad para perseguir al jugador
        agent.SetDestination(player.position);  // Movemos al enemigo hacia el jugador
        CancelInvoke("MoveToRandomLocation");  // Detenemos el movimiento aleatorio mientras se persigue al jugador
    }

    void MoveToRandomLocation()
    {
        if (isChasingPlayer || isEscaping) return;  // Si el enemigo está persiguiendo o huyendo, no se mueve aleatoriamente

        // Generamos una posición aleatoria dentro de un rango
        Vector3 randomDirection = Random.insideUnitSphere * range;
        randomDirection += transform.position;  // Desplazamos la dirección aleatoria desde la posición actual

        // Aseguramos que el punto aleatorio esté sobre la superficie del NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, range, NavMesh.AllAreas))
        {
            // Movemos al agente a la nueva posición calculada
            agent.SetDestination(hit.position);
        }
    }
}
