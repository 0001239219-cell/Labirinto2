
using UnityEngine;
using TMPro;

public class ChaveColetavel : MonoBehaviour
{
    [Header("Mensagem de coleta")]
    public GameObject mensagem;
    public ControladorMensagem controladorMensagem;

    [Header("Portão que esta chave abre")]
    public PortaoComChave portaoQueAbre;

    [Header("Interação")]
    public KeyCode tecla = KeyCode.E;

    private bool jogadorPerto = false;
    private bool coletada = false;

    void Start()
    {
        // A mensagem começa escondida.
        if (mensagem != null)
        {
            mensagem.SetActive(false);

            TMP_Text texto = mensagem.GetComponent<TMP_Text>();

            if (texto != null)
                texto.text = "APERTE E PARA COLETAR";
        }
    }

    void Update()
    {
        if (jogadorPerto &&
            !coletada &&
            Input.GetKeyDown(tecla))
        {
            if (portaoQueAbre == null)
            {
                Debug.LogError(
                    "Conecte o portão correto no Inspector!"
                );
                return;
            }

            coletada = true;

            // Libera somente o portão conectado.
            portaoQueAbre.LiberarPortao();

            if (mensagem != null)
                mensagem.SetActive(false);

            // Mostra a confirmação da coleta.
            if (controladorMensagem != null)
            {
                controladorMensagem.MostrarMensagem(
                    "VOCÊ PEGOU A CHAVE!",
                    3f
                );
            }

            // A chave desaparece após a coleta.
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !coletada)
        {
            jogadorPerto = true;

            if (mensagem != null)
                mensagem.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;

            if (mensagem != null)
                mensagem.SetActive(false);
        }
    }
}