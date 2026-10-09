
using UnityEngine;
using TMPro;
using System.Collections;

public class ControladorMensagem : MonoBehaviour
{
    public TMP_Text textoColetada;

    private Coroutine rotinaMensagem;

    void Start()
    {
        if (textoColetada != null)
            textoColetada.gameObject.SetActive(false);
    }

    public void MostrarMensagem(string texto, float duracao)
    {
        if (textoColetada == null)
            return;

        if (rotinaMensagem != null)
            StopCoroutine(rotinaMensagem);

        textoColetada.text = texto;
        textoColetada.gameObject.SetActive(true);

        rotinaMensagem = StartCoroutine(
            EsconderMensagem(duracao)
        );
    }

    IEnumerator EsconderMensagem(float duracao)
    {
        yield return new WaitForSeconds(duracao);

        if (textoColetada != null)
            textoColetada.gameObject.SetActive(false);

        rotinaMensagem = null;
    }
}