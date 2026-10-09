using UnityEngine;

public class Dano : MonoBehaviour
{
    [SerializeField] private int quantidadeDano = 10;
    [SerializeField] private bool destruirAoColidir = false;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Busca o componente Vida diretamente no objeto colidido
        Vida componenteVida = collision.GetComponent<Vida>();

        // Se encontrar o componente Vida, aplica o dano
        if (componenteVida != null)
        {
            componenteVida.ReceberDano(quantidadeDano);

            // Destrói o objeto atual se for um projétil
            if (destruirAoColidir)
            {
                Destroy(gameObject);
            }
        }
    }
}
