using UnityEngine;

public class BotellaScript : MonoBehaviour
{
    public bool isPickedUp = false;  // Para saber si la botella está en la mano

    private void Start()
    {
        // Inicializar cualquier configuración si es necesario
    }

    public void PickUp(Transform handPosition)
    {
        isPickedUp = true;
        transform.SetParent(handPosition);  // Hacer la botella hija de la mano
        transform.localPosition = Vector3.zero;  // Posicionar la botella en la mano
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        isPickedUp = false;
        transform.SetParent(null);  // Desvincular la botella del jugador
        Destroy(gameObject);  // Destruir la botella completamente
    }

}