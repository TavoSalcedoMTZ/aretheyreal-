using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momm : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animador; // Cambié 'ani' a 'animador'
    public Quaternion angulo;
    public float grado;

    public GameObject target;

    void Start()
    {
        animador = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    public void Comportamiento_Enemigo()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 5)
        {
            animador.SetBool("run", false);
            cronometro += Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animador.SetBool("walk", false);
                    break;

                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;

                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * Time.deltaTime);
                    animador.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            animador.SetBool("walk", false);
            animador.SetBool("run", true);
            transform.Translate(Vector3.forward * 4 * Time.deltaTime);
        }
    }

    void Update()
    {
        Comportamiento_Enemigo();
    }
}
