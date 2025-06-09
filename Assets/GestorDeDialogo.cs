using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GestorDeDialogos : MonoBehaviour
{
    public TextoAnimado animadorTexto;
    public string[] dialogos;
    public UnityEvent alFinalizarDialogo;

    private int indiceDialogo = 0;
    private bool esperandoInput = false;
    private bool dialogando = false;
    private Coroutine esperaCoroutine;

    void Start()
    {
        IniciarDialogo();
    }

    void Update()
    {
        if (!dialogando) return;

        if (esperandoInput && Input.GetKeyDown(KeyCode.Space))
        {
            AvanzarDialogo();
        }
    }

    public void IniciarDialogo()
    {
        indiceDialogo = 0;
        dialogando = true;
        StartCoroutine(MostrarDialogo(dialogos[indiceDialogo]));
    }

    private IEnumerator MostrarDialogo(string texto)
    {
        esperandoInput = false;

        yield return StartCoroutine(animadorTexto.BorrarTexto());
        yield return StartCoroutine(animadorTexto.EscribirTexto(texto));

        esperandoInput = true;

        if (esperaCoroutine != null)
            StopCoroutine(esperaCoroutine);
        esperaCoroutine = StartCoroutine(EsperarAutoContinuacion());
    }

    private IEnumerator EsperarAutoContinuacion()
    {
        yield return new WaitForSeconds(3f);

        if (esperandoInput)
        {
            AvanzarDialogo();
        }
    }

    private void AvanzarDialogo()
    {
        esperandoInput = false;

        if (esperaCoroutine != null)
            StopCoroutine(esperaCoroutine);

        indiceDialogo++;

        if (indiceDialogo < dialogos.Length)
        {
            StartCoroutine(MostrarDialogo(dialogos[indiceDialogo]));
        }
        else
        {
            FinalizarDialogo();
        }
    }

    private void FinalizarDialogo()
    {
   
        dialogando = false;
        alFinalizarDialogo?.Invoke(); 
    }
}
