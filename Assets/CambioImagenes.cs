using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambioImagenes : MonoBehaviour
{
    public Image imagen;
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public int Index;

    void Start()
    {

    }

    public void setSprite(int Index)
    {

        if (Index > 3)
        {
            Debug.LogWarning("ERROR");
        }
        else
        {


            switch (Index)
            {
                case 0:
                    imagen.sprite = sprite1;
                    break;
                case 1:
                    imagen.sprite = sprite2;
                    break;
                case 2:
                    imagen.sprite = sprite3;
                    break;
                case 3:
                    imagen.sprite = sprite4;
                    break;
                default:
                    Debug.Log("Índice fuera de rango");
                    break;
            }
        }
    }

}
