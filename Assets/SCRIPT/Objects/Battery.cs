using System.Collections;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public Energia energymanager;
    private RandomSpawner spawner;

    private void Start()
    {
        // Buscar el componente RandomSpawner en la escena
        spawner = FindObjectOfType<RandomSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (energymanager.AddBatery())
            {
                // Notificar al spawner que la batería ha sido recogida
                if (spawner != null)
                {
                    spawner.NotifyObjectTaken(gameObject.tag);
                }

                // Destruir el objeto
                Destroy(gameObject);
            }
        }
    }
}
