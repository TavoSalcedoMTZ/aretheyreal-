using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    public Energia energymanager;
     public void OnColissionEnter(Collider other)
    {

        Debug.Log("Chocando");  
        if (other.CompareTag("Player"))
        {
            if (energymanager.AddBatery())
            { 
                gameObject.SetActive(false);

            }

        }
    }

 public void BatteryActive()
    {
        if (energymanager.AddBatery())
        {
            gameObject.SetActive(false);

        }
    }
}
    