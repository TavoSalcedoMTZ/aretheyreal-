using UnityEngine;

public class PreventFallingThroughFloor : MonoBehaviour
{
    private Rigidbody rb;
    public float groundCheckDistance = 0.5f; // Distancia a la que comprobamos si tocamos el suelo
    public LayerMask groundLayer; // Capa en la que está el suelo (puedes asignar desde el Inspector)

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtiene el Rigidbody del objeto
    }

    void FixedUpdate()
    {
        // Comprobar si estamos tocando el suelo utilizando un Raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            // Si el objeto tocado por el raycast está en la capa "Piso"
            if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
            {
                // Si tocamos el suelo, aseguramos que el objeto no caiga más allá
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Detenemos la caída
                }
            }
        }
    }
}
