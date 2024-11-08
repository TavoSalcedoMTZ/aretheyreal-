using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent OnInteract;

    public void Interact()
    {
        if (OnInteract != null)
        {

            Debug.Log("Prueba");
            OnInteract.Invoke();
        }
    }
}