using UnityEngine;

public class SpotlightHighlight : MonoBehaviour
{
    public GameObject spotlightGameObject;  // Referencia al GameObject que contiene la Spotlight
    public GameObject targetObject;         // El objeto que puede resaltar
    public Material highlightMaterial;      // Material de contorno para resaltar el objeto
    public Material defaultMaterial;        // Material original del objeto

    private void Update()
    {
        // Verificar si el GameObject que contiene la Spotlight está activo
        bool isSpotlightActive = spotlightGameObject.activeInHierarchy;

        // Si el GameObject de la Spotlight está activo, aplicamos el material de contorno
        if (isSpotlightActive)
        {
            targetObject.GetComponent<Renderer>().material = highlightMaterial;
        }
        else
        {
            // Si el GameObject no está activo, restauramos el material original
            targetObject.GetComponent<Renderer>().material = defaultMaterial;
        }
    }
}
