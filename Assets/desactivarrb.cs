using UnityEngine;

public class desactivarrb : MonoBehaviour
{
    private Rigidbody rb;
    public float groundCheckDistance = 0.1f; // Distancia a la que comprobamos si tocamos el suelo

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtiene el Rigidbody del objeto
    }

    void FixedUpdate()      
    {
        // Llamamos a un raycast hacia abajo para ver si tocamos el suelo
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance))
        {
            // Si tocamos el suelo, nos aseguramos de que el objeto no caiga más allá
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Detenemos la caída
            }
        }
    }
}
