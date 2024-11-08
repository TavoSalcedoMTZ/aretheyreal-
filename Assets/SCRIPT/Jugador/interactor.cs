using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactor : MonoBehaviour
{
    
    // Funciones de interaccion
    void Update()
    {
        Interaction();
    }
    public void Interaction()
    {
        // Si presiono el clic izquiedo
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Creamos un rayo desde la camara hacia la posicion del cursor
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hit;
            
            if (Physics.Raycast(ray, out _hit, 100))
            {
                // Si el objeto tiene el componente de interaccion
                if (_hit.transform.gameObject.GetComponent<InteractableObject>() != null)
                {
                    _hit.transform.gameObject.GetComponent<InteractableObject>().Interact();
                    Debug.Log("Prueba jalando");
                }
                else
                {
                    Debug.Log("Prueba Fallida");
                   
                }
            }
        }
    }
}