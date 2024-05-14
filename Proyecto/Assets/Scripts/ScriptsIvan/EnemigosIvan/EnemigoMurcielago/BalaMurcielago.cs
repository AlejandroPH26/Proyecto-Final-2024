using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BalaMurcielago : MonoBehaviour

{   // Estadisticas
    public float velocidad = 10f; // Velocidad de la bala
    public int Daño = 1;

    //Variables externas
    private GameManagerHats gm;
    public JugadorV1 Player;

    //Variables internas
    private Rigidbody2D rb; 
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
     
        Player = FindObjectOfType<JugadorV1>();
        gm = GameManagerHats.instance;

        animator = GetComponent<Animator>();

        Vector2 direccion = (Player.transform.position - transform.position).normalized;
        rb.velocity = direccion * velocidad;
    }
    
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("Roca"))
        {
            rb.velocity = Vector2.zero;
            animator.Play("BAT_BULLET_DESTROY");
        }

        else if (collision.CompareTag("Player"))
        {
            // Acceder al GameManager y restarle vida al jugador
            rb.velocity = Vector2.zero;
            gm.RestarVidas();
            animator.Play("BAT_BULLET_DESTROY");  // En el ultimo frame da la animación se llama al metodo DestruirObjeto;
           
        }

        else if (collision.gameObject.CompareTag("BalaJugador"))
        {
            rb.velocity = Vector2.zero;
            Destroy(collision.gameObject);
            animator.Play("BAT_BULLET_DESTROY");
        }
    }

    public void DestruirObjeto()
    {
        Destroy(this.gameObject);
    }

    
}



