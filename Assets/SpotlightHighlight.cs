using UnityEngine;

public class SpotlightHighlight : MonoBehaviour
{
    public GameObject spotlightGameObject;  // Referencia al GameObject que contiene la Spotlight
    public GameObject targetObject;         // El objeto que puede resaltar
    public Material highlightMaterial;      // Material de contorno para resaltar el objeto
    public Material defaultMaterial;        // Material original del objeto

    private void Update()
    {
        // Verificar si el GameObject que contiene la Spotlight est� activo
        bool isSpotlightActive = spotlightGameObject.activeInHierarchy;

        // Si el GameObject de la Spotlight est� activo, aplicamos el material de contorno
        if (isSpotlightActive)
        {
            targetObject.GetComponent<Renderer>().material = highlightMaterial;
        }
        else
        {
            // Si el GameObject no est� activo, restauramos el material original
            targetObject.GetComponent<Renderer>().material = defaultMaterial;
        }
    }
}
