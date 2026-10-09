
using UnityEngine;

public class BauInteracao : MonoBehaviour
{
    public GameObject mensagem;
    public Animator animacaoBau;

    private bool jogadorPerto = false;
    private bool aberto = false;

    void Start()
    {
        mensagem.SetActive(false);
    }

    void Update()
    {
        if (jogadorPerto && !aberto && Input.GetKeyDown(KeyCode.E))
        {
            aberto = true;
            mensagem.SetActive(false);
            animacaoBau.SetTrigger("Abrir");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !aberto)
        {
            jogadorPerto = true;
            mensagem.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            mensagem.SetActive(false);
        }
    }
}