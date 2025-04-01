using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarNivel : MonoBehaviour
{
    public void CambiarEscena(string nombre)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nombre);
       
    }

    public void PausarJuEGO()
    {

        Time.timeScale = 0;

    }
}
