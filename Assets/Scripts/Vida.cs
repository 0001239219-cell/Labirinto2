using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Vida : MonoBehaviour
{
    [Header("Configurações de Vida")]
    [SerializeField] private int vidaMaxima = 100;
    private int vidaAtual = 100;
    public TextMeshProUGUI textoVidaAtual;

    public SpriteRenderer spriteRenderer;
    public Color corDano = Color.red;
    public float duracaoFlash = 0.15f;

    private Color corOriginal;
    private float timerFlash;
    private bool piscando;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        corOriginal = spriteRenderer.color;

        vidaAtual = vidaMaxima;
        textoVidaAtual.text = ($"VIDA RESTANTE {vidaAtual}");

    }

    private void Update()
    {
        if (piscando)
        {
            timerFlash += Time.deltaTime;

            if (timerFlash >= duracaoFlash)
            {
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = corOriginal; // Volta a cor normal
                }
                piscando = false;
            }
        }
    }




    public void ReceberDano(int quantidadeDano)
    {

        if (vidaAtual <= 0 || quantidadeDano <= 0) return;

        vidaAtual -= quantidadeDano;
        

        // Se sobreviveu ao dano, ativa o efeito de piscar
        if (vidaAtual > 0)
        {
            textoVidaAtual.text = ($"VIDA RESTANTE {vidaAtual}");
            if (spriteRenderer != null)
            {
                Debug.Log($"Mudei a cor para: {corDano} no objeto {spriteRenderer.gameObject.name}");
                spriteRenderer.color = corDano;
                timerFlash = 0f;
                piscando = true;
            }
            else {
                Debug.LogError("O SpriteRenderer NAO ESTA ATRIBUIDO no script Vida!");
            }


        }
        else
        {
            vidaAtual = 0;
            Morrer();
        }
    




}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Morrer();       
        
        }
    }




    private void Morrer()
    {
        Debug.Log($"{gameObject.name} morreu!");

        
        gameObject.SetActive(false);
        SceneManager.LoadScene("GamePlay");

    }

}