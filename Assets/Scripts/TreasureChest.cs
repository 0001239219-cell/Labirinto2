
using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    [Header("Baú")]
    public Transform lid;
    public float anguloAberto = -90f;
    public float velocidade = 3f;

    [Header("Interação")]
    public KeyCode tecla = KeyCode.E;
    public GameObject mensagem;

    [Header("Chave")]
    public GameObject chave;

    private bool jogadorPerto = false;
    private bool aberto = false;

    private Quaternion rotacaoFechada;
    private Quaternion rotacaoAberta;

    void Start()
    {
        if (lid != null)
        {
            rotacaoFechada = lid.localRotation;

            rotacaoAberta = rotacaoFechada *
                Quaternion.Euler(anguloAberto, 0f, 0f);
        }

        if (mensagem != null)
            mensagem.SetActive(false);

        if (chave != null)
            chave.SetActive(false);
    }

    void Update()
    {
        // Abre o baú quando o jogador está perto
        // e aperta E.
        if (jogadorPerto &&
            !aberto &&
            Input.GetKeyDown(tecla))
        {
            aberto = true;

            if (mensagem != null)
                mensagem.SetActive(false);

            // Faz a chave aparecer.
            if (chave != null)
                chave.SetActive(true);
        }

        // Anima a rotação da tampa.
        if (lid != null)
        {
            Quaternion alvo = aberto
                ? rotacaoAberta
                : rotacaoFechada;

            lid.localRotation = Quaternion.Slerp(
                lid.localRotation,
                alvo,
                velocidade * Time.deltaTime
            );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;

            if (mensagem != null && !aberto)
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