using UnityEngine;

public class ArmadilhasConfig : MonoBehaviour
{
    public enum TipoArmadilha { Espeto, Torreta };

    [Header("Configuração da armadilha")]
    [SerializeField] private TipoArmadilha tipo = TipoArmadilha.Espeto;
    

    private Animator anim;
    [Header("Espeto")]
    public float atrasoInicial = 2f;
    public float tempoAtivado = 2f;
    public float tempoDesativado = 2f;
    public Collider2D colisorDano;

    private float timer = 0f;
    private bool espetoEstaAtivo;


    [Header("Torreta")]
    public GameObject preFabProjetil;
    public Transform disparador;
    private AudioSource audioSource;
    public AudioClip somDisparo;
    
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch (tipo)
        {
            case TipoArmadilha.Espeto:


                timer += Time.deltaTime;

                
                if (timer >= atrasoInicial + tempoAtivado + tempoDesativado)
                {
                    timer = 0f;
                    colisorDano.enabled = false;
                }
                
                else if (timer >= atrasoInicial + tempoAtivado)
                {
                    anim.SetInteger("Estado", 2); // Animação de desativar
                    colisorDano.enabled = false;
                }
               
                else if (timer >= atrasoInicial)
                {
                    anim.SetInteger("Estado", 1); // Animação de ativar
                    colisorDano.enabled = true;
                 }
                
                else
                {
                    anim.SetInteger("Estado", 0); // Animação Idle/Desativado
                    
                }


                           
                break;

            case TipoArmadilha.Torreta:

                Debug.Log("Torreta selecionada");
                
                anim.SetTrigger("Atirar");

                break;
        }
    }

    public void DispararTorreta() 
    {

        Instantiate(preFabProjetil, disparador.position, disparador.rotation);
        audioSource.PlayOneShot(somDisparo);
        
    }

    

    
}









