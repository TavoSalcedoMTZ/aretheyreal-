using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public Energia energymanager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (energymanager.AddBatery())
            { 
                gameObject.SetActive(false);

            }

        }
    }
}
