
using UnityEngine;
using TMPro;

public class PortaoComChave : MonoBehaviour
{
    [Header("Folhas do portão")]
    public Transform portaoEsquerdo;
    public Transform portaoDireito;

    [Header("Mensagem única")]
    public TMP_Text textoMensagem;

    [Header("Interação")]
    public KeyCode tecla = KeyCode.E;

    [Header("Abertura pelo eixo Z")]
    public float angulo = 90f;
    public float velocidade = 2f;

    private bool temChave = false;
    private bool jogadorPerto = false;
    private bool aberto = false;

    private Quaternion fechadoEsquerdo;
    private Quaternion fechadoDireito;
    private Quaternion abertoEsquerdo;
    private Quaternion abertoDireito;

    void Start()
    {
        if (portaoEsquerdo == null || portaoDireito == null)
        {
            Debug.LogError("Arraste as duas folhas do portão!");
            enabled = false;
            return;
        }

        fechadoEsquerdo = portaoEsquerdo.localRotation;
        fechadoDireito = portaoDireito.localRotation;

        abertoEsquerdo = fechadoEsquerdo *
            Quaternion.Euler(0f, 0f, -angulo);

        abertoDireito = fechadoDireito *
            Quaternion.Euler(0f, 0f, angulo);

        MostrarMensagem("");
    }

    public void LiberarPortao()
    {
        temChave = true;

        if (jogadorPerto)
            AtualizarMensagem();
    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(tecla))
        {
            if (!temChave)
            {
                AtualizarMensagem();
                return;
            }

            aberto = !aberto;
            AtualizarMensagem();
        }

        portaoEsquerdo.localRotation = Quaternion.Slerp(
            portaoEsquerdo.localRotation,
            aberto ? abertoEsquerdo : fechadoEsquerdo,
            velocidade * Time.deltaTime
        );

        portaoDireito.localRotation = Quaternion.Slerp(
            portaoDireito.localRotation,
            aberto ? abertoDireito : fechadoDireito,
            velocidade * Time.deltaTime
        );
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        jogadorPerto = true;
        AtualizarMensagem();
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        jogadorPerto = false;
        MostrarMensagem("");
    }

    void AtualizarMensagem()
    {
        if (!temChave)
            MostrarMensagem("VOCÊ PRECISA DE UMA CHAVE!");
        else if (aberto)
            MostrarMensagem("APERTE E PARA FECHAR");
        else
            MostrarMensagem("APERTE E PARA ABRIR");
    }

    void MostrarMensagem(string texto)
    {
        if (textoMensagem == null)
            return;

        textoMensagem.text = texto;
        textoMensagem.gameObject.SetActive(
            !string.IsNullOrEmpty(texto)
        );
    }
}