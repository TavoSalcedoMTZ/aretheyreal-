using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ZonaDeTexto : MonoBehaviour
{
    public UnityEvent AparicionTexto;
    public UnityEvent DesaparicionTexto;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AparicionTexto.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DesaparicionTexto.Invoke();
        }
    }

}
