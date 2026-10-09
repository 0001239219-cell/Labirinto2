using UnityEngine;


public class ProjetilConfig : MonoBehaviour
{
    private Rigidbody2D rb;
    public float velocidade = 5f;
    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.linearVelocity = transform.up * velocidade;
        Destroy(gameObject, 3f);
    }

}
