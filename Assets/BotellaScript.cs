using UnityEngine;

public class BotellaScript : MonoBehaviour
{
    public Transform handPosition;  // Posición en la que se coloca la botella en la mano del personaje
    public bool isPickedUp = false;  // Para verificar si la botella ya está en la mano
    private GameObject player;  // Referencia al jugador

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Asumimos que el personaje tiene la etiqueta "Player"
    }

    // Este método se llamará cuando el jugador interactúe con la botella
    public void PickUp()
    {
        if (!isPickedUp)
        {
            isPickedUp = true;  // Marcamos que la botella está siendo sostenida
            transform.SetParent(player.transform);  // Hacemos que la botella sea hija del jugador
            transform.localPosition = handPosition.localPosition;  // Colocamos la botella en la posición de la mano
            transform.localRotation = handPosition.localRotation;  // Alineamos la rotación de la botella
            GetComponent<Collider>().enabled = false;  // Deshabilitamos el collider de la botella para que no interactúe más
            GetComponent<Rigidbody>().isKinematic = true;  // Hacemos que la botella no sea afectada por la física
        }
    }

    // Este método se llamará cuando el jugador deje la botella
    public void Drop()
    {
        if (isPickedUp)
        {
            isPickedUp = false;  // Marcamos que la botella ya no está en la mano
            transform.SetParent(null);  // Desvinculamos la botella del jugador
            GetComponent<Collider>().enabled = true;  // Volvemos a habilitar el collider
            GetComponent<Rigidbody>().isKinematic = false;  // Hacemos que la botella vuelva a ser afectada por la física
        }
    }
}
