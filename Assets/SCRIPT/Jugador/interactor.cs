using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactor : MonoBehaviour
{
    private GameObject currentBottle = null;  // Para llevar un registro de la botella que el jugador está sosteniendo

    void Update()
    {
        Interaction();
    }

    public void Interaction()
    {
        // Si presiono el clic izquierdo
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Creamos un rayo desde la cámara hacia la posición del cursor
            Ray ray = Camera.main?.ScreenPointToRay(Input.mousePosition);  // Usamos el operador '?.' para verificar si Camera.main es null
            if (ray == null) return;  // Si no hay cámara principal, no hacemos nada

            RaycastHit _hit;

            if (Physics.Raycast(ray, out _hit, 100))
            {
                // Si el objeto tiene el componente de interaccion
                var interactable = _hit.transform.gameObject.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    interactable.Interact();
                    Debug.Log("Interacción exitosa");
                }

                // Verificamos si el objeto es una botella
                var bottleScript = _hit.transform.gameObject.GetComponent<BotellaScript>();
                if (bottleScript != null)
                {
                    // Si no hay ninguna botella en la mano, tomar la botella
                    if (currentBottle == null)
                    {
                        currentBottle = _hit.transform.gameObject;  // Guardamos la referencia de la botella
                        bottleScript.PickUp();  // Tomamos la botella
                    }
                    else
                    {
                        Debug.Log("Ya tienes una botella en la mano.");
                    }
                }
            }
        }
    }
}
